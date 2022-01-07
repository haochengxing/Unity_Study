using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curves : MonoBehaviour
{

    public AnimationCurve animationCurve;

    // Start is called before the first frame update
    void Start()
    {
        //�ܾ���
        float distance = 360;
        //֡��
        int frame = 30;
        //���ٶ�
        float [] accelerate = new float [frame];
        for (int i = 1; i <= frame; i++)
        {
            //ÿ֡��ƽ��ֵ
            float delta = (float)i / frame;
            //ÿ֡�Ĳ�ֵ
            float value = animationCurve.Evaluate(delta);

            //Debug.Log(delta+" "+value);

            //������ٶ�
            accelerate[i-1] = value;

            //Debug.Log( accelerate[i - 1]);
        }

        //���ٶ���ֵ
        float count = 0;
        for (int i = 0; i < frame; i++)
        {
            count += accelerate[i];
        }
        //Debug.Log("count " + count);

        //����ƽ��ֵ
        float average = distance/count;

        //Debug.Log("accelerate " + accelerate);

        //���ս��
        float sum = 0;
        for (int i = 0; i < frame; i++)
        {
            //��������ֵ
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
