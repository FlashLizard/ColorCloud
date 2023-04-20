using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    public string player_name; // 玩家名
    
    public int teamColor; // 队伍
    public float currentHP, maxHp, recoverHp; // 血量 以及恢复速度
    public float currentMP, maxMp, recoverMp; // 蓝量 = 颜料量 以及恢复速度
    public float flyConsume; // 在敌方地块染色消耗地 蓝量
    private float RecoverInterval, Rtimer; // 多久不受到伤害就开始回复
    public float defenseRate; // 防御率 受到的伤害乘以 (1 - df)

    public int damage; // 输出
    public int sufferance; // 受到伤害量
    public int kills; // 人头数
    public int death; // 死亡数

    public int kit; // 职业种类

    public float moveSpeedX; // X方向最大速度
    public float moveSpeedY; // Y方向最大速度

    public float gravityFactor; // 重力加速度
    public float maxFallSpeed; // 最大下落速度

    public bool isInEnemy; // 是否在敌人的地块
    public bool isInAlley; // 在我方地块
    public bool isGrounded; // 是否落地
    //private bool canHurt; // 没有处于无敌帧 fps好像都没有无敌帧

    public LayerMask ground;
    public Rigidbody2D rb;
    public Collider2D coll;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var raycastAll = Physics2D.OverlapBoxAll(transform.position, new Vector2(0.4f, 0.4f), 0, ground);
        if (raycastAll.Length > 0)
        { // 在地面上
            isGrounded = true;
        } 
        else isGrounded = false;
        /*
         *  对 isInEnemy 进行赋值
         *  
         *  对 isInAlley 进行赋值
         */
        // Fire的控制放到waeponController了
    }

    private void FixedUpdate()
    {
        updateStatus();
        Movement();
    }

    void updateStatus()
    {
        float Mp_recovery = recoverMp; // 缓慢地自然恢复速度
        float Hp_recovery = recoverHp;

        if (Input.GetKeyDown("space") ) // 按下了空格 
        {
            // if () 和云朵相交
            {
                Mp_recovery *= 10; // 汲取云彩，迅速恢复
                Hp_recovery *= 10;
            }
        }
        float Buff;
        if (isInAlley) // 在我方地块
        {
            Buff = 1.2f;
        }
        else if (isInEnemy) // 在敌方地块
        {
            Buff = 0.6f;
        }
        else // 在没有颜色的地块
        {
            Buff = 1.0f;
        }

        if (Rtimer > RecoverInterval) // 一段时间内没有受到过伤害才可以恢复
        {
            currentHP = Mathf.Min(maxHp, Hp_recovery * Time.deltaTime * Buff + currentHP);
            currentMP = Mathf.Min(maxMp, Mp_recovery * Time.deltaTime * Buff + currentMP);
        }
        Rtimer += Time.deltaTime; // 在受到伤害时记得清零
    }


    void Movement()
    {
        float horizontalMove;
        float verticalMove;
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        float velocityX = rb.velocity.x;
        float velocityY = rb.velocity.y;
        float speedBuff = 1.0f;
        if (isInAlley) // 在我方地块 速度稍微变快
        {
            speedBuff = 1.1f;
        }

        if (horizontalMove != 0)
        {
            velocityX = horizontalMove * moveSpeedX * Time.deltaTime * speedBuff;
        }
        if (verticalMove != 0)
        {
            if (isInAlley || (!isInAlley && currentMP > flyConsume * Time.deltaTime))
            {
                velocityY = verticalMove * moveSpeedY * Time.deltaTime * speedBuff;
                if (!isInAlley) // 不在我方地块 需要消耗染料
                {
                    currentMP -= flyConsume * Time.deltaTime;
                }
            }
        }
        
        else if (!isGrounded) // 没有接触地面
        {
            if (isInEnemy)
            { // 在敌人的地块 加速下坠
                velocityY -= gravityFactor * Time.deltaTime; //加速下降
                if (velocityY < maxFallSpeed) // 最大下落速度
                {
                    velocityY = maxFallSpeed;
                }
            }
            else
            { // 在我方地块 缓慢匀速下降
                velocityY = maxFallSpeed * 0.15f;
            }
        }
        rb.velocity = new Vector2 (velocityX, velocityY);

        // 跟随鼠标 转换人物朝向
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.rotation = Quaternion.Euler(0, -180f, 0);
    }

    public void takeDamage(float damage)
    {
        if (damage > 0) // 是攻击不是奶 要用防御值修正
        {
            damage *= (1 - defenseRate);
            Rtimer = 0; // 没有受伤的时间清零
        }
        //if (canHurt) 
        //{
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0;
            Death();
        }
        if (currentHP > maxHp)
        {
            currentHP = maxHp;
        }
        //}
    }

    private void Death() // 死了怎么处理？
    {

    }
}

