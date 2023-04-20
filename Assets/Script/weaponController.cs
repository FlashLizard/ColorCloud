using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor.Tilemaps;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    [Tooltip("武器标号")]
    public int weaponIndex; // 
    [Tooltip("一次发射的子弹数量")]
    public int bulletNum = 1; // 
    [Tooltip("多子弹的散射角度")]
    public float bulletAngle = 15; // 
    [Tooltip("射击间隔")]
    public float interval; // 
    [Tooltip("耗费的法力值")]
    public int MP_consume; //
    public playerController player;
    public GameObject bulletPrefab;
    //public GameObject shellPrefab;
    [Tooltip("枪口坐标")]
    protected Transform muzzlePos; // 
    [Tooltip("鼠标坐标")]
    protected Vector2 mousePos; // 
    [Tooltip("朝向")]
    protected Vector2 faceDirection; // 
    [Tooltip("计时器")]
    protected float timer; // 
    protected float flipY;
    protected Animator animator;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle"); // 枪口位置
        player = GetComponentInParent<playerController>();
        flipY = transform.localScale.y;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 翻转武器图标
        if (mousePos.x < transform.position.x)
        {
            transform.localScale = new Vector3(flipY, - flipY, 1f);
        }
        else
        {
            transform.localScale = new Vector3(flipY, flipY, 1f);
        }
        faceDirection = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = faceDirection;
        
    }

    virtual public void Shoot()//ref float MP
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0) { timer = 0; }
        }

        float MP = player.currentMP;
        
        if (Input.GetMouseButtonDown(0)) // 左键发射
        {
            if (timer == 0 && MP >= MP_consume)
            {
                Fire();
                timer = interval;
                player.currentMP -= MP_consume;
                player.rb.AddRelativeForce(- faceDirection * 100, ForceMode2D.Force); // 后坐力
            }
        }
    }

    virtual protected void Fire()
    {
        //animator.SetTrigger("Shooting");
        if (bulletNum == 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
            bullet.transform.parent = player.transform;
            float biasAngle = Random.Range(-3f, 3f); // 随机偏转角
            bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(biasAngle, Vector3.forward) * faceDirection);
        }
        else // 多发
        {
            int medium = bulletNum / 2;
            for (int i = 0; i < bulletNum; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
                bullet.transform.parent = player.transform;
                if (bulletNum % 2 == 1)
                {
                    bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - medium), Vector3.forward) * faceDirection);
                }
                else
                {
                    bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - medium) + bulletAngle/2, Vector3.forward) * faceDirection);
                }
            }
        }

    }
}
