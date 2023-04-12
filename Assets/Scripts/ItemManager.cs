using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Items
{
    [Tooltip("道具名")]
    public string name;
    [Tooltip("道具描述")]
    public string desciption;
    [Tooltip("生成该道具的时间间隔")]
    public float appearInterval;
    [Tooltip("该道具在地图中的存在时间")]
    public float existTime;
    [Tooltip("道具可使用的次数")]
    public float reuseTimes;
}
[System.Serializable]
public class Bird : Items
{
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
}
[System.Serializable]
public class Spring : Items
{
    [Tooltip("高度")]
    public float flySpeed = 1.14f;
    [Tooltip("宽度")]
    public float emitInterval = 5.14f;
    [Tooltip("玩家被击中时颜料槽减少速度")]
    public float velocityDecrease = 1.91f;
    [Tooltip("玩家被击中时血量恢复速度")]
    public float velocityIncrease = 9.810f;
    [Tooltip("击中玩家的鸡腿力度")]
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


