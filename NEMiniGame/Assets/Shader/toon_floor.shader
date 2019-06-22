// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon_floor"
{
	Properties{
		_Color("Color",Color)=(1,1,1,1)
		_MainTex("Main Tex",2D)="white"{}
		_reflectPower("reflect power",Range(0,1))=1
		//_shadowColor("shadow color",Color)=(0.1,0.1,0.1,0.1)
	}
	SubShader{
		Tags{"LightMode"="ForwardBase"  "RenderType"="Opaque"}
		Pass
		{
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"
			#pragma multi_compile_fwdbase
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color,_shadowColor;
			half _reflectPower;
			struct a2v{
				float4 vertex:POSITION;
				float3 normal:NORMAL;
				half2 uv:TEXCOORD0;
				half2 uv2:TEXCOORD1;
			};
			struct v2f{
				float4 pos:SV_POSITION;
				float3 worldpos:TEXCOORD0;
				float3 reflectionDir:TEXCOORD1;
				half2 uv:TEXCOORD2;
				half2 uv2:TEXCOORD3;
				SHADOW_COORDS(4)
			};
			v2f vert(a2v v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.worldpos=mul(unity_ObjectToWorld,v.vertex).xyz;
				float3 worldnormal=UnityObjectToWorldNormal(v.normal);
				float3 worldViewDir = WorldSpaceViewDir(v.vertex);
				o.reflectionDir = reflect(-worldViewDir, worldnormal);
				o.uv=TRANSFORM_TEX(v.uv,_MainTex);
				o.uv2=v.uv2* unity_LightmapST.xy + unity_LightmapST.zw;
				TRANSFER_SHADOW(o);
				return o;
			}
			fixed4 frag(v2f i):SV_Target
			{
				fixed3 ccolor=tex2D(_MainTex,i.uv).rgb;
				fixed3 diffuse=ccolor*_Color.rgb;
				fixed3 lightmapColor=DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap, i.uv2)); 
				float3 reflectDir = BoxProjectedCubemapDirection(i.reflectionDir, i.worldpos, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
				half4 rgbm = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, reflectDir);
				diffuse=lerp(diffuse*lightmapColor,rgbm.rgb,_reflectPower);
				UNITY_LIGHT_ATTENUATION(atten,i,i.worldpos);
				diffuse=lerp(_shadowColor*diffuse,diffuse,atten);
				return fixed4(diffuse,1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}