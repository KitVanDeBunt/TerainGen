//Shader "Terain" {
//	Properties {
//		_Color ("Main Color", Color) = (1,1,1,1)
//		_MainTex ("Base (RGB)", 2D) = "white" {}
//		_BumpMap ("Bump (RGB) Illumin (A)", 2D) = "bump" {}
//	}
//	SubShader {
//		UsePass "Self-Illumin/VertexLit/BASE"
//		UsePass "Bumped Diffuse/PPL"
//	} 
//	FallBack "Diffuse"
//}

Shader "Custom/Terain_0.1" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _MapTex ("Map (R)", 2D) = "white" {}
        _MapCount ("Map count", Float) = 4.0
        //_texCount ("")
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;

            fixed4 frag(v2f_img i) : SV_Target {
            	//float lightVector = float3(unity_LightPosition.xyz);
            	//float pointOrDirLight = unity_LightPosition.w;
            	
            	// Directiornal Light
            	
            	//float3 directiornalLight = ;
            	
            	// point light
            	//float3 pointLight = float3(0,0,1); 
            	
            	
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}



