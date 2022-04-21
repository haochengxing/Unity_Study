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


