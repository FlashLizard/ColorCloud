using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Item
{
    [Header("Spring")]
        [Tooltip("一个的高度")]
        public float singleHeight = 4f;
        [Tooltip("总高度，最好是单个高的整数倍")]
        public float totalHeight = 4f;
        [Tooltip("一个的宽度")]
        public float singleWidth = 4f;
        [Tooltip("总宽度,最好是单个宽的整数倍")]
        public float totalWidth = 8f;
        [Tooltip("出现时上升速度")]
        public float velocity_Spring_Appear_Rise = 4f;
        [Tooltip("消失时下降速度")]
        public float velocity_Spring_Disappear_Fall = 2f;
        [Tooltip("闲置时移动速度")]
        public float velocity_Spring_Idle = 0.5f;
        [Tooltip("玩家被击中时颜料槽减少速度")]
        public float velocity_Color_Decrease = 1.91f;
        [Tooltip("玩家被击中时血量恢复速度")]
        public float velocity_HP_Increase = 9.810f;
        [Tooltip("击中玩家的鸡腿力度")]
        public float hitForce = -1f;
    [Tooltip("目标位置y坐标")]
    public float target_pos_y;
    [Tooltip("存在时间 计时器")]
    public float existTimer = 0f;
    [Tooltip("闲置单方向移动时间 计时器")]
    public float idleTimer = 0f;
    [Tooltip("闲置单方向移动 时长")]
    public float idleTime = 4.5f;
    [Tooltip("转向时静止时间 计时器")]
    public float changeDirectionTimer = 0f;//转向动画计时器
    [Tooltip("转向时静止 时长")]
    public float changeDirectionTime = 1.5f;//转向动画限时
    [Tooltip("闲置时是否向上移动")]
    public bool isDirectionUp = false;
    [Tooltip("闲置时是否在静止（即改变方向）")]
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
