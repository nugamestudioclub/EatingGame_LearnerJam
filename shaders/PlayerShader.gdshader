shader_type spatial;

uniform vec3 offset;

void vertex(){
	VERTEX = (vec4(VERTEX, 1.0)).xyz+offset;
    NORMAL = normalize((MODELVIEW_MATRIX * vec4(NORMAL, 0.0)).xyz);
}
void fragment() {
	// Place fragment code here.
}
