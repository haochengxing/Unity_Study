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