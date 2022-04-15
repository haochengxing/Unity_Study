Shader "Unlit/depth_testing"
{
    Properties
    {
        _DepthFactor("Depth Factor", float) = 1.0
    }

    SubShader
    {
        Tags{"Queue"="Transparent" "RenderType"="Transparent"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 screenPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            float _DepthFactor;
            sampler2D _CameraDepthTexture;

            v2f vert(appdata_base v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //��������µ���Ļ����ֵ
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                //��Ļ�ռ�������Ϣ
                float depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
                //ת���������ֵ
                float depth = LinearEyeDepth(depthSample);
                float foamLine = 1 - saturate(_DepthFactor * (depth - i.screenPos.w));
                return foamLine;
                //float depth = Linear01Depth(depthSample);
                //return depth;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}