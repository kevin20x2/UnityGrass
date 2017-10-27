// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/grassShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AlphaTex("alpha",2D) = "white" {}
		_Width ("width", float)=0.05 
		_Height ("height",float) = 3.0
	}
	SubShader {
		Tags { "Queue" = "AlphaTest" "RenderType" = "transparentCutout" "IgnoreProjector" = "True"}
		Pass{
		Cull Off
		Tags { "Lightmode" = "ForwardBase"}
		AlphaToMask On
		CGPROGRAM
		#pragma target 4.0
		#pragma vertex vert
		#pragma geometry geom

		#pragma fragment frag
		#include "UnityCG.cginc"
		struct a2v {
		 float4 vertex : POSITION;
		 float4 texcoord : TEXCOORD0;

		};
		struct v2g
		{
			float4 vertex : POSITION;
		//	float2 uv :TEXCOORD0;

		};
		struct g2f
		{
			float4 pos: SV_POSITION;
			float2 uv : TEXCOORD0;
			float3 norm : NORMAL;
		
		};

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		float _Width;
		float _Height;

		v2g vert(a2v v)
		{
			v2g o;
			o.vertex = v.vertex;
		//	o.uv = v.texcoord.xy;
			return o;
		}
		g2f init()
		{
			g2f output;
			output.pos = float4(0, 0, 0, 0);
			output.norm = float3(0, 0, 0);
			output.uv = float2(0, 0);
			return output;

		}
		[maxvertexcount(30)]
		void geom(point v2g points[1], inout TriangleStream<g2f> triStream)
		{
			float4 root = points[0].vertex;
			float random = sin(UNITY_HALF_PI * frac(root.x) + UNITY_HALF_PI*frac(root.z));
			_Width = _Width + (random / 50);
			_Height = _Height + (random / 5);
			const unsigned int vertexcount = 12;
			g2f v[12] = { init(),init(),init(),
			init(),init(),init(),
			init(),init(),init(),
			init(),init(),init()};
			float currentV = 0.0f;
			float currentVertexHeight = 0.0f;
			for (uint i = 0; i < vertexcount; ++i)
			{
				v[i].norm = float3(0, 0, 1);
				if (fmod(i, 2) == 0)
				{
					v[i].pos = float4(root.x - _Width, root.y + currentVertexHeight , root.z, 1);
					v[i].uv = float2(0, currentV);
					
				}
				else
				{
					v[i].pos = float4(root.x + _Width, root.y + currentVertexHeight, root.z, 1);
					v[i].uv = float2(1, currentV);
					currentV += 2.0f/(vertexcount-2);
					currentVertexHeight = currentV *_Height;
				}

				v[i].pos = UnityObjectToClipPos(v[i].pos);
			}
			for (uint j = 0; j < (vertexcount - 2); ++j)
			{
				triStream.Append(v[j]);
				triStream.Append(v[j + 2]);
				triStream.Append(v[j + 1]);
			}


		}

		float4 frag(g2f i): COLOR
		{
			fixed4 color = tex2D(_MainTex,i.uv);
			fixed4 alpha = tex2D(_AlphaTex, i.uv);
			return float4(color.rgb, alpha.r);
		}




		ENDCG
			}
	}
	FallBack "Diffuse"
}
