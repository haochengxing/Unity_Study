Shader "Unlit/framebuffers"
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float offset = 1.0/300.0; 

                float2 offsets[9];
                offsets[0] = float2(-offset,  offset); // 左上
                offsets[1] = float2( 0.0f,    offset); // 正上
                offsets[2] = float2( offset,  offset); // 右上
                offsets[3] = float2(-offset,  0.0f);   // 左
                offsets[4] = float2( 0.0f,    0.0f);   // 中
                offsets[5] = float2( offset,  0.0f);   // 右
                offsets[6] = float2(-offset, -offset); // 左下
                offsets[7] = float2( 0.0f,   -offset); // 正下
                offsets[8] = float2( offset, -offset);  // 右下

                //锐化
                /*float kernel[9] = {
                    -1, -1, -1,
                    -1,  9, -1,
                    -1, -1, -1
                };*/

                //模糊
                /*float kernel[9] = {
                    1.0 / 16, 2.0 / 16, 1.0 / 16,
                    2.0 / 16, 4.0 / 16, 2.0 / 16,
                    1.0 / 16, 2.0 / 16, 1.0 / 16 
                };*/

                //边缘检测
                float kernel[9] = {
                    1, 1, 1,
                    1, -8, 1,
                    1, 1, 1
                };

                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return fixed4(fixed3(1.0-col.rgb),1.0);//反相

                //float average = 0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
                //return fixed4(average, average, average, 1.0);//灰度

                float3 sampleTex[9];
                for(int j = 0; j < 9; j++)
                {
                    sampleTex[j] = tex2D(_MainTex, i.uv+offsets[j]);
                }
                float3 col = float3(0,0,0);
                for(int i = 0; i < 9; i++)
                    col += sampleTex[i] * kernel[i];
                return fixed4(col,1.0);
            }
            ENDCG
        }
    }
}
