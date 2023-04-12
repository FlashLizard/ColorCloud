using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Items
{
    [Tooltip("������")]
    public string name;
    [Tooltip("��������")]
    public string desciption;
    [Tooltip("���ɸõ��ߵ�ʱ����")]
    public float appearInterval;
    [Tooltip("�õ����ڵ�ͼ�еĴ���ʱ��")]
    public float existTime;
    [Tooltip("���߿�ʹ�õĴ���")]
    public float reuseTimes;
}
[System.Serializable]
public class Bird : Items
{
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
}
[System.Serializable]
public class Spring : Items
{
    [Tooltip("�߶�")]
    public float flySpeed = 1.14f;
    [Tooltip("���")]
    public float emitInterval = 5.14f;
    [Tooltip("��ұ�����ʱ���ϲۼ����ٶ�")]
    public float velocityDecrease = 1.91f;
    [Tooltip("��ұ�����ʱѪ���ָ��ٶ�")]
    public float velocityIncrease = 9.810f;
    [Tooltip("������ҵļ�������")]
    public float hitForce = -1f;
}
public class ItemManager : MonoBehaviour
{
    public Bird bird = new Bird();
    public Spring spring = new Spring();
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void ItemGenerator()
    {
        int randomNum = UnityEngine.Random.Range(0, 229028);
    }
    void BirdMover()
    {
        int randomNum = UnityEngine.Random.Range(0, 229028);
    }
    void SpringBoomer()
    {
        int randomNum = UnityEngine.Random.Range(0, 229028);
    }
}


