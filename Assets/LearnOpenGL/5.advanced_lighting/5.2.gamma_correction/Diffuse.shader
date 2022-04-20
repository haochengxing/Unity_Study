// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Diffuse"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Diffuse("Diffuse", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

		Pass{
			ZWrite On//开启深度写入
			ColorMask 0//关闭颜色输出
			CGPROGRAM
			struct a2v
			{
				float4 vertex : POSITION;
			};
			struct v2f
			{
				float4 pos : SV_POSITION;
			};
			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				return fixed4(1.0,1.0,1.0, 1.0);
			}
			#pragma vertex vert
			#pragma fragment frag	
			ENDCG
		}

        Pass
        {
            //定义Tags
			Tags{ "RenderType" = "Opaque" }

			ZWrite Off//关闭深度写入
			ZTest Equal//深度相等为通过

			//ZWrite On
			//ZTest Less
			//ZTest Always
 
			CGPROGRAM
			//引入头文件
			#include "Lighting.cginc"
			//定义Properties中的变量
			fixed4 _Diffuse;
			//定义结构体：应用阶段到vertex shader阶段的数据
			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};
			//定义结构体：vertex shader阶段输出的内容
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};
			sampler2D _MainTex;
            float4 _MainTex_ST;
 
			//定义顶点shader
			v2f vert(a2v v)
			{
				v2f o;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.pos = UnityObjectToClipPos(v.vertex);
				//把法线转化到世界空间
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}
 
			//定义片元shader
			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				//归一化法线，即使在vert归一化也不行，从vert到frag阶段有差值处理，传入的法线方向并不是vertex shader直接传出的
				fixed3 worldNormal = normalize(i.worldNormal);
				//把光照方向归一化
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				//根据兰伯特模型计算像素的光照信息，小于0的部分理解为看不见，置为0
				fixed3 lambert = max(0.0, dot(worldNormal, worldLightDir));
				//最终输出颜色为lambert光强*材质diffuse颜色*光颜色
				fixed3 diffuse = lambert * _Diffuse.xyz * _LightColor0.xyz*col;
		
				//float3 diffuseCol = pow(diffuse, 2.2);//转到线性空间

				float3 diffuseCol = pow(diffuse, 1.0/2.2);//gamma校正

				return fixed4(diffuseCol, 1.0);
			}
 
			//使用vert函数和frag函数
			#pragma vertex vert
			#pragma fragment frag	
 
			ENDCG
        }
    }
}
