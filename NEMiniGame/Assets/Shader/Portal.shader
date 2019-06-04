Shader "MiniGame/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_MaskTex("MaskTex", 2D) = "white" {}
		_NoiseTex("NoiseTex",2D) = "white" {}
		_AlphaClip("AlphaClip",2D) = "white" {}
		_GlowMask("_GlowMask",2D) = "white" {}
		_DistortFactor("DistortFactor",Range(0,100)) = 1
		_NoiseFactor("NoiseFactor",Range(0,100)) = 1
		_Speed("Speed",Range(0,100))=32
		_Color1("Color1",Color)=(1,1,1,1)
		_Color2("Color2",Color) = (1,1,1,1)
<<<<<<< HEAD
		_Force("Force",Range(0,1.85))=1
		_PortalOffset("PortalOffset",Range(0,5))=1
=======
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
    }
    SubShader
    {
        Tags { "Queue"="AlphaTest"  "IgnoreProjector"="True ""RenderType"="TransparentCutout" }
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
<<<<<<< HEAD
				float3 normal : NORMAL;
=======
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
<<<<<<< HEAD
				UNITY_FOG_COORDS(1)
				float3 normal : TEXCOORD2;
				float3 worldNormal:TEXCOORD3;
				float3 worldPos : TEXCOORD4;
=======
                UNITY_FOG_COORDS(1)
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _MaskTex;
			float4 _MaskTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			sampler2D _AlphaClip;
			float4 _AlphaClip_ST;
			sampler2D _GlowMask;
			float4 _GlowMask_ST;
			float _DistortFactor;
			float _NoiseFactor;
			float _Speed;
<<<<<<< HEAD
			float _Force;
			float _PortalOffset;
=======
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
			fixed4 _Color1;
			fixed4 _Color2;
			

            v2f vert (appdata v)
            {
                v2f o;
<<<<<<< HEAD

				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float4 pos = mul(UNITY_MATRIX_M, v.vertex);
				float3 normal = UnityObjectToWorldNormal(v.normal);
				float t = length(v.uv-0.5) / sqrt(0.5);
				pos = pos + float4(normal,0)*(pow((2-_Force),t) + _Force - 1 - _PortalOffset);
				o.vertex = mul(UNITY_MATRIX_VP, pos);


				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
=======
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
				UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

			float2 remapCoord(float2 uv)
			{
				uv.x = uv.x - 0.5;
				uv.y = uv.y - 0.5;
				float r = sqrt(uv.x * uv.x + uv.y * uv.y);
				float theta = 0;
				theta = atan2(uv.y, uv.x);
				uv.y = r;
				uv.x = theta;
				return  uv;
			}

            fixed4 frag (v2f i) : SV_Target
            {
				//i.uv = remapCoord(i.uv);
				//i.uv.y += _Time* _DistortFactor;
				//i.uv.x -= _Time* _NoiseFactor;
				//i.uv.x += tex2D(_MaskTex, i.uv).x;
    //            fixed4 col = tex2D(_MainTex, i.uv);
    //            // apply fog
    //            UNITY_APPLY_FOG(i.fogCoord, col);
    //            return col;

<<<<<<< HEAD
				//worldPos.xyz += worldNormal * _Force;
=======
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
				float2 uv = i.uv;
				i.uv += -0.5 + tex2D(_MainTex, uv)/3;
				float r = sqrt(i.uv.x * i.uv.x + i.uv.y * i.uv.y);
				float theta = 0;
				theta = atan2(i.uv.y, i.uv.x);
				float a = sin(r*(-3.14159265358979323)*_DistortFactor + theta * _NoiseFactor + _Time * _Speed);
				a = saturate(tex2D(_MainTex, float2(theta, r)) + a);
				fixed4 col = lerp(_Color1,_Color2,a);
				clip (tex2D(_AlphaClip, uv).x - 0.5);
				col = lerp(col, tex2D(_GlowMask, uv), tex2D(_GlowMask, uv).x);
<<<<<<< HEAD
				//return _Force;
=======
>>>>>>> 205209e0d2537f1090c8be8688fdb1b23f15fdd2
				return fixed4(col.xyz, 1.0);
				////将uv原点放到屏幕中心
				//float2 dir = i.uv - float2(0.5, 0.5);
				////获取中心点到像素的向量
				//float2 dir1 = i.uv - float2(0.5, 0.5);
				////旋转角度=3.14/180再处以向量长度（越远旋转越小）
				//float rot = _DistortFactor * 0.1745 / (length(dir1) + 0.1);
				//float sinValue;
				//float cosValue;
				////输入角度输出sin和cos值
				//sincos(rot, sinValue, cosValue);
				////旋转矩阵
				//float2x2 rotMatrix = float2x2(cosValue, -sinValue, sinValue, cosValue);
				////绕uv原点（现在在屏幕中心）旋转（行向量）
				//dir = mul(dir, rotMatrix);
				////将uv原点返回uv原点
				//dir += float2(0.5, 0.5);
				//float4 noise = tex2D(_NoiseTex, i.uv);
				////噪声扰动
				//float2 noiseOffset = (noise.xy - float2(0.5, 0.5)) * 2 * _NoiseFactor*dir1;
				////已经旋转过的uv和噪声偏移
				//float2 uv = dir + noiseOffset;
				//return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
