Shader "Custom/Unlit-Texture-Outline" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,0,1) //描边颜色
        _Outline("Outline width", Range(0.0, 0.5)) = 0.03 // 描边宽度
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

    v2f vert(appdata_t v) //第一个pass的顶点变换就是普通变换
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

        return o;
    }

    v2f vert_outline(appdata_t v) //第二个Pass的顶点变换，做法线扩张
    {
        v2f o;
        // 方式一，观察空间 下往法线偏移顶点
        float4 viewPos = mul(UNITY_MATRIX_MV, v.vertex);
        //float3 viewNorm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        float3 viewNorm = mul(v.normal, (float3x3)UNITY_MATRIX_T_MV);
        float3 offset = normalize(viewNorm) * _Outline;
        viewPos.xyz += offset;
        o.vertex = mul(UNITY_MATRIX_P, viewPos);

        //方式二，世界空间 下往法线偏移顶点
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

        Pass{ // 正常绘制
            Stencil
            {
                Ref 1
                Comp Always  //Comp comparisonFunction，这个比较重要，这个是比较方式。大致有Greater，GEqual，Equal等八种比较方式，具体待会列图。
                Pass Replace //Pass stencilOperation，这个是当stencil测试和深度测试都通过的时候，进行的stencilOperation操作方法。注意是都通过的时候！
                ZFail Replace //ZFail stencilOperation，这个是在stencil测试通过，但是深度测试没有通过的时候执行的stencilOperation方法。
                //Fail stencilOperation，这个是在stencil测试通过的时候执行的stencilOperation方法。这里只要stencil测试通过就可以了
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

        Pass{ // 遮挡部分绘制描边
            //ZTest Greater  //只绘制被遮挡的部分
            ZWrite Off //关闭深度写入
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
