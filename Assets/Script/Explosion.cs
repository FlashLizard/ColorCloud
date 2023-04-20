using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo info;
    private Bullet bullet;
    void Awake()
    {
        animator = GetComponent<Animator>();
        bullet = GetComponentInParent<Bullet>();
        
        /*
         * 根据 bullet.col.radius; 来爆炸染色。
         */
    }

    // Update is called once per frame
    void Update()
    {
        info = GetComponent<AnimatorStateInfo>();
        if (info.normalizedTime >= 1) // 动画结束 销毁
        {
            Destroy(gameObject);
        }
    }
}
