using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("�Ծ�ǰ����")]
        [Tooltip("���������")]
        public int countAllPlayers = 0;
        [Tooltip("��������")]
        public int countTeams = 0;
        [Tooltip("������ɫ��Ӫ")]
        public List<string> colorsOfTeams = new List<string>();
        [Tooltip("�������������")]
        public List<int> countPlayersOfTeams = new List<int>();
        [Tooltip("��սģʽ���")]
        public int playMode = 0;
        [Tooltip("��ͼ���")]
        public int indexMap = 0;
        [Tooltip("һ����Ϸʱ��")]
        public float totalTime = 300f;
        [Tooltip("���ý��� 0:���ڶԾ� 1:ѡ����Ϸģʽ 2:ѡ���ͼ����Ϸʱ�� 3:������ұ�� 4:���ѡ������ͼ���" )]
        public int settingState = 0;
   
    [Header("�Ծֽ���ʱ")]
        [Tooltip("ʣ��ʱ��")]
        public float leftTime;
        [Tooltip("��ͼ�������ɵ�������")]
        public int countItems = 0;
        
        [Tooltip("true:�Ծֿ�ʼ���ѽ����ͼ�ҶԾֿ�ʼ�� false:�Ծ�δ��ʼ��δ�����ͼ �� �ѽ����ͼ���Ծ�δ��ʼ��")]
        public bool isStart = false;
        [Tooltip("true:�Ծֽ���")]
        public bool isOver = false;

        [Tooltip("�������鵱ǰ�������")]
        public List<int> survivors = new List<int>();
        [Tooltip("��������Ⱦɫ���ռ��")]
        public List<float> coloredRatios = new List<float>();
        [Tooltip("���������ɱ��")]
        public List<int> killings = new List<int>();
        [Tooltip("��������������")]
        public List<int> deaths = new List<int>();

    [Header("�Ծֺ����")]
        [Tooltip("��ʤ������")]
        public int indexWinnerTeam;
        [Tooltip("MVP")]
        public int mostValuablePlayer;
    public static GameManager Instance { get { return instance; } }
    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
        {
            Debug.Log("����GM����");
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
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log("esc");
        //}
    }

    public void AddPlayerByDevices()
    {
        Debug.Log("Count : " + InputSystem.devices.Count);
        Debug.Log("Name 0 :" + InputSystem.devices[0].name);
        Debug.Log("Name 1 :" + InputSystem.devices[1].name);
        //InputSystem.AddDevice();
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("InputDevice Added");
                    // New Device.
                    //������������
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



