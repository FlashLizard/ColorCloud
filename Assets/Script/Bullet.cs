using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float distance; // ��� 
    public float damage; // �˺�
    public float gravityScale; // ������Ӱ��
    public float speed; // �ӵ��ٶ�
    private Rigidbody2D rb;
    public CircleCollider2D col;
    private playerController player;
    //private weaponController weapon;
    public GameObject explosionPrefab; // ��ը��Ч
    private float travelDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        col.radius = 0.1f; // �ӵ��İ뾶
        travelDistance = 0; // �Ѿ��߹��˶�Զ
        player = GetComponentInParent<playerController>();
        //weapon = GetComponentInParent<weaponController>();
        // if () �����ӵ����������ܲ�������Ӱ�� 
        {
            gravityScale = 0.2f;
        }
        // else ��Ͷ����
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
         *  �Ծ����ĵؿ����Ⱦɫ 
         *  
         *  ����С�ӵ��İ뾶
         *  col.radius -= 
         */
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravityScale * Time.deltaTime); // ������Ӱ��
        
        float tempVelocity = rb.velocity.magnitude;
        travelDistance += tempVelocity * Time.deltaTime;
        if (travelDistance > distance)  //��������� ��ը
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
                if (otherPlayer.teamColor != player.teamColor) // �򵽵�����
                {
                    otherPlayer.takeDamage(damage);
                    otherPlayer.rb.AddRelativeForce(rb.velocity * 10, ForceMode2D.Force); // ������˻��� 
                }
                else // ���Ѿ���
                {
                    otherPlayer.takeDamage(- damage * 0.5f);
                }
                var temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);// �����ӵ���ը
                temp.transform.parent = transform;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Bullet")) // �ӵ����ӵ�
        {
            Bullet otherBullet = collision.gameObject.GetComponent<Bullet>();
            if (otherBullet.player.teamColor != player.teamColor) // ���˵��ӵ�
            {
                // ��������
            }
        }
        //if () ײ��ǽ
        {
            var temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            temp.transform.parent = transform;
            Destroy(gameObject);
        }
        
    }
}
