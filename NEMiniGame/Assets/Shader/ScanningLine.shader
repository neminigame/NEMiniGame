Shader "MiniGame/ScanningLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_TransitionTex("TransitionTex",2D) = "white" {}
		_Color("Screen Color",Color)=(0,0,0,1)
		_Cutoff("Cutoff",Range(0,1))=0
		[MaterialToggle] _Distort("Distort",Float)=0
		_Fade("Fade",Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 uv1:TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _TransitionTex;
			float4 _TransitionTex_ST;
			fixed4 _Color;
			float _Cutoff;
			float _Distort;
			float _Fade;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv1 = TRANSFORM_TEX(v.uv, _MainTex);
				#if UNITY_UV_START_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					o.uv1.y = 1 - o.uv1.y;
				#endif
				return o;
            }

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 transit = tex2D(_TransitionTex, i.uv1);

				fixed2 direction = float2(0, 0);
				if (_Distort)
					direction = normalize(float2((transit.r - 0.5) * 2, 0));

				fixed4 col = tex2D(_MainTex, i.uv + _Cutoff * direction);

				if (transit.b < _Cutoff)
					return col = lerp(col, _Color, _Fade);
				return col;
			}
            ENDCG
        }
    }
}
