using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float distance; // 射程 
    public float damage; // 伤害
    public float gravityScale; // 受重力影响
    public float speed; // 子弹速度
    private Rigidbody2D rb;
    public CircleCollider2D col;
    private playerController player;
    //private weaponController weapon;
    public GameObject explosionPrefab; // 爆炸特效
    private float travelDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.radius = 0.1f; // 子弹的半径
        travelDistance = 0; // 已经走过了多远
        player = GetComponentInParent<playerController>();
        //weapon = GetComponentInParent<weaponController>();
        // if () 根据子弹类型设置受不受重力影响 
        {
            gravityScale = 0.2f;
        }
        // else 非投掷类
        {
            gravityScale = 0;
        }
    }

    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    void Update()
    {
        /*
         *  对经过的地块进行染色 
         *  
         *  并减小子弹的半径
         *  col.radius -= 
         */
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravityScale * Time.deltaTime); // 受重力影响
        
        float tempVelocity = rb.velocity.magnitude;
        travelDistance += tempVelocity * Time.deltaTime;
        if (travelDistance > distance)  //超过了射程 爆炸
        {
            var temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController otherPlayer = collision.gameObject.GetComponent<playerController>();
            if (otherPlayer.player_name != player.name)
            {
                if (otherPlayer.teamColor != player.teamColor) // 打到敌人了
                {
                    otherPlayer.takeDamage(damage);
                    otherPlayer.rb.AddRelativeForce(rb.velocity * 10, ForceMode2D.Force); // 给予敌人击退 
                }
                else // 打到友军了
                {
                    otherPlayer.takeDamage(- damage * 0.5f);
                }
                var temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);// 生成子弹爆炸
                temp.transform.parent = transform;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Bullet")) // 子弹碰子弹
        {
            Bullet otherBullet = collision.gameObject.GetComponent<Bullet>();
            if (otherBullet.player.teamColor != player.teamColor) // 敌人的子弹
            {
                // 抵消操作
            }
        }
        //if () 撞到墙
        {
            var temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            Destroy(gameObject);
        }
        
    }
}
