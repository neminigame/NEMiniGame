// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon_lightmap"
{
	Properties{
		_Color("Color",Color)=(1,1,1,1)
		_MainTex("Main Tex",2D)="white"{}
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
	}
	SubShader{
		Tags{ "LightMode"="ForwardBase" "RenderType"="Opaque"}
		Cull [_Cull] 
		Pass
		{
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			struct a2v{
				float4 vertex:POSITION;
				half2 uv:TEXCOORD0;
				half2 uv2:TEXCOORD1;
			};
			struct v2f{
				float4 pos:SV_POSITION;
				float3 worldpos:TEXCOORD1;
				half2 uv:TEXCOORD2;
				half2 uv2:TEXCOORD5;
			};
			v2f vert(a2v v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.worldpos=mul(unity_ObjectToWorld,v.vertex).xyz;
				o.uv=TRANSFORM_TEX(v.uv,_MainTex);
				o.uv2=v.uv2.xy*unity_LightmapST.xy + unity_LightmapST.zw;
				return o;
			}
			fixed4 frag(v2f i):SV_Target
			{
				fixed3 ccolor=tex2D(_MainTex,i.uv).rgb;
				fixed3 col=DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2)); 
				return fixed4(col*_Color.rgb*ccolor,1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}