void CirclePattern_float(in float2 uv, in float2 center, in float radius, in float smooth, out float output)
{
    float circle = pow((uv.y - center.y), 2) + pow((uv.x - center.x), 2);
	float radiusQ = pow(radius, 2);
	
	if(circle < radiusQ)
	{
	    output = smoothstep(radiusQ, radiusQ - smooth, circle);
	}
	else
	    output = 0;
}