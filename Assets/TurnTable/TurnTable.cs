using UnityEngine;

/// <summary>
/// 大转盘
/// </summary>
public class TurnTable : MonoBehaviour
{
    //初始旋转速度
    //[Range(100, 500)]
    public float speed = 0;
    //速度变化值
    public float delta = 0.05f;

    //转盘是否暂停
    private bool pause = true;

    //方向
    [Range(-1, 1)]
    public int direction = -1;

    //帧数
    public int frame = 30;
    //每帧执行时间
    private float deltaTime = 0f;

    //转盘份数
    public int count = 10;

    //分区的角度
    private float partition = 0f;

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

    /// <summary>
    /// 旋转转盘
    /// </summary>
    [ContextMenu("旋转")]
    public void Rotate()
    {
        if (pause)
        {
            //开始旋转
            pause = false;

            //清零距离
            trip = 0f;
        }
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    void Start()
    {
        //每帧时间
        deltaTime = 1f / frame;
        //分区
        partition = 360f / count;

        //初始化位置
        transform.localEulerAngles = Vector3.zero;

        //当前旋转
        currentItem = 0;

        Debug.Log("deltaTime: " + deltaTime);
        Debug.Log("partition: " + partition);
    }

    /// <summary>
    /// 转动函数
    /// </summary>
    void Update()
    {
        if (!pause)
        {
            //计算走多长
            float step = speed * deltaTime;

            //累计步长
            trip += step;

            if (distance - trip <= 0)
            {
                //防止指针走过头
                step = distance - trip;
                //停止转盘
                speed = 0;
            }

            //转动转盘(-1为顺时针,1为逆时针)
            transform.Rotate(new Vector3(0, 0, direction) * step);
            //让转动的速度缓缓降低
            speed -= delta;
            //当转动的速度为0时转盘停止转动
            if (speed <= 0)
            {
                //转动停止
                pause = true;
            }

        }
    }

    /// <summary>
    /// 计算初始速度
    /// </summary>
    [ContextMenu("计算")]
    public void Calc()
    {
        //计算距离
        distance = (count - currentItem) * partition + circle * 360 + item * partition;

        Debug.Log("distance: " + distance);

        //记录旋转的选项
        currentItem = item;

        //计算速度
        float _speed = 0;
        float _distance = 0;
        //最笨的计算
        while (_distance < distance)
        {
            if (_distance + _speed * deltaTime > distance)
            {
                break;
            }
            _distance += _speed * deltaTime;
            _speed += delta;
        }
        Debug.Log("_speed: " + _speed);

        speed = _speed;

        //return _speed;
    }

}