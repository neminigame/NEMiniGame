// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable
// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MiniGame/toon_curtain"
{
//shadowgun模拟风吹旗帜
	Properties {
		_Color("Color",Color)=(1,1,1,1)
		_Wind("Wind params",Vector) = (1,1,1,1)
		_WindEdgeFlutter("Wind edge fultter factor", float) = 0.5
		_WindEdgeFlutterFreqScale("Wind edge fultter freq scale",float) = 0.5
	}

	SubShader {
		Tags {"Queue"="Transparent" "RenderType"="Transparent" "LightMode"="ForwardBase"}
		LOD 100
    
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off ZWrite Off
        CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
        #pragma target 3.0    
		sampler2D _MainTex;
		float4 _MainTex_ST;
		samplerCUBE _ReflTex;
		fixed4 _Color;
		float4 _Wind;
		float _WindEdgeFlutter;          
		float _WindEdgeFlutterFreqScale; 

		struct Input {
			float4 pos ;
			float2 uv ;
			fixed3 spec ;
		};
		inline float4 SmoothCurve( float4 x ) {
			return x * x *( 3.0 - 2.0 * x );
		}
		inline float4 TriangleWave( float4 x ) {
			return abs( frac( x + 0.5 ) * 2.0 - 1.0 );
		}
		inline float4 SmoothTriangleWave( float4 x ) {
			return SmoothCurve( TriangleWave( x ) );
		}
		inline float4 AnimateVertex2(float4 pos, float3 normal, float4 animParams,float4 wind,float2 time)
		{    
			// animParams stored in color
			// animParams.x = branch phase
			// animParams.y = edge flutter factor
			// animParams.z = primary factor
			// animParams.w = secondary factor

			float fDetailAmp = 0.1f;
			float fBranchAmp = 0.3f;
    
			// Phases (object, vertex, branch)
			float fObjPhase = dot(unity_ObjectToWorld[3].xyz, 1);  
			float fBranchPhase = fObjPhase + animParams.x;  
    
			float fVtxPhase = dot(pos.xyz, animParams.y + fBranchPhase);  
			// x is used for edges; y is used for branches
			float2 vWavesIn = time  + float2(fVtxPhase, fBranchPhase ); 
    
			// 1.975, 0.793, 0.375, 0.193 are good frequencies
			float4 vWaves = (frac( vWavesIn.xxyy * float4(1.975, 0.793, 0.375, 0.193) ) * 2.0 - 1.0);
    
			vWaves = SmoothTriangleWave( vWaves );  
			float2 vWavesSum = vWaves.xz + vWaves.yw;

			// Edge (xz) and branch bending (y)
			float3 bend = animParams.y * fDetailAmp * normal.xyz; 
			bend.y = animParams.w * fBranchAmp; 
			pos.xyz += ((vWavesSum.xyx * bend) + (wind.xyz * vWavesSum.y * animParams.w)) * wind.w;  

			// Primary bending
			// Displace position
			pos.xyz += animParams.z * wind.xyz;  
    
			return pos;
		}
		 void vert(inout appdata_full v,out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
        
			float4    wind;
        
			float    bendingFact = v.color.r;
        
			wind.xyz    = mul((float3x3)unity_WorldToObject,_Wind.xyz);      
			wind.w        = _Wind.w  * bendingFact;                       
        
        
			float4    windParams    = float4(0,_WindEdgeFlutter,bendingFact.xx);  
			float         windTime         = _Time.y * float2(_WindEdgeFlutterFreqScale,1);  
			float4    mdlPos            = AnimateVertex2(v.vertex,v.normal,windParams,wind,windTime); 
        
			v.vertex=mdlPos;
        
			o.spec = v.color;
		}
		void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 col =  _Color;
            o.Albedo = col.rgb ;
          
            o.Alpha = col.a ;
        }
        ENDCG 
	}  
}
