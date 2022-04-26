// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

Shader "Unlit/LightMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            

            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2:TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            // 光照贴图
		    // sampler2D unity_Lightmap ;
            // 光照贴图的UV变换(只需声明即可，启用光照贴图时自动赋值)
		    // float4 unity_LightmapST;

            v2f vert (appdata_full v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // 用宏计算出采样二维纹理UV的缩放和偏移量
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                // 计算光照贴图UV坐标的缩放量和偏移量
			    o.uv2 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 lm = DecodeLightmap (UNITY_SAMPLE_TEX2D(unity_Lightmap,i.uv2)) ;
                col.rgb *= lm;
                return col;
            }
            ENDCG
        }
    }
}