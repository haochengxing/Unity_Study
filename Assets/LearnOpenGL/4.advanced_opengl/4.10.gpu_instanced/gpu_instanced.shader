// Upgrade NOTE: upgraded instancing buffer 'MyProperties' to new syntax.

Shader "Unlit/gpu_instanced"{
	Properties{
		_Diffuse("Diffuse", Color) = (1,1,1,1)
	}

	SubShader{
		Pass{
			Tags {"LightMode" = "ForwardBase"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Lighting.cginc"
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"

			struct a2v {
				float4 vertex: POSITION;
				float3 normal: NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f {
				float4 pos: SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			UNITY_INSTANCING_BUFFER_START(MyProperties)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Diffuse)
#define _Diffuse_arr MyProperties
			UNITY_INSTANCING_BUFFER_END(MyProperties)

			v2f vert(a2v v) {
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize(i.worldNormal);
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				fixed3 diffuse = _LightColor0.rgb * UNITY_ACCESS_INSTANCED_PROP(_Diffuse_arr, _Diffuse).rgb * saturate(dot(worldNormal, worldLightDir));
				fixed3 color = ambient + diffuse;
				return fixed4(color, 1.0);
			}

			ENDCG
		}
	}
}
