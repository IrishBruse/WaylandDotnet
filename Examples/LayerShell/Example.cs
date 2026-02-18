namespace Example;

using System.Runtime.CompilerServices;
using System.Text;
using SDL3;
using WaylandDotnet;
using WaylandDotnet.Wlr;

public class Example
{
    // Wayland objects
    private static WlDisplay? wlDisplay = null;
    private static WlRegistry wlRegistry = null!;
    private static WlCompositor? wlCompositor = null;
    private static WlOutput? wlOutput = null;
    private static ZwlrLayerShellV1? layerShell = null;
    private static WlSurface? wlSurface = null;
    private static ZwlrLayerSurfaceV1? layerSurface = null;

    // SDL3 GPU objects
    private static IntPtr gpuDevice = IntPtr.Zero;

    // SDL objects - for events and custom surface
    private static IntPtr sdlWindow = IntPtr.Zero;

    // State
    private static int windowWidth = 800;
    private static int windowHeight = 600;
    private static bool done = false;
    private static float rotation = 0.0f;

    // GPU Pipeline and buffers
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
                wlOutput = wlRegistry.Bind<WlOutput>(name, version);
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

        // Get registry and bind globals
        wlRegistry = wlDisplay.GetRegistry();

        wlRegistry.OnGlobal += RegistryGlobal;

        // Dispatch to bind globals
        wlDisplay.Roundtrip();

        if (wlCompositor == null || layerShell == null)
        {
            Console.Error.WriteLine("Failed to bind required Wayland interfaces");
            return false;
        }

        return true;
    }


    private static bool InitSDL()
    {
        // Set SDL to use the existing Wayland display
        uint globalProps = SDL.SDL_GetGlobalProperties();

        Console.WriteLine("SetPointerProperty");

        SDL.SDL_SetPointerProperty(globalProps, SDL.SDL_PROP_GLOBAL_VIDEO_WAYLAND_WL_DISPLAY_POINTER, wlDisplay);

        Console.WriteLine("Init");


        if (!SDL.SDL_Init(SDL.SDL_InitFlags.SDL_INIT_VIDEO | SDL.SDL_InitFlags.SDL_INIT_EVENTS))
        {
            Console.Error.WriteLine($"Failed to initialize SDL: {SDL.SDL_GetError()}");
            return false;
        }

        return true;
    }

    private static bool InitSDLGPU()
    {
        // Create SDL3 GPU device with NULL driver (let SDL choose)
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
        // Create surface
        wlSurface = wlCompositor!.CreateSurface();
        if (wlSurface == null)
        {
            Console.Error.WriteLine("Failed to create surface");
            return false;
        }

        // Get layer surface role
        Console.WriteLine($"Creating layer surface (output: {wlOutput?.Handle.ToString() ?? "null"})...");
        layerSurface = layerShell!.GetLayerSurface(
            wlSurface,
            wlOutput,
            (uint)ZwlrLayerShellV1.Layer.Overlay,
            "sdl3-overlay"
        );

        if (layerSurface == null)
        {
            Console.Error.WriteLine("Failed to create layer surface");
            return false;
        }

        layerSurface.OnConfigure += (serial, w, h) =>
        {
            Console.WriteLine($"Layer surface configure: {w}x{h}");
            windowWidth = (int)w;
            windowHeight = (int)h;

            // Update SDL window size to match the new surface size
            if (sdlWindow != IntPtr.Zero)
            {
                SDL.SDL_SetWindowSize(sdlWindow, (int)w, (int)h);
            }

            layerSurface.AckConfigure(serial);
        };

        layerSurface.SetSize((uint)windowWidth, (uint)windowHeight);
        layerSurface.SetAnchor((uint)(
            ZwlrLayerSurfaceV1.AnchorFlag.Top |
            ZwlrLayerSurfaceV1.AnchorFlag.Left |
            ZwlrLayerSurfaceV1.AnchorFlag.Right |
            ZwlrLayerSurfaceV1.AnchorFlag.Bottom));
        layerSurface.SetExclusiveZone(-1);
        layerSurface.SetKeyboardInteractivity((uint)ZwlrLayerSurfaceV1.KeyboardInteractivity.Exclusive);

        wlSurface.Commit();
        wlDisplay?.Roundtrip();

        Console.WriteLine("Layer surface created");
        return true;
    }

    private static bool CreateSDLWindow()
    {
        // Create SDL window that wraps the custom Wayland surface
        uint props = SDL.SDL_CreateProperties();
        SDL.SDL_SetPointerProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WAYLAND_WL_SURFACE_POINTER, wlSurface!.Handle);
        SDL.SDL_SetBooleanProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WAYLAND_SURFACE_ROLE_CUSTOM_BOOLEAN, true);
        SDL.SDL_SetNumberProperty(props, SDL.SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER, windowWidth);
        SDL.SDL_SetNumberProperty(props, SDL.SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER, windowHeight);
        SDL.SDL_SetBooleanProperty(props, SDL.SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN, false);

        sdlWindow = SDL.SDL_CreateWindowWithProperties(props);
        SDL.SDL_DestroyProperties(props);

        if (sdlWindow == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create SDL window: {SDL.SDL_GetError()}");
            return false;
        }

        // Show the window
        SDL.SDL_ShowWindow(sdlWindow);

        Console.WriteLine("SDL window created for custom surface");
        return true;
    }

    private static bool ClaimWindowForGPU()
    {
        // Claim the window for GPU rendering
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
                Console.Error.WriteLine($"Failed to create fragment shader: {SDL.SDL_GetError()}");
                SDL.SDL_ReleaseGPUShader(gpuDevice, ptr);
                return IntPtr.Zero;
            }
            return ptr;
        }
    }

    private static unsafe bool CreateTrianglePipeline()
    {
        IntPtr vertexShader = LoadShaderFromFile("Shaders/Triangle.vert.spv", SDL.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_VERTEX, 1);
        IntPtr fragmentShader = LoadShaderFromFile("Shaders/Triangle.frag.spv", SDL.SDL_GPUShaderStage.SDL_GPU_SHADERSTAGE_FRAGMENT, 0);

        // Create graphics pipeline
        var colorTargetDesc = new SDL.SDL_GPUColorTargetDescription
        {
            format = SDL.SDL_GetGPUSwapchainTextureFormat(gpuDevice, sdlWindow)
        };

        // Allocate vertex buffer description and attributes on stack
        var vertexBufferDesc = new SDL.SDL_GPUVertexBufferDescription
        {
            slot = 0,
            pitch = (uint)(sizeof(float) * 5),
            input_rate = SDL.SDL_GPUVertexInputRate.SDL_GPU_VERTEXINPUTRATE_VERTEX,
            instance_step_rate = 0
        };

        var vertexAttribute0 = new SDL.SDL_GPUVertexAttribute
        {
            location = 0,
            format = SDL.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT2,
            offset = 0,
            buffer_slot = 0
        };

        var vertexAttribute1 = new SDL.SDL_GPUVertexAttribute
        {
            location = 1,
            format = SDL.SDL_GPUVertexElementFormat.SDL_GPU_VERTEXELEMENTFORMAT_FLOAT3,
            offset = (uint)(sizeof(float) * 2),
            buffer_slot = 0
        };

        {
            // Use stackalloc for all pointer-based structures
            SDL.SDL_GPUVertexBufferDescription* vertexBufferDescPtr = stackalloc SDL.SDL_GPUVertexBufferDescription[1];
            vertexBufferDescPtr[0] = vertexBufferDesc;

            SDL.SDL_GPUVertexAttribute* vertexAttributesPtr = stackalloc SDL.SDL_GPUVertexAttribute[2];
            vertexAttributesPtr[0] = vertexAttribute0;
            vertexAttributesPtr[1] = vertexAttribute1;

            SDL.SDL_GPUColorTargetDescription* colorTargetDescPtr = stackalloc SDL.SDL_GPUColorTargetDescription[1];
            colorTargetDescPtr[0] = colorTargetDesc;

            var vertexInputState = new SDL.SDL_GPUVertexInputState
            {
                num_vertex_buffers = 1,
                vertex_buffer_descriptions = vertexBufferDescPtr,
                num_vertex_attributes = 2,
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
                primitive_type = SDL.SDL_GPUPrimitiveType.SDL_GPU_PRIMITIVETYPE_TRIANGLELIST,
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

        // Create vertex buffer
        // Triangle vertices: position (x,y) + color (r,g,b)
        float[] vertices = new float[]
        {
            // Top vertex - Red
            0.0f,  0.5f,  1.0f, 0.0f, 0.0f,
            // Bottom left - Green
            -0.5f, -0.5f,  0.0f, 1.0f, 0.0f,
            // Bottom right - Blue
            0.5f, -0.5f,  0.0f, 0.0f, 1.0f
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

        // Upload vertex data
        var transferInfo = new SDL.SDL_GPUTransferBufferCreateInfo
        {
            size = (uint)(vertices.Length * sizeof(float)),
            usage = SDL.SDL_GPUTransferBufferUsage.SDL_GPU_TRANSFERBUFFERUSAGE_UPLOAD
        };

        IntPtr transferBuffer = SDL.SDL_CreateGPUTransferBuffer(gpuDevice, in transferInfo);
        if (transferBuffer == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to create transfer buffer: {SDL.SDL_GetError()}");
            return false;
        }

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

        Console.WriteLine("Triangle pipeline created");
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
                if (e.key.key == (int)SDL.SDL_Keycode.SDLK_ESCAPE)
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
        // Update rotation
        rotation += 0.02f;

        // Begin command buffer
        IntPtr cmdBuf = SDL.SDL_AcquireGPUCommandBuffer(gpuDevice);
        if (cmdBuf == IntPtr.Zero)
        {
            Console.Error.WriteLine($"Failed to acquire command buffer: {SDL.SDL_GetError()}");
            return;
        }

        // Get a texture to render to
        IntPtr renderTarget;
        uint textureWidth, textureHeight;

        if (!SDL.SDL_WaitAndAcquireGPUSwapchainTexture(cmdBuf, sdlWindow, out renderTarget, out textureWidth, out textureHeight))
        {
            Console.Error.WriteLine($"Failed to acquire swapchain texture: {SDL.SDL_GetError()}");
            SDL.SDL_CancelGPUCommandBuffer(cmdBuf);
            return;
        }

        // Skip rendering if texture is NULL (window minimized, etc.)
        if (renderTarget == IntPtr.Zero)
        {
            SDL.SDL_SubmitGPUCommandBuffer(cmdBuf);
            return;
        }

        // Begin render pass with a semi-transparent dark overlay
        var colorTarget = new SDL.SDL_GPUColorTargetInfo
        {
            texture = renderTarget,
            mip_level = 0,
            layer_or_depth_plane = 0,
            clear_color = new SDL.SDL_FColor { r = 0.0f, g = 0.0f, b = 0.0f, a = 0.5f },
            load_op = SDL.SDL_GPULoadOp.SDL_GPU_LOADOP_CLEAR,
            store_op = SDL.SDL_GPUStoreOp.SDL_GPU_STOREOP_STORE
        };

        // Create Span for color target
        var colorTargets = new[] { colorTarget };
        IntPtr renderPass = SDL.SDL_BeginGPURenderPass(cmdBuf, colorTargets, 1, Unsafe.NullRef<SDL.SDL_GPUDepthStencilTargetInfo>());

        // Bind pipeline and draw triangle
        SDL.SDL_BindGPUGraphicsPipeline(renderPass, pipeline);

        // Push rotation constant - using 16 bytes for std140 alignment
        float[] uniformData = new float[] { rotation, 0.0f, 0.0f, 0.0f };
        fixed (float* uniformDataPtr = uniformData)
        {
            SDL.SDL_PushGPUVertexUniformData(cmdBuf, 0, new nint(uniformDataPtr), (uint)(uniformData.Length * sizeof(float)));
        }

        // Bind vertex buffer
        var vertexBindings = new[]
        {
            new SDL.SDL_GPUBufferBinding
            {
                buffer = vertexBuffer,
                offset = 0
            }
        };
        SDL.SDL_BindGPUVertexBuffers(renderPass, 0, vertexBindings, 1);

        // Draw the triangle
        SDL.SDL_DrawGPUPrimitives(renderPass, 3, 1, 0, 0);

        // End render pass
        SDL.SDL_EndGPURenderPass(renderPass);

        // Submit command buffer
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

        if (!CreateTrianglePipeline())
        {
            Console.Error.WriteLine("Failed to create triangle pipeline");
            Cleanup();
            return 1;
        }

        // Recommit the surface after SDL window is created and shown
        wlSurface?.Commit();
        wlDisplay?.Flush();

        Console.WriteLine("All initialized. Press ESC to exit.");

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