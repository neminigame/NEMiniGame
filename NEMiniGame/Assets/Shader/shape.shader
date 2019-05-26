Shader "MiniGame/shape"
{
    Properties
    {
		_clipangle("clip angle",Range(0,1))=0
		_radius("radius",Range(0,1))=0
		_Color("Color",Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
			ZWrite Off
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
			
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			half _clipangle,_radius;
			half4 _Color;
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

            sampler2D _RampTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
				i.uv.xy=i.uv.xy*2-1;
				float ang=atan2(i.uv.y,i.uv.x)/(3.1415926);
				float t=abs(ang)-_clipangle;
				float d=length(i.uv);
                if(t<0||d>_radius)
					discard;
                return fixed4(_Color.rgb,_Color.a*(1.-d));
            }
            ENDCG
        }
    }
}
