using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    [Tooltip("�����")]
    public string player_name; // �����
    [Tooltip("����")]
    public int teamColor; // 

    public float currentHP, maxHp, recoverHp; // Ѫ�� �Լ��ָ��ٶ�
    public float currentMP, maxMp, recoverMp; // ���� = ������ �Լ��ָ��ٶ�
    [Tooltip("�ڵз��ؿ����Ⱦɫ���ĵ�����")]
    public float flyConsume; // 

    private float RecoverInterval, Rtimer; // ��ò��ܵ��˺��Ϳ�ʼ�ظ�
    [Tooltip("������ �ܵ����˺����� (1 - df)")]
    public float defenseRate; // 

    [Tooltip("������")]
    public float damages; // 
    [Tooltip("�ܵ��˺���")]
    public int sufferance; // 
    [Tooltip("��ͷ��")]
    public int kills; // 
    [Tooltip("������")]
    public int death; // 
    [Tooltip("ְҵ����")]
    public int kit; // 
    [Tooltip("X��������ٶ�")]
    public float moveSpeedX; // 
    [Tooltip("Y��������ٶ�")]
    public float moveSpeedY; // 
    [Tooltip("�������ٶ�")]
    public float gravityFactor; // 
    [Tooltip("��������ٶ�")]
    public float maxFallSpeed; // 
    [Tooltip("�Ƿ��ڵ��˵ĵؿ�")]
    public bool isInEnemy; // 
    [Tooltip("���ҷ��ؿ�")]
    public bool isInAlley; // 
    [Tooltip("�Ƿ����")]
    public bool isGrounded; // 
    //private bool canHurt; // û�д����޵�֡ fps����û���޵�֡

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
        { // �ڵ�����
            isGrounded = true;
        } 
        else isGrounded = false;
        /*
         *  �� isInEnemy ���и�ֵ
         *  
         *  �� isInAlley ���и�ֵ
         */
        // Fire�Ŀ��Ʒŵ�waeponController��
    }

    private void FixedUpdate()
    {
        updateStatus();
        Movement();
    }

    void updateStatus()
    {
        float Mp_recovery = recoverMp; // ��������Ȼ�ָ��ٶ�
        float Hp_recovery = recoverHp;

        if (Input.GetKeyDown("space") ) // �����˿ո� 
        {
            // if () ���ƶ��ཻ
            {
                Mp_recovery *= 10; // ��ȡ�Ʋʣ�Ѹ�ٻָ�
                Hp_recovery *= 10;
            }
        }
        float Buff;
        if (isInAlley) // ���ҷ��ؿ�
        {
            Buff = 1.2f;
        }
        else if (isInEnemy) // �ڵз��ؿ�
        {
            Buff = 0.6f;
        }
        else // ��û����ɫ�ĵؿ�
        {
            Buff = 1.0f;
        }

        if (Rtimer > RecoverInterval) // һ��ʱ����û���ܵ����˺��ſ��Իָ�
        {
            currentHP = Mathf.Min(maxHp, Hp_recovery * Time.deltaTime * Buff + currentHP);
            currentMP = Mathf.Min(maxMp, Mp_recovery * Time.deltaTime * Buff + currentMP);
        }
        Rtimer += Time.deltaTime; // ���ܵ��˺�ʱ�ǵ�����
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
        if (isInAlley) // ���ҷ��ؿ� �ٶ���΢���
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
                if (!isInAlley) // �����ҷ��ؿ� ��Ҫ����Ⱦ��
                {
                    currentMP -= flyConsume * Time.deltaTime;
                }
            }
        }
        
        else if (!isGrounded) // û�нӴ�����
        {
            if (isInEnemy)
            { // �ڵ��˵ĵؿ� ������׹
                velocityY -= gravityFactor * Time.deltaTime; //�����½�
                if (velocityY < maxFallSpeed) // ��������ٶ�
                {
                    velocityY = maxFallSpeed;
                }
            }
            else
            { // ���ҷ��ؿ� ���������½�
                velocityY = maxFallSpeed * 0.15f;
            }
        }
        rb.velocity = new Vector2 (velocityX, velocityY);

        // ������� ת�����ﳯ��
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
            transform.rotation = Quaternion.Euler(0, -180f, 0);
    }

    public bool takeDamage(float damage)
    {
        if (damage > 0) // �ǹ��������� Ҫ�÷���ֵ����
        {
            damage *= (1 - defenseRate);
            Rtimer = 0; // û�����˵�ʱ������
        }
        //if (canHurt) 
        //{
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0;
            Death();
            return true;
        }
        if (currentHP > maxHp)
        {
            currentHP = maxHp;
        }
        //}
        return false;
    }

    private void Death() // ������ô����
    {

    }
}

