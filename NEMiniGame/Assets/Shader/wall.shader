// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "MiniGame/wall"
{
	Properties
	{
		_Center ("Center", Vector) = (0.0, 0.0, 0.0)
		_Color("Color",Color)=(1,1,1,1)
		_GridColor ("GridColor", Color) = (0.2, 0.3, 0.5)
		_GridEmission ("GridEmission", Float) = 8.0
		_speed("speed",Range(0,50))=1
		_width("width",Range(0,10))=1
		[Enum(xy,0,xz,1,yz,2)]_dir("dir",float)=0 //此shader为单平面计算特效，根据想要显示那个平面的世界坐标的轴向来确定。
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			float3 _Center;
			float4 _GridColor,_Color;
			float _GridEmission;
			half _speed,_width,_dir;
			float2 mod(float2 a, float2 b) { return a - b * floor(a/b); }
			float Circle(float2 pos)
			{
				float dis = length(pos);
				float c=0;
				if(dis<_speed&&dis>_speed-_width)
					c=(dis-_speed)/_width+1;
				return c;
			}
			float Hex( float2 p, float2 h )
			{
				float2 q = abs(p);
				return max(q.x-h.y,max(q.x+q.y*0.57735,q.y*1.1547)-h.x);
			}

			float HexGrid(float2 p)
			{
				float scale = 1.2;
				float2 grid = float2(0.692, 0.4) * scale;
				float radius = 0.22 * scale;

				float2 p1 = mod(p, grid) - grid*0.5;
				float c1 = Hex(p1, radius);

				float2 p2 = mod(p+grid*0.5, grid) - grid*0.5;
				float c2 = Hex(p2, radius);
				return min(c1, c2);
			}
			 struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos:TEXCOORD1;
			};
			v2f vert(appdata v)
			{
				v2f o;
				o.worldPos=mul(unity_ObjectToWorld,v.vertex).xyz;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			fixed4 frag(v2f i):SV_Target
			{
				
				float3 center = i.worldPos - _Center;
			
				float2 pp;
				if(_dir==1) pp=center.xz;
				else if(_dir==2)pp=center.yz;
				else pp=center.xy;

				float grid = HexGrid(pp) > 0.0 ? 1.0 : 0.0;
				float circle = Circle(pp);

				float3 color= _GridColor * grid *circle* _GridEmission;
				return fixed4(color,1.0);
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
