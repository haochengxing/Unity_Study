Shader "Custom/Unlit-Texture-Outline" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,0,1) //�����ɫ
        _Outline("Outline width", Range(0.0, 0.5)) = 0.03 // ��߿��
    }

    CGINCLUDE
    #include "UnityCG.cginc"
    struct appdata_t {
        float4 vertex : POSITION;
        float2 texcoord : TEXCOORD0;
        float3 normal : NORMAL;
    };

    struct v2f {
        float4 vertex : SV_POSITION;
        half2 texcoord : TEXCOORD0;
    };

    sampler2D _MainTex;
    float4 _MainTex_ST;
    float _Outline;
    float4 _OutlineColor;

    v2f vert(appdata_t v) //��һ��pass�Ķ���任������ͨ�任
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

        return o;
    }

    v2f vert_outline(appdata_t v) //�ڶ���Pass�Ķ���任������������
    {
        v2f o;
        // ��ʽһ���۲�ռ� ��������ƫ�ƶ���
        float4 viewPos = mul(UNITY_MATRIX_MV, v.vertex);
        //float3 viewNorm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        float3 viewNorm = mul(v.normal, (float3x3)UNITY_MATRIX_T_MV);
        float3 offset = normalize(viewNorm) * _Outline;
        viewPos.xyz += offset;
        o.vertex = mul(UNITY_MATRIX_P, viewPos);

        //��ʽ��������ռ� ��������ƫ�ƶ���
        //float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
        //float3 worldNormal = UnityObjectToWorldNormal(v.normal);
        //float3 offset = normalize(worldNormal) * _Outline;
        //worldPos.xyz += offset;
        //o.vertex = mul(UNITY_MATRIX_VP, worldPos);
        return o;
    }

    ENDCG

    SubShader{
        Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }

        Pass{ // ��������
            Stencil
            {
                Ref 1
                Comp Always  //Comp comparisonFunction������Ƚ���Ҫ������ǱȽϷ�ʽ��������Greater��GEqual��Equal�Ȱ��ֱȽϷ�ʽ�����������ͼ��
                Pass Replace //Pass stencilOperation������ǵ�stencil���Ժ���Ȳ��Զ�ͨ����ʱ�򣬽��е�stencilOperation����������ע���Ƕ�ͨ����ʱ��
                ZFail Replace //ZFail stencilOperation���������stencil����ͨ����������Ȳ���û��ͨ����ʱ��ִ�е�stencilOperation������
                //Fail stencilOperation���������stencil����ͨ����ʱ��ִ�е�stencilOperation����������ֻҪstencil����ͨ���Ϳ�����
            }

            ZTest LEqual 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord);
                return col;
            }

            ENDCG
        }

        Pass{ // �ڵ����ֻ������
            //ZTest Greater  //ֻ���Ʊ��ڵ��Ĳ���
            ZWrite Off //�ر����д��
            //Blend DstAlpha OneMinusDstAlpha

            Stencil{
                Ref 1
                Comp NotEqual

            }
            CGPROGRAM
            #pragma vertex vert_outline
            #pragma fragment frag
            half4 frag(v2f i) :COLOR
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
