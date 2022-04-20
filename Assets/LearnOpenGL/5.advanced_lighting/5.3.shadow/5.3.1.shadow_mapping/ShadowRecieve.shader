Shader "Shadow/ShadowRecieve"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

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
				float2 shaodwUv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 shadowPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _ShadowTexture;
			float4x4 _ShadowLauncherMatrix;
			float3 _ShadowLauncherParam;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float4 shadowPos = mul(_ShadowLauncherMatrix, worldPos);
				shadowPos.xy = (shadowPos.xy / _ShadowLauncherParam.x + 1) / 2;
				shadowPos.z = (shadowPos.z / shadowPos.w - _ShadowLauncherParam.y)  / (_ShadowLauncherParam.z - _ShadowLauncherParam.y);
				o.shadowPos = shadowPos;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 shadow = tex2Dproj(_ShadowTexture, i.shadowPos);

				fixed shadowAlpha = shadow.r;

				fixed2 clipAlpha = saturate((0.5 - abs(i.shadowPos.xy - 0.5)) * 20);//阴影区域边缘过渡，如无需求可删除
				shadowAlpha *= clipAlpha.x * clipAlpha.y;

				fixed depth = 1 - UNITY_SAMPLE_DEPTH(shadow);//处理自投影和投影遮挡，如无需求可删除
				shadowAlpha *= step(depth,i.shadowPos.z);
				col.rgb *= 1 - shadowAlpha;

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}


	// GL
    // x = 1-far/near
    // y = far/near
    // z = x/far
    // w = y/far
    // DX UNITY_REVERSED_Z
    // x = -1+far/near
    // y = 1
    // z = x/far
    // w = 1/far
    // Linear01Depth: 1.0 / (_ZBufferParams.x * z + _ZBufferParams.y);
	// LinearEyeDepth: 1.0 / (_ZBufferParams.z * z + _ZBufferParams.w);