Shader "MiniGame/Glitch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_strength("strength",Range(0,1))=0.1
		_speed("speed",Range(0,2))=0.4
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
			half _strength,_speed;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            sampler2D _MainTex;
            float4 _MainTex_ST;
			inline float random2d(float2 n) { 
				return frac(sin(dot(n, float2(12.9898, 4.1414))) * 43758.5453);
			}
			inline float insideRange(float v, float bottom, float top) {
				 return step(bottom, v) - step(top, v);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float time=_Time.y*50.*_speed;
				float3 outCol=tex2D(_MainTex, i.uv).rgb;
				float maxOffset = _strength/2.0;
				float Y = random2d(float2(time , 2000.0));
				float H = random2d(float2(time , 9000.0)) * 0.25;
				float offset = lerp(-maxOffset,maxOffset,random2d(float2(time,9000)));
				if (insideRange(i.uv.y, Y, frac(Y+H)) == 1.0 )
        			outCol = tex2D(_MainTex, float2(i.uv.x+offset,i.uv.y)).rgb;
                float maxColOffset = _strength/6.0;
				float randNum = random2d(float2(time , 9000.0));
				float2 colOffset = float2(lerp(-maxColOffset,maxColOffset,random2d(float2(time,9000))), 
								   lerp(-maxColOffset,maxColOffset,random2d(float2(time,7000))));

				if (randNum < 0.3) outCol.r = tex2D(_MainTex, i.uv + colOffset).r;
				else if (randNum < 0.6) outCol.g = tex2D(_MainTex, i.uv + colOffset).g;
				else outCol.b = tex2D(_MainTex, i.uv + colOffset).b;
                return fixed4(outCol,1.0);
            }
            ENDCG
        }
    }
}
