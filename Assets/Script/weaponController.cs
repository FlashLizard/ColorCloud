using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor.Tilemaps;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    [Tooltip("�������")]
    public int weaponIndex; // 
    [Tooltip("һ�η�����ӵ�����")]
    public int bulletNum = 1; // 
    [Tooltip("���ӵ���ɢ��Ƕ�")]
    public float bulletAngle = 15; // 
    [Tooltip("������")]
    public float interval; // 
    [Tooltip("�ķѵķ���ֵ")]
    public int MP_consume; //
    public playerController player;
    public GameObject bulletPrefab;
    //public GameObject shellPrefab;
    [Tooltip("ǹ������")]
    protected Transform muzzlePos; // 
    [Tooltip("�������")]
    protected Vector2 mousePos; // 
    [Tooltip("����")]
    protected Vector2 faceDirection; // 
    [Tooltip("��ʱ��")]
    protected float timer; // 
    protected float flipY;
    protected Animator animator;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle"); // ǹ��λ��
        player = GetComponentInParent<playerController>();
        flipY = transform.localScale.y;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ��ת����ͼ��
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
        
        if (Input.GetMouseButtonDown(0)) // �������
        {
            if (timer != 0 && MP >= MP_consume)
            {
                Fire();
                timer = interval;
                player.currentMP -= MP_consume;
                player.rb.AddRelativeForce(- faceDirection * 100, ForceMode2D.Force); // ������
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
            float biasAngle = Random.Range(-3f, 3f); // ���ƫת��
            bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(biasAngle, Vector3.forward) * faceDirection);
        }
        else // �෢
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
