Shader "Unlit/deferred"
{
    Properties
    {

    }
    SubShader
    {
		ZWrite Off
		//LDR Blend DstColor Zero    HDR : Blend One One
		Blend one one
        Pass
        {

            CGPROGRAM
			#pragma target 3.0
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile_lightpass
			//代表排除不支持MRT的硬件
			#pragma exclude_renderers norm
			//#pragma multi_compile __ UNITY_HDR_ON

			#include "UnityCG.cginc"
			#include "UnityDeferredLibrary.cginc"
			#include "UnityGBuffer.cginc"

			sampler2D _CameraGBufferTexture0;
			sampler2D _CameraGBufferTexture1;
			sampler2D _CameraGBufferTexture2;

			struct a2v
			{
				float4 vertex : POSITION;
				float3 normal :NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 uv :TEXCOORD0;
				float3 ray : TEXCOORD1;
			};

			v2f vert(a2v i)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = ComputeScreenPos(o.pos);
				o.ray = UnityObjectToViewPos(i.vertex);
				//_LightAsQuad  当在处理四边形时，也就是直射光时返回1，否则返回0
				o.ray = lerp(o.ray, i.normal, _LightAsQuad);
				return o;
			}


			fixed4 frag(v2f i) : SV_Target
			{
				float3 worldPos;
				float2 uv;
				half3 lightDir;
				float atten;
				float fadeDist;
				UnityDeferredCalculateLightParams(i,worldPos,uv,lightDir, atten,fadeDist);


				half3 lightColor = _LightColor.rgb * atten;

				half4 gbuffer0 = tex2D(_CameraGBufferTexture0, uv);
				half4 gbuffer1 = tex2D(_CameraGBufferTexture1, uv);
				half4 gbuffer2 = tex2D(_CameraGBufferTexture2, uv);

				half3 diffuseColor = gbuffer0.rgb;
				half3 specularColor = gbuffer1.rgb;
				float gloss = gbuffer1.a * 50;
				float3 worldNormal = normalize(gbuffer2.xyz * 2 - 1);

				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				fixed3 halfDir = normalize(lightDir + viewDir);

				half3 diffuse = lightColor * diffuseColor * max(0,dot(worldNormal,lightDir));
				half3 specular = lightColor * specularColor * pow(max(0,dot(worldNormal,halfDir)),gloss);

				half4 color = float4(diffuse + specular,1);
				
				return color;
				
			}
            ENDCG
        }

		
		//转码pass，主要是对于LDR转码
		Pass 
		{
			ZTest Always
			Cull Off
			ZWrite Off
			Stencil
			{
				Ref[_StencilNonBackGround]
				ReadMask[_StencilNonBackground]

				CompBack equal
				CompFront equal
			}

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#pragma exclude_renderers nomrt

			#include "UnityCG.cginc"

			sampler2D _LightBuffer;
			struct v2f
			{
				float4 vertex:SV_POSITION;
				float2 texcoord: TEXCOORD0;
			};

			v2f vert(float4 vertex:POSITION,float2 texcoord :TEXCOORD0)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(vertex);
				o.texcoord = texcoord.xy;
				#ifdef UNITY_SINGLE_PASS_STEREO
				o.texcoord = TransformStereoScreenSpaceTex(o.texcoord,1.0);
				#endif
				return o;
			}

			fixed4 frag(v2f i) :SV_Target
			{
				return -log2(tex2D(_LightBuffer,i.texcoord));
			}
			ENDCG
		}
    }
}

