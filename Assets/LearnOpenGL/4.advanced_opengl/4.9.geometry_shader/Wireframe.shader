Shader "Unlit/Wireframe"
{
	Properties
	{
		_WireFrameColor ("Wireframe Color", color) = (1.0, 1.0, 1.0, 1.0)
	}

	SubShader
	{
		Pass
		{
			Tags { "RenderType" = "Opaque" "RenderQueue" = "Geometry"}

			CGPROGRAM
			
			// 1.设置着色器编译目标等级
			#pragma target 4.0

			#pragma vertex vert
			// 2.声明几何着色器方法
			#pragma geometry geo
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct v2g {
				float4 pos : SV_POSITION;
			};

			struct g2f {
				float4 pos : SV_POSITION;
			};

			fixed4 _WireFrameColor;

			v2g vert(appdata_base v) 
			{
				v2g o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			// 输入: point line triangle 
			// 输出: PointStream LineStream TriangleStream
			// Append
			// triStream.RestartStrip

			// 3.指定几何着色器方法内添加的顶点数
			[maxvertexcount(3)]
			void geo(triangle v2g p[3], inout LineStream<g2f> stream)
			{
				g2f o;
				o.pos = p[0].pos;
				stream.Append(o);

				o.pos = p[1].pos;
				stream.Append(o);

				o.pos = p[2].pos;
				stream.Append(o);
			}

			fixed4 frag(g2f i) : SV_Target
			{
				return _WireFrameColor;
			}

			ENDCG
		}
	}
 }
