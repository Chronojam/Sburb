shader_type spatial;

uniform sampler2D NOISE_PATTERN;

uniform vec4 albedo : hint_color = vec4(1.0f);
uniform sampler2D albedo_texture : hint_albedo;


void fragment(){
	float noiseValue = texture(NOISE_PATTERN, UV).x;
	ALBEDO = albedo.rgb * texture(albedo_texture, UV).rgb * vec3(noiseValue);
}