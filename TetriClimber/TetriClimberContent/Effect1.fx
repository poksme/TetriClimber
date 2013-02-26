sampler s0;
float param1;

float4 BlackAndWhite(float2 coords: TEXCOORD0) : COLOR0
{
    return float4(1, 0, 0, 1);
}

float4 Disapear(float2 coords: TEXCOORD0) : COLOR0
{
	return float4(1, 0, 0, 1);
}

technique Technique1
{
    pass Pass1
    {
        // TODO: set renderstates here.
        PixelShader = compile ps_2_0 BlackAndWhite();
    }

	pass Pass2
	{
		PixelShader = compile ps_2_0 Disapear();
	}
}
