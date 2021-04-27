Shader "PIXO/PowertrainOutline" {
	
	 Properties {
      _MainTex ("Texture", 2D) = "cat-outline-texture" {}
      _OutlineColor ("Outline Color", Color) = (1,1,1,1)
	  _Outline ("Outline Width", Range (.002, 0.1)) = 0.01
	 }
    
   SubShader {
	  Tags { "Queue"="Transparent+1" }
	 
   	  ZWrite off
      CGPROGRAM
	      #pragma surface surf Lambert vertex:vert
	      struct Input {
	          float2 uv_MainTex;
	      };
	      float _Outline;
	      float4 _OutlineColor;
	      void vert (inout appdata_full v) {
	          v.vertex.xyz += v.normal * _Outline;
	      }
	      sampler2D _MainTex;
	      void surf (Input IN, inout SurfaceOutput o) 
	      {
	          o.Emission = _OutlineColor.rgb;
	      }
      ENDCG

      ZWrite on
      Cull Off 
      ZTest Off
      
      CGPROGRAM
	      #pragma surface surf Lambert
	      struct Input {
	          float2 uv_MainTex;
	      };

	      sampler2D _MainTex;
	      void surf (Input IN, inout SurfaceOutput o) {
	          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
	         // o.Emission = (1,1,1,.2);
	      }
      ENDCG

    } 
    Fallback "Diffuse"
}

