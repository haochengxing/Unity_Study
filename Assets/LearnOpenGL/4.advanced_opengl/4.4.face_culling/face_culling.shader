// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/face_culling"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
		_Diffuse("Diffuse", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            //定义Tags
			Tags{ "RenderType" = "Opaque" }

			//Cull Off //双面显示

			Cull Back//（默认）剔除背面（不显示内表面）

			//Cull Front//剔除正面（不显示外表面）

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
			};
			//定义结构体：vertex shader阶段输出的内容
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
			};
 
			//定义顶点shader
			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//把法线转化到世界空间
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}
 
			//定义片元shader
			fixed4 frag(v2f i) : SV_Target
			{
				//归一化法线，即使在vert归一化也不行，从vert到frag阶段有差值处理，传入的法线方向并不是vertex shader直接传出的
				fixed3 worldNormal = normalize(i.worldNormal);
				//把光照方向归一化
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				//根据兰伯特模型计算像素的光照信息，小于0的部分理解为看不见，置为0
				fixed3 lambert = max(0.0, dot(worldNormal, worldLightDir));
				//最终输出颜色为lambert光强*材质diffuse颜色*光颜色
				fixed3 diffuse = lambert * _Diffuse.xyz * _LightColor0.xyz;
				return fixed4(diffuse, 1.0);
			}
 
			//使用vert函数和frag函数
			#pragma vertex vert
			#pragma fragment frag	
 
			ENDCG
        }
    }
}
