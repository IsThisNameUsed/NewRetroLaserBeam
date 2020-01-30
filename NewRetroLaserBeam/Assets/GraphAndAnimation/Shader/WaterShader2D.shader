Shader "RockSolid/WaterShader2D"
{
	Properties
	{
		// Usual stuffs
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) TransGloss (A)", 2D) = "white" {}

		_Power("Power",Range(0.0,2.0)) = 1.0
	}
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			//"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"CanUseSpriteAtlas" = "True"
		}
		Pass {
			Blend One One
			SetTexture[_MainTex] { combine texture }
		}
		// Main Surface Pass (Handles Spot/Point lights)
		CGPROGRAM
			 #pragma vertex vert
		#pragma surface surf Standard alpha fullforwardshadows //approxview
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float4 _Color;

		float _Power;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};
		void vert(inout appdata_full v) {
			// Do whatever you want with the "vertex" property of v here
			//v.vertex.x += _Power;
		}
		void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex + float2(_Time.y* _Power,0));
			o.Albedo = col.rgb * _Color.rgb;
			o.Alpha = col.a * _Color.a;
		}		

		ENDCG

		// Shadow Pass : Adding the shadows (from Directional Light)
		// by blending the light attenuation
	}
	Fallback "Diffuse"
}
