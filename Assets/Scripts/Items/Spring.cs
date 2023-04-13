using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Item
{
    [Header("Spring")]
        [Tooltip("һ���ĸ߶�")]
        public float singleHeight = 4f;
        [Tooltip("�ܸ߶ȣ�����ǵ����ߵ�������")]
        public float totalHeight = 4f;
        [Tooltip("һ���Ŀ��")]
        public float singleWidth = 4f;
        [Tooltip("�ܿ��,����ǵ������������")]
        public float totalWidth = 8f;
        [Tooltip("����ʱ�����ٶ�")]
        public float velocity_Spring_Appear_Rise = 4f;
        [Tooltip("��ʧʱ�½��ٶ�")]
        public float velocity_Spring_Disappear_Fall = 2f;
        [Tooltip("����ʱ�ƶ��ٶ�")]
        public float velocity_Spring_Idle = 0.5f;
        [Tooltip("��ұ�����ʱ���ϲۼ����ٶ�")]
        public float velocity_Color_Decrease = 1.91f;
        [Tooltip("��ұ�����ʱѪ���ָ��ٶ�")]
        public float velocity_HP_Increase = 9.810f;
        [Tooltip("������ҵļ�������")]
        public float hitForce = -1f;
    [Tooltip("Ŀ��λ��y����")]
    public float target_pos_y;
    [Tooltip("//����ʱ�� ��ʱ��")]
    public float existTimer = 0f;
    [Tooltip("//���õ������ƶ�ʱ�� ��ʱ��")]
    public float idleTimer = 0f;
    [Tooltip("//���õ������ƶ� ʱ��")]
    public float idleTime = 4.5f;
    [Tooltip("ת��ʱ��ֹʱ�� ��ʱ��")]
    public float changeDirectionTimer = 0f;//ת�򶯻���ʱ��
    [Tooltip("ת��ʱ��ֹ ʱ��")]
    public float changeDirectionTime = 1.5f;//ת�򶯻���ʱ
    [Tooltip("����ʱ�Ƿ������ƶ�")]
    public bool isDirectionUp = false;
    [Tooltip("����ʱ�Ƿ��ھ�ֹ�����ı䷽��")]
    public bool isChangeDirection = false;
    //[HideInInspector]
    public bool isAppearing = true;
    //[HideInInspector]
    public bool isDisappearing = false;

    // Start is called before the first frame update
    void Start()
    {
        target_pos_y = transform.position.y + totalHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAppearing)
        {
            AppearingMove();
        }
        if(!isAppearing&&!isDisappearing)
        {
            existTimer += Time.deltaTime;
            if(idleTimer > idleTime) 
            {
                idleTimer = 0f;
                isChangeDirection = true;
            }
            if(isChangeDirection)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                changeDirectionTimer += Time.deltaTime;
            }
            else
            {
                idleTimer += Time.deltaTime;
            }
            if(changeDirectionTimer > changeDirectionTime)
            {
                changeDirectionTimer = 0f;
                isDirectionUp = !isDirectionUp;
                IdleMove(isDirectionUp);
                isChangeDirection = false;
            }
        }
        if(existTimer >= existTime)
        {
            isDisappearing = true;
        }
        if(isDisappearing)
        {
            DisappearingMove();
        }
    }

    void AppearingMove()
    {
        if(transform.position.y < target_pos_y)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =new Vector2(0,velocity_Spring_Appear_Rise);
            return;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isAppearing = false;
        IdleMove(isDirectionUp);
    }
    void IdleMove(bool directionUp)
    {
        if(directionUp)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity_Spring_Idle);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity_Spring_Idle);
        }
    }
    void DisappearingMove()
    {
        if (transform.position.y > -singleHeight)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocity_Spring_Disappear_Fall);
            return;
        }
        Destroy(gameObject);
    }
}
