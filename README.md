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

