namespace Taskbar;

using System.Runtime.CompilerServices;
using System.Text;
using SDL3;
using WaylandDotnet;
using WaylandDotnet.Wlr;

public class Example
{
    private static WlDisplay? wlDisplay = null;
    private static WlRegistry wlRegistry = null!;
    private static WlCompositor? wlCompositor = null;
    private static WlOutput? wlOutput = null;
    private static ZwlrLayerShellV1? layerShell = null;
    private static WlSurface? wlSurface = null;
    private static ZwlrLayerSurfaceV1? layerSurface = null;

    private static List<WlOutput> allOutputs = [];
    private static (WlOutput output, int x, int y) mainOutput;

    private static IntPtr gpuDevice = IntPtr.Zero;
    private static IntPtr sdlWindow = IntPtr.Zero;

    private static int taskbarHeight = 48;
    private static int screenWidth = 0;
    private static bool done = false;

    private static IntPtr pipeline = IntPtr.Zero;
    private static IntPtr vertexBuffer = IntPtr.Zero;

    private static void RegistryGlobal(uint name, string interfaceName, uint version)
    {
        switch (interfaceName)
        {
            case WlCompositor.InterfaceName:
                wlCompositor = wlRegistry.Bind<WlCompositor>(name, version);
                break;

            case ZwlrLayerShellV1.InterfaceName:
                layerShell = wlRegistry.Bind<ZwlrLayerShellV1>(name, version);
                break;

            case WlOutput.InterfaceName:
                var output = wlRegistry.Bind<WlOutput>(name, version);
                allOutputs.Add(output);
                output.OnGeometry += (x, y, _, _, _, _, _, _) =>
                {
                    Console.WriteLine($"Output {name}: position ({x}, {y})");
                    if (x == 0 && y == 0)
                    {
                        mainOutput = (output, x, y);
                    }
                };
                break;
        }
    }

    private static bool InitWayland()
    {
        wlDisplay = WlDisplay.Connect();
        if (wlDisplay == null)
        {
            Console.Error.WriteLine("Failed to connect to Wayland display");
            return false;
        }

        wlRegistry = wlDisplay.GetRegistry();
        wlRegistry.OnGlobal += RegistryGlobal;
        wlDisplay.Roundtrip();

        wlDisplay?.DispatchPending();
        wlDisplay?.Roundtrip();

        if (wlCompositor == null || layerShell == null)
        {
            Console.Error.WriteLine("Failed to bind required Wayland interfaces");
            return false;
        }

        if (mainOutput.output != null)
        {
            wlOutput = mainOutput.output;
            Console.WriteLine("Using main output at position (0, 0)");
        }
        else if (allOutputs.Count > 0)
        {
            wlOutput = allOutputs[0];
            Console.WriteLine($"Using first output (no output at 0,0 found)");
        }
        else
        {
            Console.Error.WriteLine("No outputs available");
            return false;
        }

        return true;
    }

    private static bool InitSDL()
    {
        uint globalProps = SDL.SDL_GetGlobalProperties();
        SDL.SDL_SetPointerProperty(globalProps, SDL.SDL_PROP_GLOBAL_VIDEO_WAYLAND_WL_DISPLAY_POINTER, wlDisplay);

        if (!SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO | SDL.SDL_InitFlags.SDL_INIT_EVENTS))
        {
            Console.Error.WriteLine($"Failed to initialize SDL: {SDL.SDL_GetError()}");
            return false;
        }

        return true;
    }

    private static bool InitSDLGPU()
    {
        gpuDevice = SDL.SDL_CreateGPUDevice(SDL.SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_SPIRV, true, null);
        if (gpuDevice == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create SDL GPU device: {SDL.SDL_GetError()}");
            return false;
        }

        return true;
    }

    private static bool CreateLayerShellSurface()
    {
        wlSurface = wlCompositor!.CreateSurface();
        if (wlSurface == null)
        {
            Console.Error.WriteLine("Failed to create surface");
            return false;
        }

        layerSurface = layerShell!.GetLayerSurface(
            wlSurface,
            wlOutput,
            (uint)ZwlrLayerShellV1.Layer.Bottom,
            "waylanddotnet-taskbar"
        );

        if (layerSurface == null)
        {
            Console.Error.WriteLine("Failed to create layer surface");
            return false;
        }

        layerSurface.OnConfigure += (serial, w, h) =>
        {
            Console.WriteLine($"Layer surface configure: {w}x{h}");
            screenWidth = (int)w;
            taskbarHeight = (int)h;

            if (sdlWindow != IntPtr.Zero)
            {
                SDL.SDL_SetWindowSize(sdlWindow, (int)w, (int)h);
            }

            layerSurface.AckConfigure(serial);
        };

        layerSurface.SetSize((uint)screenWidth, (uint)taskbarHeight);
        layerSurface.SetAnchor((uint)(
            ZwlrLayerSurfaceV1.AnchorFlag.Bottom |
            ZwlrLayerSurfaceV1.AnchorFlag.Left |
            ZwlrLayerSurfaceV1.AnchorFlag.Right));
        layerSurface.SetExclusiveZone(taskbarHeight);
        layerSurface.SetKeyboardInteractivity((uint)ZwlrLayerSurfaceV1.KeyboardInteractivity.None);

        wlSurface.Commit();
        wlDisplay?.Roundtrip();

        Console.WriteLine("Layer surface created");
        return true;
    }

    private static bool CreateSDLWindow()
    {
        uint props = SDL.SDL_CreateProperties();
        SDL.SDL_SetPointerProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER, wlSurface!.Handle);
        SDL.SDL_SetBooleanProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN, true);
        SDL.SDL_SetNumberProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER, screenWidth);
        SDL.SDL_SetNumberProperty(props, SDL.SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER, taskbarHeight);
        SDL.SDL_SetBooleanProperty(props, SDL.SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN, false);

        sdlWindow = SDL.SDL_CreateWindowWithProperties(props);
        SDL.SDL_DestroyProperties(props);

        if (sdlWindow == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create SDL window: {SDL.SDL_GetError()}");
            return false;
        }

        SDL.SDL_ShowWindow(sdlWindow);
        Console.WriteLine("SDL window created for custom surface");
        return true;
    }

    private static bool ClaimWindowForGPU()
    {
        if (!SDL.SDL_ClaimWindowForGPUDevice(gpuDevice, sdlWindow))
        {
            Console.Error.WriteLine($"Failed to claim window for GPU: {SDL.SDL_GetError()}");
            return false;
        }

        Console.WriteLine("Window claimed for GPU rendering");
        return true;
    }

    private static unsafe nint LoadShaderFromFile(string path, SDL.SDL_GPUShaderStage stage, uint numUniformBuffers)
    {
        byte[] spirv = File.ReadAllBytes(path);
        byte[] entryPointBytes = Encoding.UTF8.GetBytes("main");

        fixed (byte* entryPointPtr = entryPointBytes)
        fixed (byte* codePtr = spirv)
        {
            var shaderInfo = new SDL.SDL_GPUShaderCreateInfo
            {
                code_size = (nuint)spirv.Length,
                code = codePtr,
                entrypoint = entryPointPtr,
                format = SDL.SDL_GPUShaderFormat.SDL_GPU_SHADERFORMAT_SPIRV,
                stage = stage,
                num_uniform_buffers = numUniformBuffers,
                num_samplers = 0,
                num_storage_textures = 0,
                num_storage_buffers = 0
            };

            var ptr = SDL.SDL_CreateGPUShader(gpuDevice, in shaderInfo);
            if (ptr == IntPtr.Zero)
            {
                Console.Error.WriteLine($"Failed to create shader: {SDL.SDL_GetError()}");
                return IntPtr.Zero;
            }
            return ptr;
        }
    }

    private static unsafe bool CreateTaskbarPipeline()
    {
        IntPtr vertexShader = LoadShaderFromFile("Shaders/Taskbar.vert.spv", SDL.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_VERTEX, 0);
        IntPtr fragmentShader = LoadShaderFromFile("Shaders/Taskbar.frag.spv", SDL.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_FRAGMENT, 0);

        var colorTargetDesc = new SDL.SDL_GPUColorTargetDescription
        {
            format = SDL.SDL_GetGPUSwapchainTextureFormat(gpuDevice, sdlWindow)
        };

        var vertexBufferDesc = new SDL.SDL_GPUVertexBufferDescription
        {
            slot = 0,
            pitch = (uint)(sizeof(float) * 2),
            input_rate = SDL.SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_VERTEX,
            instance_step_rate = 0
        };

        var vertexAttribute = new SDL.SDL_GPUVertexAttribute
        {
            location = 0,
            format = SDL.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
            offset = 0,
            buffer_slot = 0
        };

        {
            SDL.SDL_GPUVertexBufferDescription* vertexBufferDescPtr = stackalloc SDL.SDL_GPUVertexBufferDescription[1];
            vertexBufferDescPtr[0] = vertexBufferDesc;

            SDL.SDL_GPUVertexAttribute* vertexAttributesPtr = stackalloc SDL.SDL_GPUVertexAttribute[1];
            vertexAttributesPtr[0] = vertexAttribute;

            SDL.SDL_GPUColorTargetDescription* colorTargetDescPtr = stackalloc SDL.SDL_GPUColorTargetDescription[1];
            colorTargetDescPtr[0] = colorTargetDesc;

            var vertexInputState = new SDL.SDL_GPUVertexInputState
            {
                num_vertex_buffers = 1,
                vertex_buffer_descriptions = vertexBufferDescPtr,
                num_vertex_attributes = 1,
                vertex_attributes = vertexAttributesPtr
            };

            var targetInfo = new SDL.SDL_GPUGraphicsPipelineTargetInfo
            {
                num_color_targets = 1,
                color_target_descriptions = colorTargetDescPtr,
                has_depth_stencil_target = false
            };

            var pipelineInfo = new SDL.SDL_GPUGraphicsPipelineCreateInfo
            {
                vertex_shader = vertexShader,
                fragment_shader = fragmentShader,
                vertex_input_state = vertexInputState,
                primitive_type = SDL.SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_TRIANGLESTRIP,
                rasterizer_state = new SDL.SDL_GPURasterizerState
                {
                    fill_mode = SDL.SDL_GPUFillMode.SDL_GPU_FILLMODE_FILL,
                    cull_mode = SDL.SDL_GPUCullMode.SDL_GPU_CULLMODE_NONE
                },
                multisample_state = new SDL.SDL_GPUMultisampleState
                {
                    sample_count = SDL.SDL_GPUSampleCount.SDL_GPU_SAMPLECOUNT_1,
                    sample_mask = 0
                },
                depth_stencil_state = new SDL.SDL_GPUDepthStencilState
                {
                    compare_op = SDL.SDL_GPUCompareOp.SDL_GPU_COMPAREOP_ALWAYS
                },
                target_info = targetInfo
            };

            pipeline = SDL.SDL_CreateGPUGraphicsPipeline(gpuDevice, in pipelineInfo);
        }

        SDL.SDL_ReleaseGPUShader(gpuDevice, vertexShader);
        SDL.SDL_ReleaseGPUShader(gpuDevice, fragmentShader);

        if (pipeline == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create graphics pipeline: {SDL.SDL_GetError()}");
            return false;
        }

        float[] vertices = new float[]
        {
            -1.0f, -1.0f,
             1.0f, -1.0f,
            -1.0f,  1.0f,
             1.0f,  1.0f
        };

        var bufferInfo = new SDL.SDL_GPUBufferCreateInfo
        {
            size = (uint)(vertices.Length * sizeof(float)),
            usage = SDL.SDL_GPUBufferUsageFlags.SDL_GPU_BUFFERUSAGE_VERTEX
        };

        vertexBuffer = SDL.SDL_CreateGPUBuffer(gpuDevice, in bufferInfo);
        if (vertexBuffer == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create vertex buffer: {SDL.SDL_GetError()}");
            return false;
        }

        var transferInfo = new SDL.SDL_GPUTransferBufferCreateInfo
        {
            size = (uint)(vertices.Length * sizeof(float)),
            usage = SDL.SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD
        };

        IntPtr transferBuffer = SDL.SDL_CreateGPUTransferBuffer(gpuDevice, in transferInfo);
        IntPtr data = SDL.SDL_MapGPUTransferBuffer(gpuDevice, transferBuffer, false);
        fixed (float* verticesPtr = vertices)
        {
            Buffer.MemoryCopy(verticesPtr, (void*)data, vertices.Length * sizeof(float), vertices.Length * sizeof(float));
        }
        SDL.SDL_UnmapGPUTransferBuffer(gpuDevice, transferBuffer);

        IntPtr cmdBuf = SDL.SDL_AcquireGPUCommandBuffer(gpuDevice);
        IntPtr copyPass = SDL.SDL_BeginGPUCopyPass(cmdBuf);

        var src = new SDL.SDL_GPUTransferBufferLocation
        {
            transfer_buffer = transferBuffer,
            offset = 0
        };

        var dst = new SDL.SDL_GPUBufferRegion
        {
            buffer = vertexBuffer,
            offset = 0,
            size = (uint)(vertices.Length * sizeof(float))
        };

        SDL.SDL_UploadToGPUBuffer(copyPass, in src, in dst, false);
        SDL.SDL_EndGPUCopyPass(copyPass);
        SDL.SDL_SubmitGPUCommandBuffer(cmdBuf);

        SDL.SDL_ReleaseGPUTransferBuffer(gpuDevice, transferBuffer);

        Console.WriteLine("Taskbar pipeline created");
        return true;
    }

    private static void HandleEvents()
    {
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e))
        {
            if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_QUIT)
            {
                done = true;
            }
            else if (e.type == (uint)SDL.SDL_EventType.SDL_EVENT_KEY_DOWN)
            {
                if (e.key.key == (int)SDL.SDL_Keycode.SDLK_ESCAPE || e.key.key == (int)SDL.SDL_Keycode.SDLK_Q)
                {
                    done = true;
                }
            }
        }

        wlDisplay?.Flush();
        wlDisplay?.DispatchPending();
    }

    private static unsafe void Render()
    {
        IntPtr cmdBuf = SDL.SDL_AcquireGPUCommandBuffer(gpuDevice);
        if (cmdBuf == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to acquire command buffer: {SDL.SDL_GetError()}");
            return;
        }

        IntPtr renderTarget;
        uint textureWidth, textureHeight;

        if (!SDL.SDL_WaitAndAcquireGPUSwapchainTexture(cmdBuf, sdlWindow, out renderTarget, out textureWidth, out textureHeight))
        {
            Console.Error.WriteLine($"Failed to acquire swapchain texture: {SDL.SDL_GetError()}");
            SDL.SDL_CancelGPUCommandBuffer(cmdBuf);
            return;
        }

        if (renderTarget == IntPtr.Zero)
        {
            SDL.SDL_SubmitGPUCommandBuffer(cmdBuf);
            return;
        }

        var colorTarget = new SDL.SDL_GPUColorTargetInfo
        {
            texture = renderTarget,
            mip_level = 0,
            layer_or_depth_plane = 0,
            clear_color = new SDL.SDL_FColor { r = 0.1f, g = 0.1f, b = 0.12f, a = 1.0f },
            load_op = SDL.SDL_GPULoadOp.SDL_GPU_LOADOP_CLEAR,
            store_op = SDL.SDL_GPUStoreOp.SDL_GPU_STOREOP_STORE
        };

        var colorTargets = new[] { colorTarget };
        IntPtr renderPass = SDL.SDL_BeginGPURenderPass(cmdBuf, colorTargets, 1, Unsafe.NullRef<SDL.SDL_GPUDepthStencilTargetInfo>());

        SDL.SDL_BindGPUGraphicsPipeline(renderPass, pipeline);

        var vertexBindings = new[]
        {
            new SDL.SDL_GPUBufferBinding
            {
                buffer = vertexBuffer,
                offset = 0
            }
        };
        SDL.SDL_BindGPUVertexBuffers(renderPass, 0, vertexBindings, 1);

        SDL.SDL_DrawGPUPrimitives(renderPass, 4, 1, 0, 0);

        SDL.SDL_EndGPURenderPass(renderPass);
        SDL.SDL_SubmitGPUCommandBuffer(cmdBuf);
    }

    private static void Cleanup()
    {
        if (vertexBuffer != IntPtr.Zero && gpuDevice != IntPtr.Zero)
        {
            SDL.SDL_ReleaseGPUBuffer(gpuDevice, vertexBuffer);
        }

        if (pipeline != IntPtr.Zero && gpuDevice != IntPtr.Zero)
        {
            SDL.SDL_ReleaseGPUGraphicsPipeline(gpuDevice, pipeline);
        }

        if (gpuDevice != IntPtr.Zero && sdlWindow != IntPtr.Zero)
        {
            SDL.SDL_ReleaseWindowFromGPUDevice(gpuDevice, sdlWindow);
        }

        if (gpuDevice != IntPtr.Zero)
        {
            SDL.SDL_DestroyGPUDevice(gpuDevice);
        }

        if (sdlWindow != IntPtr.Zero)
        {
            SDL.SDL_DestroyWindow(sdlWindow);
        }

        SDL.SDL_Quit();
    }

    public static int Run()
    {
        WaylandLogger.Initialize();

        if (!InitWayland())
        {
            Console.Error.WriteLine("Failed to init Wayland");
            Cleanup();
            return 1;
        }

        if (!InitSDL())
        {
            Console.Error.WriteLine("Failed to init SDL");
            Cleanup();
            return 1;
        }

        if (!InitSDLGPU())
        {
            Console.Error.WriteLine("Failed to init SDL GPU");
            Cleanup();
            return 1;
        }

        if (!CreateLayerShellSurface())
        {
            Console.Error.WriteLine("Failed to create layer shell surface");
            Cleanup();
            return 1;
        }

        if (!CreateSDLWindow())
        {
            Console.Error.WriteLine("Failed to create SDL window");
            Cleanup();
            return 1;
        }

        if (!ClaimWindowForGPU())
        {
            Console.Error.WriteLine("Failed to claim window for GPU");
            Cleanup();
            return 1;
        }

        if (!CreateTaskbarPipeline())
        {
            Console.Error.WriteLine("Failed to create taskbar pipeline");
            Cleanup();
            return 1;
        }

        wlSurface?.Commit();
        wlDisplay?.Flush();

        Console.WriteLine("Taskbar initialized. Press ESC or Q to exit.");

        while (!done)
        {
            HandleEvents();
            Render();
            SDL.SDL_Delay(16);
        }

        Cleanup();
        Console.WriteLine("Exited cleanly");
        return 0;
    }
}
