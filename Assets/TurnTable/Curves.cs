using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curves : MonoBehaviour
{

    public AnimationCurve animationCurve;

    // Start is called before the first frame update
    void Start()
    {
        //总距离
        float distance = 360;
        //帧数
        int frame = 30;
        //加速度
        float [] accelerate = new float [frame];
        for (int i = 1; i <= frame; i++)
        {
            //每帧的平均值
            float delta = (float)i / frame;
            //每帧的差值
            float value = animationCurve.Evaluate(delta);

            //Debug.Log(delta+" "+value);

            //保存加速度
            accelerate[i-1] = value;

            //Debug.Log( accelerate[i - 1]);
        }

        //加速度总值
        float count = 0;
        for (int i = 0; i < frame; i++)
        {
            count += accelerate[i];
        }
        //Debug.Log("count " + count);

        //距离平均值
        float average = distance/count;

        //Debug.Log("accelerate " + accelerate);

        //最终结果
        float sum = 0;
        for (int i = 0; i < frame; i++)
        {
            //距离增加值
            float speed =  accelerate[i]* average;
            //Debug.Log(add);
            sum += speed;
        }

        Debug.Log(sum);
    }

    // Update is called once per frame
    void Update()
    {
        //float time = Time.time;
        //float value = animationCurve.Evaluate(Time.time);
        //Debug.Log(time+" "+value);
    }
}
