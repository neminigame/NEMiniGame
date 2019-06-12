// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon_alpha"
{
	Properties{
		_Color("Color",Color)=(1,1,1,1)

		_light("light",Range(1,5))=1
		_shadowColor("shadow color",Color)=(0.1,0.1,0.1,0.1)
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
		Pass
		{
			
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color,_shadowColor;
			half _light;
			struct v2f{
				float4 pos:SV_POSITION;
				float3 worldnormal:TEXCOORD0;
				float3 worldpos:TEXCOORD1;
				half2 uv:TEXCOORD2;
			};
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.worldpos=mul(unity_ObjectToWorld,v.vertex).xyz;
				o.worldnormal=UnityObjectToWorldNormal(v.normal);
				o.uv=TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			fixed4 frag(v2f i):SV_Target
			{
				fixed3 worldnormal = normalize(i.worldnormal);
				fixed3 albedo=_Color.rgb;
				fixed3 diffuse=albedo;

				return fixed4((diffuse),_Color.a);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}