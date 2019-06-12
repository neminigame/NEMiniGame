Shader "MiniGame/Line"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_speed("speed",Range(0,100))=10
		_Color("color",Color)=(1,1,1,1)
    }
    SubShader
    {
        Pass
        {
			Cull Off
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			half _speed;
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				fixed4 color:COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				fixed4 color:COLOR0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			half4 _Color;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color=v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, float2(i.uv.x-_Time.y*_speed,i.uv.y));
                return col*_Color*i.color;
            }
            ENDCG
        }
    }
	FallBack "Diffuse"
}
