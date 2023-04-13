using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Item
{
    [Header("Bird")]
        [Tooltip("飞行速度")]
        public float flySpeed = 1.14f;
        [Tooltip("排泄时间间隔")]
        public float emitInterval = 5.14f;
        [Tooltip("染色粒子半径")]
        public float coloredRadius = 1.91f;
        [Tooltip("染色粒子持续时间")]
        public float colorExistTime = 9.810f;
        [Tooltip("鸟的颜色，初始白")]
        public string colorItself = "white";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
