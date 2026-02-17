#version 450

layout(binding = 0, set = 1) uniform UniformBuffer {
    float rotation;
};

layout(location = 0) in vec2 inPosition;
layout(location = 1) in vec3 inColor;

layout(location = 0) out vec3 fragColor;

void main() {
    float c = cos(rotation);
    float s = sin(rotation);
    mat2 rot = mat2(c, -s, s, c);
    vec2 pos = rot * inPosition;
    gl_Position = vec4(pos, 0.0, 1.0);
    fragColor = inColor;
}