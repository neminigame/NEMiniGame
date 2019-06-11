// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/Screen"
{
    Properties
    {
        _MainTex      ("Base",          2D) = ""{}
        _StripeTex    ("Stripe",        2D) = ""{}
        _FlickerFreq  ("Flicker Freq",  Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D _StripeTex;
			float4 _StripeTex_ST;

			float _BaseLevel;
			float _FlickerFreq;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv0 = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv1 = TRANSFORM_TEX(v.texcoord, _StripeTex);
				return o;
			}

			fixed4 frag(v2f i) :SV_Target
			{
				fixed4 color = tex2D(_MainTex, i.uv0);
				half amp = tex2D(_StripeTex, i.uv1+_Time.y*3.0f).r+0.5f;
				half time = _Time.y * 3.14f * _FlickerFreq;
				half flicker=sin(time)*0.5f+0.8f;
				return color * (amp * flicker);
			}
            ENDCG
        }
    } 
    FallBack "Diffuse"
}
