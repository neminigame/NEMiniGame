// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon"
{
	Properties{
		_Color("Color",Color)=(1,1,1,1)
		_MainTex("Main Tex",2D)="white"{}
		_RampTex("Ramp Tex",2D)="white"{}
		_light("light",Range(0.1,5))=1
		_shadowColor("shadow color",Color)=(0.1,0.1,0.1,0.1)
		_lightprobe("light probe",Range(0,10))=1
	}
	SubShader{
		Tags{"LightMode"="ForwardBase"  "RenderType"="Opaque"}
		Pass
		{
			
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
			half _light,_lightprobe;
			struct v2f{
				float4 pos:SV_POSITION;
				float3 worldnormal:TEXCOORD0;
				float3 worldpos:TEXCOORD1;
				half2 uv:TEXCOORD2;
				fixed3 SHlighting:Color;
				SHADOW_COORDS(4)
			};
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.worldpos=mul(unity_ObjectToWorld,v.vertex).xyz;
				o.worldnormal=UnityObjectToWorldNormal(v.normal);
				o.uv=TRANSFORM_TEX(v.texcoord,_MainTex);
				o.SHlighting=ShadeSH9(float4(o.worldnormal,1));
				o.SHlighting*=_lightprobe;
				TRANSFER_SHADOW(o);
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
				
				fixed3 ambient=albedo;
				//fixed diff=dot(worldnormal,worldlightdir)*0.5+0.5;
				//fixed3 diffuse=_LightColor0.rgb*albedo*tex2D(_RampTex,float2(diff,diff)).rgb;
				//UNITY_LIGHT_ATTENUATION(atten,i,i.worldpos);
				//diffuse=lerp(_shadowColor*diffuse,diffuse,atten);

				albedo*=i.SHlighting;
				return fixed4(albedo*_light,1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}