using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("对局前配置")]
        [Tooltip("玩家总数量")]
        public int countAllPlayers = 0;
        [Tooltip("队伍数量")]
        public int countTeams = 0;
        [Tooltip("队伍颜色阵营")]
        public List<string> colorsOfTeams = new List<string>();
        [Tooltip("各队伍玩家数量")]
        public List<int> countPlayersOfTeams = new List<int>();
        [Tooltip("对战模式序号")]
        public int playMode = 0;
        [Tooltip("地图序号")]
        public int indexMap = 0;
        [Tooltip("一局游戏时长")]
        public float totalTime = 300f;
        [Tooltip("配置进度")]
        public int settingState = 0;

    [Header("对局进行时")]
        [Tooltip("剩余时长")]
        public float leftTime;
        [Tooltip("地图中已生成道具数量")]
        public int countItems = 0;
        
        [Tooltip("true:对局开始（已进入地图且对局开始） false:对局未开始（未进入地图 或 已进入地图但对局未开始）")]
        public bool isStart = false;
        //[Tooltip("true:对局结束")]
        //public bool isOver = false;

        [Tooltip("各个队伍当前存活人数")]
        public List<int> survivors = new List<int>();
        [Tooltip("各个队伍染色面积占比")]
        public List<float> coloredRatios = new List<float>();
        [Tooltip("各个队伍击杀数")]
        public List<int> killings = new List<int>();
        [Tooltip("各个队伍死亡数")]
        public List<int> deaths = new List<int>();

    [Header("对局后结算")]
        [Tooltip("获胜队伍编号")]
        public int indexWinnerTeam;
        [Tooltip("MVP")]
        public int mostValuablePlayer;
    public static GameManager Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            Debug.Log("生成GM单例");
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(settingState<=3&&!isStart)
        {
            AddPlayerByDevices();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc");
        }
    }

    public void AddPlayerByDevices()
    {
        Debug.Log("Count : " + InputSystem.devices.Count);
        //InputSystem.AddDevice();
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("InputDevice Added");
                    // New Device.
                    //在这里添加玩家
                    break;
                case InputDeviceChange.Disconnected:
                    Debug.Log("InputDevice Disconnected");
                    // Device got unplugged.
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log("InputDevice Reconnected");
                    // Plugged back in.
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("InputDevice Removed");
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                case InputDeviceChange.UsageChanged:
                    Debug.Log("InputDevice UsageChanged");
                    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                    break;
                default:
                    // See InputDeviceChange reference for other event types.
                    break;
            }
        };
    }
}



