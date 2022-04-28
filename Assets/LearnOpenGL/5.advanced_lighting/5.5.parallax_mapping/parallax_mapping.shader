Shader "Unlit/parallax_mapping"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Texture", 2D) = "white" {}
        [NoScaleOffset]_NormalMap("Normal Map",2D) = "bump" {}
        [NoScaleOffset]_DepthMap("Depth Map",2D) = "white" {}
        _SpecularColor("Specular Color",Color) = (1,1,1,1)
        _Gloss("Gloss",Range(32,256))=64
        _HeightScale("Height Scale",Range(0,1))=0.1
        _MaxLayerNum("Max Layer Num",Range(0,200))=50
        _MinLayerNum("Min Layer Num",Range(0,100))=30
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        

        Pass
        {
            Tags{"LightMode"="ForwardBase"}


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Lighting.cginc"

            #include "UnityCG.cginc"



            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 lightDir_tangent : TEXCOORD1;
                float3 viewDir_tangent : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            sampler2D _DepthMap;
            fixed4 _SpecularColor;
            float _Gloss;
            float _HeightScale;
            float _MaxLayerNum;
            float _MinLayerNum;

            float2 ParallaxMapping(float2 uv,float3 viewDir_tangent){
                float layerNum = lerp(_MaxLayerNum,_MinLayerNum,abs(dot(float3(0,0,1),viewDir_tangent)));
                float layerDepth = 1.0/layerNum;
                float currentLayerDepth = 0.0;
                float2 deltaTexCoords = viewDir_tangent.xy/viewDir_tangent.z*_HeightScale/layerNum;

                float2 currentTexCoords = uv;
                float currentDepthMapValue = tex2D(_DepthMap,currentTexCoords).r;
                while(currentLayerDepth<currentDepthMapValue){
                    currentTexCoords-=deltaTexCoords;
                    currentDepthMapValue = tex2Dlod(_DepthMap,float4(currentTexCoords,0,0)).r;
                    currentLayerDepth += layerDepth;
                }

                float2 prevTexCoords = currentTexCoords+deltaTexCoords;
                float prevLayerDepth = currentLayerDepth-layerDepth;

                float afterDepth = currentDepthMapValue-currentDepthMapValue;
                float beforeDepth = tex2D(_DepthMap,prevTexCoords).r - prevLayerDepth;
                float weight = afterDepth/(afterDepth-beforeDepth);
                float2 finalTexCoords = prevTexCoords*weight+currentTexCoords*(1.0-weight);

                return finalTexCoords;
            }

            v2f vert (appdata_tan v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                
                TANGENT_SPACE_ROTATION;
                o.lightDir_tangent = normalize(mul(rotation,ObjSpaceLightDir(v.vertex)));
                o.viewDir_tangent = normalize(mul(rotation,ObjSpaceViewDir(v.vertex)));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightDir = normalize(i.lightDir_tangent);
                float3 viewDir = normalize(i.viewDir_tangent);

                float2 uv = ParallaxMapping(i.uv,viewDir);
                if (uv.x>1.0 || uv.y>1.0 || uv.x<0.0 || uv.y<0.0){
                    discard;
                }

                float3 normal = normalize(UnpackNormal(tex2D(_NormalMap,uv)));
                fixed4 albedo = tex2D(_MainTex,uv);

                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz*albedo;
                fixed3 diffuse = _LightColor0.rgb*albedo.rgb*saturate(dot(normal,i.lightDir_tangent));
                float3 halfDir = normalize(lightDir+viewDir);
                fixed3 specular = _LightColor0.rgb*_SpecularColor.rgb*pow(saturate(dot(halfDir,normal)),_Gloss);

                fixed4 finalColor = fixed4(ambient+diffuse+specular,1.0);
                return finalColor;
            }
            ENDCG
        }
    }
}
