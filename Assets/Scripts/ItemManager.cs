using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item:MonoBehaviour
{
    [Header("Item")]
    [Tooltip("道具名")]
    public string name_Item;
    [Tooltip("道具描述")]
    public string desciption;
    [Tooltip("生成该道具的时间间隔")]
    public float appearInterval;
    [Tooltip("该道具在地图中的存在时间")]
    public float existTime;
    [Tooltip("道具可使用的次数")]
    public float reuseTimes;
}
public class ItemManager : MonoBehaviour
{
    
    public GameObject prefab_Spring;
    public GameObject mask_Spring;
    public GameObject prefab_Bird;
    public GameObject mask_Bird;
    void Awake()
    {
        Debug.Log("IM   Awake");
        SpringBoomer();
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
        int fromLeft = UnityEngine.Random.Range(0, 229028) % 2;
        int pos_y = UnityEngine.Random.Range(0, 229028)%20+8;
        
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

}


