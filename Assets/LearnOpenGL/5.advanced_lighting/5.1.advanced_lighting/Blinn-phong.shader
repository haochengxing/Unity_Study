Shader "Unlit/Blinn-phong"
{
    Properties
    {
        _LightColor("LightColor",Color) = (1,1,1)
        _Power("Power",Range(1,256)) = 1
        _SpecularColor("SpecularColor",Color) = (1,1,1)
        _DiffuseColor("DiffuseColor",Color) = (1,1,1,1)
        
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

            struct appdata
            {
                float4 vertex : POSITION;
                
                float3 normal:NORMAL;
            };

            struct v2f
            {
                
                float4 vertex : SV_POSITION;
                float3 normal:NORMAL;

                float3 viewDir:TEXCOORD;
  
            };

  
            
            float3 _LightColor;
            float _Power;
            float3 _SpecularColor;
            float3 _DiffuseColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
            
                half3 _LightDir = normalize(_WorldSpaceLightPos0.xyz);
                half3 h = normalize (_LightDir + i.viewDir);
                fixed diffFactor = max (0, dot (i.normal, _LightDir));
                float nh = max (0, dot (i.normal, h));
                float specularFactor = pow (nh, _Power);
                fixed3 diffuse =  (diffFactor * _DiffuseColor + specularFactor * _SpecularColor) * _LightColor;
                return fixed4(diffuse,1);
            }
            ENDCG
        }
    }
}

