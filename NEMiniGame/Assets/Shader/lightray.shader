// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/ray"
{
    Properties
    {
        _Color      ("Base Color", Color)    = (1, 1, 1, 1)
        _MainTex    ("Gradient Texture", 2D) = ""{}
        _NoiseTex1  ("Noise Texture 1", 2D)  = ""{}
        _NoiseSpeed ("Noise Speed", Vector)  = (0.1, 0.1, 0.1, 0.1)
		_Strength("Strength",Range(0.1,3))=1
    }
  
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f
			{
				float4 position : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float4 world_position : TEXCOORD3;
				float3 normal : TEXCOORD4;
			};
			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTex1;
			float4 _NoiseSpeed;
			half _Strength;
			v2f vert(appdata_base v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv0 = TRANSFORM_TEX(v.texcoord, _MainTex);
				float4 wp = mul(unity_ObjectToWorld, v.vertex);
				o.world_position = v.vertex;
				o.normal = normalize(mul(unity_ObjectToWorld, float4(v.normal.xyz,0.0)));

				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float3 normal = i.normal;
				float3 camDir = normalize(i.world_position - _WorldSpaceCameraPos);
				float falloff = max(abs(dot(camDir, normal)), 0.0);
				float4 c = _Color;
				float n1 = tex2D(_NoiseTex1, i.uv0+_Time.y *_NoiseSpeed.xy).r;
				c.a *= tex2D(_MainTex, i.uv0).a * n1 * falloff*_Strength;
				c.rgb*=1;
				return c;
			}
			ENDCG
        }
    } 
    FallBack "Diffuse"
}
