#version 450

layout(location = 0) in vec2 fragUV;
layout(location = 0) out vec4 outColor;

void main() {
    float gradient = mix(0.15, 0.25, fragUV.y);
    vec3 topColor = vec3(0.2, 0.2, 0.25);
    vec3 bottomColor = vec3(0.1, 0.1, 0.15);
    vec3 color = mix(bottomColor, topColor, fragUV.y);
    outColor = vec4(color, 0.95);
}
