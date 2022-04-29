# Unity_Study

使用代码创建一个立方体

https://learnopengl-cn.github.io/01%20Getting%20started/08%20Coordinate%20Systems/

学习深度测试

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/01%20Depth%20testing/

先渲染不透明物体，顺序是从前到后；再渲染透明物体，顺序是从后到前。

Early-Z的实现，主要是通过一个Z-pre-pass实现
GPU中运用了Early-Z的技术，在Vertex阶段和Fragment阶段之间（光栅化之后，fragment之前）进行一次深度测试，如果深度测试失败，就不必进行fragment阶段的计算

模板测试画物体轮廓

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/02%20Stencil%20testing/

混合

最终颜色=源颜色向量X源因子值+目标颜色向量X目标因子值
Blend SrcAlpha OneMinusSrcAlpha
源因子值0.6
目标因子值1-0.6=0.4
C=(0,1,0,0.6)*0.6+(1,0,0,1)*(1-0.6)

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/03%20Blending/

后处理

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/05%20Framebuffers/

天空盒反射和折射

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/06%20Cubemaps/

几何着色器

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/09%20Geometry%20Shader/

gpuinstance

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/10%20Instancing/

抗锯齿

https://learnopengl-cn.github.io/04%20Advanced%20OpenGL/11%20Anti%20Aliasing/

https://blog.csdn.net/eevee_1/article/details/118632398

https://www.jianshu.com/p/42c24309f299

Blinn-Phong

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/01%20Advanced%20Lighting/

gamma校正

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/02%20Gamma%20Correction/

shadowmap

https://zhuanlan.zhihu.com/p/30799329


exp(x) 计算ex 的值，e= 2.71828182845904523536
exp2(x) 计算2x 的值
log(x) 计算ln(x)的值，x 必须大于 0
log2(x) 计算log(2x) 的值，x 必须大于 0
log10(x) 计算log10(x) 的值，x 必须大于 0

https://zhuanlan.zhihu.com/p/382202359

https://zhuanlan.zhihu.com/p/454970727

距离平方成反比贡献度的算法，比平均贡献好些。

exp（-80*d)*exp(80*z)=exp(-80*(d-z));

RenderTextureFormat

https://docs.unity3d.com/ScriptReference/RenderTextureFormat.html

RenderTextureReadWrite

https://docs.unity3d.com/ScriptReference/RenderTextureReadWrite.html

float:最高精度的浮点值，32为存储
half：中等精度，16位存储
fixed：最低精度，11位存储

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/03%20Shadows/01%20Shadow%20Mapping/

CalculateFrustumCorners 将相机在远截面的四个角的位置赋值给Corners

Camera.CalculateFrustumCorners
public void CalculateFrustumCorners (Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
viewport	用于视锥体计算的标准化视口坐标。
z	从摄像机原点开始的 Z 深度（将在该位置计算四角）
eye	要使用的摄像机眼投影矩阵。
outCorners	包含视锥体四角矢量的输出数组。不能为 null，且长度必须 >= 4

TransformPoint 将target相对于A的相对坐标转换为世界坐标
InverseTransformPoint  将target的世界坐标转换为相对于A的相对坐标
GL.GetGPUProjectionMatrix 用来处理DX 和OpenGL的坐标差异性的
在 Unity 中，投影矩阵的规则和OpenGL中的规则一样。 在不同的平台上，这种规则不一定相同。因此必须变化这个规则来调用不同的API。可以用这个函数得到最终的投影矩阵
如果RenderTexture为真，进行纹理渲染，这对产生的投影矩阵也有影响。

w 具有了特殊意义：可以用来进行透视除法，
透视除法( xyz 分别除以 w )后的 x、y 用来进行屏幕映射(也就是这个顶点应该出现在屏幕上的哪个位置)，
z 用来表示这个顶点的深度(是否离观察者更近或被其他顶点挡住等)。

CSM

https://zhuanlan.zhihu.com/p/45673049

MonoOrStereoscopicEye
摄像机眼，对应于人的左眼或右眼（用于立体渲染），或不对应于眼睛（用于非立体渲染）。
Left	对应于左眼立体渲染的摄像机眼。
Right	对应于右眼立体渲染的摄像机眼。
Mono	对应于非立体渲染的摄像机眼。

Camera.CalculateFrustumCorners
给定视口坐标，计算指向指定摄像机深度处视锥体四角的视图空间矢量。

Vector3.Magnitude
返回向量的长度，也就是点目标点(x,y,z)到原点(0,0,0)的距离。

利用GPU实现大规模动画角色的渲染
 
https://www.cnblogs.com/guaishoudashu/p/9927604.html

fmod(x,y)	返回x/y的余数。如果y为0，结果不可预料。

顶点 ID：SV_VertexID
顶点着色器可以接收具有“顶点编号”（为无符号整数）的变量。当您想要从纹理或 ComputeBuffers 中 获取额外的每顶点数据时，这非常有用。
此功能从 DX10（着色器模型 4.0）和 GLCore/OpenGL ES 3 开始才存在，因此着色器需要具有 #pragma target 3.5 编译指令。

Mathf.NextPowerOfTwo 最接近的二次方

AnimationState 控制动画混合 播放动画时,AnimationState允许你修改速度，权值，时间和层

Mathf .ClosestPowerOfTwo返回两个值的最接近的幂

https://docs.unity3d.com/ScriptReference/TextureFormat.html

利用GPU实现大规模动画角色的渲染
 
https://www.cnblogs.com/guaishoudashu/p/9927604.html

烘培光照贴图

https://blog.csdn.net/u011105442/article/details/112391799

https://www.bilibili.com/read/cv15063861


通过DecodeLightmap函数将采样得到贴图颜色解码为实际的数值在shader中计算
// decodeInstructions is a internal constant value unity_Lightmap_HDR
inline half3 DecodeLightmap( fixed4 color, half4 decodeInstructions)
{
#if defined(UNITY_LIGHTMAP_DLDR_ENCODING)
    return DecodeLightmapDoubleLDR(color, decodeInstructions);
#elif defined(UNITY_LIGHTMAP_RGBM_ENCODING)
    return DecodeLightmapRGBM(color, decodeInstructions);
#else //defined(UNITY_LIGHTMAP_FULL_HDR)
    return color.rgb;
#endif
}

对于RGBM 编码格式又根据Color Space的不同分为三种
// decodeInstructions is a internal constant value unity_Lightmap_HDR
inline half3 DecodeLightmapRGBM (half4 data, half4 decodeInstructions)
{
    // If Linear mode is not supported we can skip exponent part
    #if defined(UNITY_COLORSPACE_GAMMA)
    # if defined(UNITY_FORCE_LINEAR_READ_FOR_RGBM)
        return (decodeInstructions.x * data.a) * sqrt(data.rgb);
    # else
        return (decodeInstructions.x * data.a) * data.rgb;
    # endif
    #else
        return (decodeInstructions.x * pow(data.a, decodeInstructions.y)) * data.rgb;
    #endif
}

dLDR处理比较简单
inline half3 DecodeLightmapDoubleLDR( fixed4 color, half4 decodeInstructions)
{
    // decodeInstructions.x contains 2.0 when gamma color space is used or pow(2.0, 2.2) = 4.59 when linear color space is used on mobile platforms
    return decodeInstructions.x * color.rgb;
}

1 RGBM : 一个像素颜色Color由两部分组成 颜色（RGB通道）和一个乘积系数M（A通道），在线性空间linear里，Color的每一个分量的值的范围为 0到34.49(5的2.2次方)，在gamma空间，范围为0到5

2 dLDR ： 在移动平台上dLDR编码将范围0到2，压缩为0到1，在shader中使用的时候，采样的像素颜色分量将转化到0到2，方法是将分量值乘以 2（gamma）或者 2的2.2次方（linear）所以可以知道压缩的办法是将分量值除以2（gamma）或者 2的2.2次方（linear）

https://zhuanlan.zhihu.com/p/371900093

法线贴图

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/04%20Normal%20Mapping/

https://blog.csdn.net/u014078887/article/details/117677038

inline fixed3 UnpackNormal(fixed4 packednormal)
{
#if defined(UNITY_NO_DXT5nm)
    return packednormal.xyz * 2 - 1;
#else
    return UnpackNormalmapRGorAG(packednormal);
#endif
}

fixed3 UnpackNormalmapRGorAG(fixed4 packednormal)
{
    // This do the trick
   packednormal.x *= packednormal.w;

    fixed3 normal;
    normal.xy = packednormal.xy * 2 - 1;
    normal.z = sqrt(1 - saturate(dot(normal.xy, normal.xy)));
    return normal;
}

https://blog.csdn.net/fjjaylz/article/details/110775812

顶点着色器里的值被传入片元着色器时会被进行一次插值计算

https://blog.csdn.net/u014078887/article/details/117677038

为什么使用切线空间下的法线纹理

自由度更高，当一张纹理可以用在不同的模型上时，可以只使用一套法线纹理，但是模型空间下，必须对每一个模型都创建一套对应的法线纹理
可压缩，切线空间下法线纹理的z方向总是正方向，因此我们可以通过xy轴进行点积，之后开平方求出z轴的方向。每个像素只需要存储两个分量。

视差贴图

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/05%20Parallax%20Mapping/


// Declares 3x3 matrix 'rotation', filled with tangent space basis
#define TANGENT_SPACE_ROTATION \
    float3 binormal = cross( normalize(v.normal), normalize(v.tangent.xyz) ) * v.tangent.w; \
    float3x3 rotation = float3x3( v.tangent.xyz, binormal, v.normal )

// Computes object space light direction
inline float3 ObjSpaceLightDir( in float4 v )
{
    float3 objSpaceLightPos = mul(unity_WorldToObject, _WorldSpaceLightPos0).xyz;
    #ifndef USING_LIGHT_MULTI_COMPILE
        return objSpaceLightPos.xyz - v.xyz * _WorldSpaceLightPos0.w;
    #else
        #ifndef USING_DIRECTIONAL_LIGHT
        return objSpaceLightPos.xyz - v.xyz;
        #else
        return objSpaceLightPos.xyz;
        #endif
    #endif
}

// Computes object space view direction
inline float3 ObjSpaceViewDir( in float4 v )
{
    float3 objSpaceCameraPos = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos.xyz, 1)).xyz;
    return objSpaceCameraPos - v.xyz;
}

https://www.jianshu.com/p/fea6c9fc610f

HDR

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/06%20HDR/

http://www.u3d8.com/?p=2560

在高强度区域不会丢失颜色

泛光 bloom

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/07%20Bloom/

亮度
float Luminance( vec3 c )
{
    return dot( c, vec3(0.22, 0.707, 0.071) );
}

首先提取一张图像的亮度，进行模糊之后(高斯模糊或者其他模糊方法)，覆盖到原图上。
因此需要好几个pass来实现，一个pass提取亮度，两个pass进行高斯模糊，最后一个pass混合两张图。

安卓和iOS拉起键盘，判断键盘done状态

https://zhuanlan.zhihu.com/p/371161112


延迟着色法

https://learnopengl-cn.github.io/05%20Advanced%20Lighting/08%20Deferred%20Shading/








