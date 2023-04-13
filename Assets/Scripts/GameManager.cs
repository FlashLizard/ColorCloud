using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("�Ծ�ǰ����")]
        [Tooltip("�������")]
        public int countPlayers = 0;
        [Tooltip("��������")]
        public int countTeams = 0;
        [Tooltip("��սģʽ���")]
        public int playMode = 0;
        [Tooltip("��ͼ���")]
        public int indexMap = 0;
        [Tooltip("һ����Ϸʱ��")]
        public float totalTime = 300f;
        

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

    }

    public void Func()
    {
        Debug.Log("666");
    }
}



