using UnityEngine;

public class TurnTable2 : MonoBehaviour
{
    //动画曲线
    public AnimationCurve animationCurve;

    //总时间，1秒30帧
    public int totalFrame = 30 * 5;

    //加速度数组
    private float[] accelerate;

    //速度数组
    private float[] velocity;

    //当前帧
    private int current = 0;

    //转盘是否播放
    private bool play = false;

    public bool Play
    { 
        get { return play; }
    }

    //方向
    [Range(-1, 1)]
    public int direction = -1;

    //转盘份数
    public int count = 10;

    //分区的角度
    private float partition=0f;

    //上一次的选项
    private int currentItem = 0;

    //转动过的距离
    private float trip = 0f;

    //转动到哪一项
    [Range(0, 10)]
    public int item = 0;

    //转动圈数
    [Range(0, 4)]
    public int circle = 0;

    //计算出走多少距离
    private float distance;

    [ContextMenu("旋转")]
    public void Rotate()
    {
        if (play==false)
        {
            //开始旋转
            play = true;

            //清空帧
            current = 0;

            //清零距离
            trip = 0f;
        }
    }

    void Start()
    {
        //固定每秒30帧
        Application.targetFrameRate = 30;

        //初始化数据
        transform.localEulerAngles = Vector3.zero;

        //转盘默认位置
        currentItem = 0;

        //计算分区
        partition = 360f / count;
    }

    void Update()
    {
        if (play)
        {
            //防止数组越界
            if (current>=velocity.Length)
            {
                play = false;
                return;
            }

            //每步的移动距离
            float step = velocity[current];

            //移动的总距离
            trip += step;

            //防止最后的位置对不准
            if (distance - trip <= 0)
            {
                trip -= step;

                step = distance - trip;

                play = true;
            }

            //欧拉角移动
            transform.Rotate(new Vector3(0, 0, direction) * step);

            //下一次移动距离
            current++;

        }
    }

    
    [ContextMenu("计算")]
    public void Calc()
    {
        //计算距离
        distance = (count- currentItem)*partition + circle * 360 + item * partition;

        //记录旋转的选项
        currentItem = item;

        //加速度
        accelerate = new float[totalFrame];

        //速度数组
        velocity = new float[totalFrame];

        //变化总次数
        float speed_count = 0;

        //计算曲线
        for (int i = 0; i < accelerate.Length; i++)
        {
            float delta = (float)i / totalFrame;

            float value = animationCurve.Evaluate(delta);

            accelerate[i] = value;

            speed_count += accelerate[i];
        }

        //计算平均值
        float average = distance/ speed_count;

        //计算每次速度
        for (int i = 0; i < velocity.Length; i++)
        {
            float speed = accelerate[i] * average;
            velocity[i] = speed;
        }

        Debug.Log("计算完成");

    }

}