using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : Item
{
    [Header("Bird")]
        [Tooltip("�����ٶ�")]
        public float flySpeed = 1.14f;
        [Tooltip("��йʱ����")]
        public float emitInterval = 5.14f;
        [Tooltip("Ⱦɫ���Ӱ뾶")]
        public float coloredRadius = 1.91f;
        [Tooltip("Ⱦɫ���ӳ���ʱ��")]
        public float colorExistTime = 9.810f;
        [Tooltip("�����ɫ����ʼ��")]
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
