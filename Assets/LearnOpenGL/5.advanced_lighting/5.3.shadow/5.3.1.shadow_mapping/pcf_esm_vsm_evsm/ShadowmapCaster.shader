Shader "Unlit/ShadowmapCaster"
{
    
    SubShader
    {
        

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 depth : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform fixed _Cutoff;
            uniform float esmNormalBias;
            uniform fixed4 _Color;


            v2f vert (appdata v)
            {
                v2f o;
                float4 wpos = mul(unity_ObjectToWorld,v.vertex);
                wpos.xyz -= UnityObjectToWorldNormal(v.normal).xyz*esmNormalBias;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.depth = o.vertex.zw;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                clip(col.a*_Color.a-_Cutoff);
                return i.depth.x/i.depth.y;
            }
            ENDCG
        }
    }
}
