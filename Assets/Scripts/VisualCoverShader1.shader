// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ThisIsTheShit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PlayerPos("Player Pos", Vector) = (0,0,0,0)
		_Radius("Radius", float) = 0
		_NoiseTex("Noise Tex", 2D) = "white" {}
		_NoiseSpeed("Noise Speed", float) = 0
		_NoiseFreq("Noise Frequency", Range(0.1,5)) = 0
		_NoiseMult("Noise Multiplier", float) = 0
		_PixelOffset("Pixel Offset", Range(0,1)) = 0
		_GradientOffset("GradientOffset", Range(0,200)) = 0

		
		_NoiseTex2("Noise2 Tex", 2D) = "white" {}
		_NoiseFreq2("Noise2 Frequency", Range(0.1,5)) = 0
		_NoiseRim("Noise2 Rim Mult", float) = 0
		_NoiseRimColor("Noise2 Rim Color", Color) = (0,0,0,0)

		//_ColorGrading("Try", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#define M_PI 3.14
			#include "UnityCG.cginc"
	
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
		
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 scrPos: TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.vertex);
				o.uv = v.uv;
		
				return o;
			}
			


			uniform sampler2D _MainTex, _NoiseTex, _ColorGrading, _NoiseTex2;
			uniform float4  _PlayerPos, _NoiseRimColor;
			uniform float _NoiseFreq, _NoiseMult, _NoiseSpeed, _PixelOffset, _Radius,_GradientOffset,_NoiseFreq2, _NoiseRim;
			
			fixed4 frag (v2f i) : SV_Target
			{


				
					float noise = _NoiseMult * tex2D(_NoiseTex,  float2(i.scrPos.x , i.scrPos.y) / _NoiseFreq).r;
					float2 noiseToDirection = float2(cos(noise*M_PI * 2 + _Time.x * _NoiseSpeed), sin(noise*M_PI * 2 + _Time.x * _NoiseSpeed));
					float4 col;

		
				float2 pPos = _PlayerPos.xy;
				float2 cPos = float2(_PlayerPos.z * i.scrPos.x, _PlayerPos.w * i.scrPos.y);

				float sqrPos = sqrt(dot(pPos - cPos, pPos - cPos));

				float4 colNormal = col = tex2D(_MainTex, i.scrPos.xy);
				float4 colDistorted = tex2D(_MainTex, i.scrPos.xy + _PixelOffset * normalize(noiseToDirection));

				float greyScale = (colDistorted.x + colDistorted.y + colDistorted.z) / 3;
				colDistorted = float4(greyScale, greyScale, greyScale, 1);

				
				float rimNoise = tex2D(_NoiseTex2, i.scrPos * _NoiseFreq2).r;
				float2 o = float2(0.5, 0.5);
			
				float4 rim = _NoiseRimColor * saturate(pow(1 - ((0.5 - rimNoise)), _NoiseRim)) * dot(i.scrPos - o, i.scrPos - o);
				
				rim *= -_NoiseRim / 3;

				if (_NoiseRim > -9)
					rim /= 1.3;
				if (_NoiseRim > -6)
					rim /= 3;
			//	rim = lerp(0, rim, abs(i.scrPos.x - 0.5) + _NoiseRim/15);
				rim.y += i.scrPos.x/6;
				rim.z += i.scrPos.y/6;
			

				



				// COLOR GRADING
				//colDistorted += tex2D(_ColorGrading, 1-i.uv.y);


				if (sqrPos  >_Radius)
					col = colDistorted;
				else if (sqrPos + _GradientOffset > _Radius)
				{
					float lerpValue = (sqrPos + _GradientOffset - _Radius) / _GradientOffset;
					col = lerp(colNormal,colDistorted , lerpValue);
				}
				else
					col = colNormal;

				
				return col;


				//fixed4 col = tex2Dproj(_BackgroundTexture, i.grabPos + _PixelOffset * normalize(noiseToDirection));



				//return  lerp(rim0, rim1, (cos(_Time.x * 20) + 1) / 2) * 5 + col;









				//fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				//	col.rgb = 1 - col.rgb;
				//return col;
			}
			ENDCG
		}
	}
}
