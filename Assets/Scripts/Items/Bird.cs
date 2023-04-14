using UnityEngine;

public class Bird : Item
{
    [Header("Bird")]
    [Tooltip("�����ٶ�")]
    public float flyVelocity = 1.14f;
    [Tooltip("�ı��ٶȷ����ʱ����")]
    public float flyDiretionTurnedTime = 5.14f;
    [Tooltip("��йʱ����")]
    public float emitTime = 5.14f;
    [Tooltip("��й��Ⱦɫ�뾶")]
    public float coloredRadius = 1.91f;
    [Tooltip("��Ⱦɫ����ɫ����ʱ��")]
    public float colorItselfLastingTime = 9.810f;
    [Tooltip("�����ɫ����ʼ��")]
    public string colorItself = "white";

    [Tooltip("�����ٶ���x��������ļнǦ�")]
    public float velocity_Alpha;
    [Tooltip("�ı��ٶȷ��� ��ʱ��")]
    public float flyDiretionTurnedTimer;
    [Tooltip("����ʱ�� ��ʱ��")]
    public float existTimer = 0f;
    public bool isDisappearing = false;
    public bool isOutOfMap = false;

    public float target_pos_x;
    public float target_pos_y;
    // Start is called before the first frame update
    void Start()
    {
        target_pos_x = (float)UnityEngine.Random.Range(0, 229028) % 3 + 26.5f;
        target_pos_y = (float)UnityEngine.Random.Range(0, 229028) % 3 + 15.5f;
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisappearing)
        {
            flyDiretionTurnedTimer += Time.deltaTime;
            if (flyDiretionTurnedTimer > flyDiretionTurnedTime)
            {
                flyDiretionTurnedTimer = 0f;
                ChangeDirection();
            }
            existTimer += Time.deltaTime;
            if (existTimer > existTime)
            {
                isDisappearing = true;
                DisappearMove();
            }
            if (OutOfMap())
            {
                Homecoming();
            }
        }
        else if(OutOfMap())
        {
            Destroy(gameObject);
        }
        
    }
    void ChangeDirection()
    {
        //Debug.Log("cos(90) = " + Mathf.Cos(6.28f));
        velocity_Alpha = (float)(UnityEngine.Random.Range(0, 229028) % 1000) / 1000f * (2 * Mathf.PI);// 0 ~ 2��
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(flyVelocity * Mathf.Cos(velocity_Alpha), flyVelocity * Mathf.Sin(velocity_Alpha));
        ChangeSpriteDirection();
    }
    void DisappearMove()
    {
        if (!OutOfMap())
        {
            velocity_Alpha = ((UnityEngine.Random.Range(0, 229028) % 1000) - 500) / 1000f * Mathf.PI;// -1/2�� ~ 1/2��
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(flyVelocity * Mathf.Cos(velocity_Alpha), flyVelocity * Mathf.Sin(velocity_Alpha));
            ChangeSpriteDirection();
        }
    }
    bool OutOfMap()
    {
        if (transform.position.x <= 0 || transform.position.x >= 55 || transform.position.y <= 0 || transform.position.y >= 30)
        {
            isOutOfMap = true;
            return true;
        }
        isOutOfMap = false;
        return false;
    }
    void Homecoming()
    {
        
        float delta_pos_x = target_pos_x - transform.position.x;
        float delta_pos_y = target_pos_y - transform.position.y;
        velocity_Alpha = Mathf.Atan(delta_pos_y / delta_pos_x)*360/2/Mathf.PI;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(flyVelocity * Mathf.Cos(velocity_Alpha), flyVelocity * Mathf.Sin(velocity_Alpha));
        ChangeSpriteDirection();
    }
    void ChangeSpriteDirection()
    {
        if(gameObject.GetComponent<Rigidbody2D>().velocity.x<=0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
