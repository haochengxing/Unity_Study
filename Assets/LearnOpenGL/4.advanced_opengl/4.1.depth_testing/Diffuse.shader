// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Diffuse"
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

		Pass{
			ZWrite On//�������д��
			ColorMask 0//�ر���ɫ���
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
            //����Tags
			Tags{ "RenderType" = "Opaque" }

			ZWrite Off//�ر����д��
			ZTest Equal//������Ϊͨ��

			//ZWrite On
			//ZTest Less
			//ZTest Always
 
			CGPROGRAM
			//����ͷ�ļ�
			#include "Lighting.cginc"
			//����Properties�еı���
			fixed4 _Diffuse;
			//����ṹ�壺Ӧ�ý׶ε�vertex shader�׶ε�����
			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};
			//����ṹ�壺vertex shader�׶����������
			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
			};
 
			//���嶥��shader
			v2f vert(a2v v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				//�ѷ���ת��������ռ�
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}
 
			//����ƬԪshader
			fixed4 frag(v2f i) : SV_Target
			{
				//��һ�����ߣ���ʹ��vert��һ��Ҳ���У���vert��frag�׶��в�ֵ����������ķ��߷��򲢲���vertex shaderֱ�Ӵ�����
				fixed3 worldNormal = normalize(i.worldNormal);
				//�ѹ��շ����һ��
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				//����������ģ�ͼ������صĹ�����Ϣ��С��0�Ĳ�������Ϊ����������Ϊ0
				fixed3 lambert = max(0.0, dot(worldNormal, worldLightDir));
				//���������ɫΪlambert��ǿ*����diffuse��ɫ*����ɫ
				fixed3 diffuse = lambert * _Diffuse.xyz * _LightColor0.xyz;
				return fixed4(diffuse, 1.0);
			}
 
			//ʹ��vert������frag����
			#pragma vertex vert
			#pragma fragment frag	
 
			ENDCG
        }
    }
}