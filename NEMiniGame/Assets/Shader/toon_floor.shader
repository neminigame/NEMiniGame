// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon_floor"
{
	Properties{
		_Color("Color",Color)=(1,1,1,1)
		_MainTex("Main Tex",2D)="white"{}
		_RampTex("Ramp Tex",2D)="white"{}
		_light("light",Range(1,5))=1
		_shadowColor("shadow color",Color)=(0.1,0.1,0.1,0.1)
	}
	SubShader{

		Pass
		{
			Tags{"LightMode"="ForwardBase"  "RenderType"="Opaque"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#pragma multi_compile_fwdbase
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _RampTex;
			fixed4 _Color,_shadowColor;
			half _light;
			struct v2f{
				float4 pos:SV_POSITION;
				float3 worldnormal:TEXCOORD0;
				float3 worldpos:TEXCOORD1;
				float3 reflectionDir:TEXCOORD3;
				half2 uv:TEXCOORD2;
			};
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.worldpos=mul(unity_ObjectToWorld,v.vertex).xyz;
				o.worldnormal=UnityObjectToWorldNormal(v.normal);
				float3 worldViewDir = WorldSpaceViewDir(v.vertex);
				o.reflectionDir = reflect(-worldViewDir, o.worldnormal);
				o.uv=TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			fixed4 frag(v2f i):SV_Target
			{
				fixed3 worldnormal = normalize(i.worldnormal);
				fixed3 worldlightdir=normalize(UnityWorldSpaceLightDir(i.worldpos));
				fixed3 worldviewdir=normalize(UnityWorldSpaceViewDir(i.worldpos));
				fixed3 halfdir=normalize(worldlightdir+worldviewdir);
				fixed3 ccolor=tex2D(_MainTex,i.uv).rgb;
				fixed3 albedo=ccolor*_Color.rgb;
				fixed3 ambient=UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
				fixed diff=dot(worldnormal,worldlightdir)*0.5+0.5;
				fixed3 diffuse=_LightColor0.rgb*albedo*tex2D(_RampTex,float2(diff,diff)).rgb;
			float3 reflectDir = BoxProjectedCubemapDirection(i.reflectionDir, i.worldpos, unity_SpecCube0_ProbePosition, unity_SpecCube0_BoxMin, unity_SpecCube0_BoxMax);
	half4 rgbm = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, reflectDir);
				return fixed4((rgbm.rgb)*_light,1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}