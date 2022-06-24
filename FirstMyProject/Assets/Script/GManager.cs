using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GManager : MonoBehaviour
{

    public Transform[] spawnPoints;
    public string[] obstacleObjs;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public ObjectManager objectManager;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    public Text timer;
    float timecnt;

    public GameObject gameoverSet;
    bool isShowOverSet = false;

    public int stage;
    public bool isGameEnd;

    public Player player;

  



    private void Awake()
    {                
        spawnList = new List<Spawn>();
        obstacleObjs = new string[] { "Obstacle1", "Obstacle2", "Obstacle3", "ObstacleIce1", "ObstacleIce2", "ObstacleIce3", "ObstacleSting1", "ObstacleSting2", "LastBlock" };
        StageStart();      
    }

    private void Update()
    {     
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay> nextSpawnDelay && !spawnEnd)
        {          
            SpawnObstacle();
            curSpawnDelay = 0;
        }
     
        OnGUI();
        if(!isGameEnd)
            ShowOverSet();       
    }

    void ShowOverSet()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShowOverSet == false)
        {
            Gameover();
            Time.timeScale = 0;
            isShowOverSet = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isShowOverSet == true)
        {
            gameoverSet.SetActive(false);
            Time.timeScale = 1;
            isShowOverSet = false;
        }
    }

    void OnGUI()
    {
        timecnt += Time.deltaTime;
        timer.text = timecnt.ToString();
        string timeStr;
        timeStr = "" + timecnt.ToString("00.00");
        timeStr = timeStr.Replace(".",":");
        timer.text = timeStr;
    }

    public void StageStart()
    {
        ReadSpawnFile();
    }

    public void StageEnd(bool isStageEnd)
    {
        if (isStageEnd)
            return;

        stage++;
        if (stage > 2)
        {
            Time.timeScale = 0;
            isGameEnd = true;
            Gameover();
        }
        else
            StageStart();
    }

    void SpawnObstacle()
    {
       
        int obstacleIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "N1":
                obstacleIndex = 0;
                break;

            case "N2":
                obstacleIndex = 1;
                break;

            case "N3":
                obstacleIndex = 2;
                break;

            case "I1":
                obstacleIndex = 3;
                break;

            case "I2":
                obstacleIndex = 4;
                break;

            case "I3":
                obstacleIndex = 5;
                break;

            case "S1":
                obstacleIndex = 6;
                break;

            case "S2":
                obstacleIndex = 7;
                break;
            case "L":
                obstacleIndex = 8;
                break;
        }

        int obstaclePoint = spawnList[spawnIndex].point;

        GameObject obstacle = objectManager.MakeObj(obstacleObjs[obstacleIndex]);
        obstacle.transform.position = spawnPoints[obstaclePoint].position;
     
        Obstacle obstacleLogic = obstacle.GetComponent<Obstacle>();
        obstacleLogic.player = player;

        spawnIndex++;
        if(spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("stage"+stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;
            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();

        nextSpawnDelay = spawnList[0].delay;
    }

    public void Gameover()
    {
        gameoverSet.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GameStop()
    {
        Application.Quit();
    }
   
}
