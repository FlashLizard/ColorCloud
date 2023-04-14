using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public class Item:MonoBehaviour
{
    [Header("Item")]
    [Tooltip("������")]
    public string name_Item;
    [Tooltip("��������")]
    public string desciption;
    [Tooltip("���ɸõ��ߵ�ʱ����")]
    public float appearInterval;
    [Tooltip("�õ����ڵ�ͼ�еĴ���ʱ��")]
    public float existTime;
    [Tooltip("���߿�ʹ�õĴ���")]
    public float reuseTimes;
}
public class ItemManager : MonoBehaviour
{
    
    public GameObject prefab_Spring;
    public GameObject mask_Spring;
    public GameObject prefab_Bird;
    public GameObject mask_Bird;

    public List <float> appearIntervals = new List<float>();
    public List <float> appearTimers = new List<float>();

    public delegate void ItemGeneratorsArray();
    public ItemGeneratorsArray[] ItemGenerators;//����ָ�����飬���㰴���±����

    void Awake()
    {
        appearIntervals.Add(prefab_Spring.GetComponent<Spring>().appearInterval);
        appearIntervals.Add(prefab_Bird.GetComponent<Bird>().appearInterval);
        for (int i = 0; i < appearIntervals.Count; i++)
        {
            appearTimers.Add(0);
        }
        ItemGenerators = new ItemGeneratorsArray[]
        {
        SpringBoomer,
        BirdMover,
        };
    }

    void Update()
    {
        ItemGenerator();
    }
    void ItemGenerator()
    {
        
        

        for (int i=0;i<appearIntervals.Count;i++)
        {
            appearTimers[i] += Time.deltaTime;
            if (appearTimers[i] > appearIntervals[i])
            {
                appearTimers[i] -= appearIntervals[i];
                ItemGenerators[i]();//�������ɵ�i������Ԫ�صĺ���
            }
        }
        int randomNum = UnityEngine.Random.Range(0, 229028);
        //SpringBoomer();
        //BirdMover();
    }
    
    void SpringBoomer()
    {
        int pos_x = UnityEngine.Random.Range(0, 229028) % 45 + 5;
        int num_x = (int)(prefab_Spring.GetComponent<Spring>().totalWidth / prefab_Spring.GetComponent<Spring>().singleWidth);
        int num_y = (int)(prefab_Spring.GetComponent<Spring>().totalHeight / prefab_Spring.GetComponent<Spring>().singleHeight);
        for(int i=0;i<num_x;i++)
        {
            for(int j=0;j<num_y;j++)
            {
                Vector3 pos = new Vector3(pos_x + i * prefab_Spring.GetComponent<Spring>().singleWidth, -(j + 1) * prefab_Spring.GetComponent<Spring>().singleHeight, 0);
                Instantiate(prefab_Spring,pos,new Quaternion(0,0,0,0), mask_Spring.transform);
            }
        }
    }
    void BirdMover()
    {
        int fromLeft = UnityEngine.Random.Range(0, 229028) % 2;
        int pos_y = UnityEngine.Random.Range(0, 229028)%20+8;
        int pos_x;
        if(fromLeft == 0)
        {
            pos_x = -1;
        }
        else
        {
            pos_x = 56;
        }
        Instantiate(prefab_Bird, new Vector3(pos_x, pos_y, 0), new Quaternion(0, 0, 0, 0), mask_Bird.transform);
    }
}


