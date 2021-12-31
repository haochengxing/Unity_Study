using UnityEngine;

/// <summary>
/// ��ת��
/// </summary>
public class TurnTable : MonoBehaviour
{
    //��ʼ��ת�ٶ�
    //[Range(100, 500)]
    public float speed = 0;
    //�ٶȱ仯ֵ
    public float delta = 0.05f;

    //ת���Ƿ���ͣ
    private bool pause = true;

    //����
    [Range(-1, 1)]
    public int direction = -1;

    //֡��
    public int frame = 30;
    //ÿִ֡��ʱ��
    private float deltaTime = 0f;

    //ת�̷���
    public int count = 10;

    //�����ĽǶ�
    private float partition = 0f;

    //��һ�ε�ѡ��
    private int currentItem = 0;

    //ת�����ľ���
    private float trip = 0f;

    //ת������һ��
    [Range(0, 10)]
    public int item = 0;

    //ת��Ȧ��
    [Range(0, 4)]
    public int circle = 0;

    //������߶��پ���
    private float distance;

    /// <summary>
    /// ��תת��
    /// </summary>
    [ContextMenu("��ת")]
    public void Rotate()
    {
        if (pause)
        {
            //��ʼ��ת
            pause = false;

            //�������
            trip = 0f;
        }
    }

    /// <summary>
    /// ��ʼ������
    /// </summary>
    void Start()
    {
        //ÿ֡ʱ��
        deltaTime = 1f / frame;
        //����
        partition = 360f / count;

        //��ʼ��λ��
        transform.localEulerAngles = Vector3.zero;

        //��ǰ��ת
        currentItem = 0;

        Debug.Log("deltaTime: " + deltaTime);
        Debug.Log("partition: " + partition);
    }

    /// <summary>
    /// ת������
    /// </summary>
    void Update()
    {
        if (!pause)
        {
            //�����߶೤
            float step = speed * deltaTime;

            //�ۼƲ���
            trip += step;

            if (distance - trip <= 0)
            {
                //��ָֹ���߹�ͷ
                step = distance - trip;
                //ֹͣת��
                speed = 0;
            }

            //ת��ת��(-1Ϊ˳ʱ��,1Ϊ��ʱ��)
            transform.Rotate(new Vector3(0, 0, direction) * step);
            //��ת�����ٶȻ�������
            speed -= delta;
            //��ת�����ٶ�Ϊ0ʱת��ֹͣת��
            if (speed <= 0)
            {
                //ת��ֹͣ
                pause = true;
            }

        }
    }

    /// <summary>
    /// �����ʼ�ٶ�
    /// </summary>
    [ContextMenu("����")]
    public void Calc()
    {
        //�������
        distance = (count - currentItem) * partition + circle * 360 + item * partition;

        Debug.Log("distance: " + distance);

        //��¼��ת��ѡ��
        currentItem = item;

        //�����ٶ�
        float _speed = 0;
        float _distance = 0;
        //��ļ���
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