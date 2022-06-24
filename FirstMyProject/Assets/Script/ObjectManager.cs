using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject obstacle1Prefab;
    public GameObject obstacle2Prefab;
    public GameObject obstacle3Prefab;
    public GameObject obstacleIce1Prefab;
    public GameObject obstacleIce2Prefab;
    public GameObject obstacleIce3Prefab;
    public GameObject obstacleSting1Prefab;
    public GameObject obstacleSting2Prefab;
    public GameObject lastPrefab;

    GameObject[] obstacle1;
    GameObject[] obstacle2;
    GameObject[] obstacle3;
    GameObject[] obstacleIce1;
    GameObject[] obstacleIce2;
    GameObject[] obstacleIce3;
    GameObject[] obstacleSting1;
    GameObject[] obstacleSting2;
    GameObject[] lastblock;

    GameObject[] targetPool;

    private void Awake()
    {
        obstacle1 = new GameObject[20];
        obstacle2 = new GameObject[20];
        obstacle3 = new GameObject[20];

        obstacleIce1 = new GameObject[20];
        obstacleIce2 = new GameObject[20];
        obstacleIce3 = new GameObject[20];

        obstacleSting1 = new GameObject[20];
        obstacleSting2 = new GameObject[20];

        lastblock = new GameObject[3];

        Generate();
    }

    void Generate()
    {
        for(int i = 0; i < obstacle1.Length; i++)
        {
            obstacle1[i] = Instantiate(obstacle1Prefab);
            obstacle1[i].SetActive(false);
        }

        for (int i = 0; i < obstacle1.Length; i++)
        {
            obstacle2[i] = Instantiate(obstacle2Prefab);
            obstacle2[i].SetActive(false);
        }

        for (int i = 0; i < obstacle1.Length; i++)
        {
            obstacle3[i] = Instantiate(obstacle3Prefab);
            obstacle3[i].SetActive(false);
        }


        for (int i = 0; i < obstacleIce1.Length; i++)
        {
            obstacleIce1[i] = Instantiate(obstacleIce1Prefab);
            obstacleIce1[i].SetActive(false);
        }

        for (int i = 0; i < obstacleIce2.Length; i++)
        {
            obstacleIce2[i] = Instantiate(obstacleIce2Prefab);
            obstacleIce2[i].SetActive(false);
        }

        for (int i = 0; i < obstacleIce3.Length; i++)
        {
            obstacleIce3[i] = Instantiate(obstacleIce3Prefab);
            obstacleIce3[i].SetActive(false);
        }

        for (int i = 0; i < obstacleSting1.Length; i++)
        {
            obstacleSting1[i] = Instantiate(obstacleSting1Prefab);
            obstacleSting1[i].SetActive(false);
        }

        for (int i = 0; i < obstacleSting2.Length; i++)
        {
            obstacleSting2[i] = Instantiate(obstacleSting2Prefab);
            obstacleSting2[i].SetActive(false);
        }

        for(int i = 0;  i < lastblock.Length; i++)
        {
            lastblock[i] = Instantiate(lastPrefab);
            lastblock[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Obstacle1":
                targetPool = obstacle1;
                break;
            case "Obstacle2":
                targetPool = obstacle2;
                break;
            case "Obstacle3":
                targetPool = obstacle3;
                break;

            case "ObstacleIce1":
                targetPool = obstacleIce1;
                break;
            case "ObstacleIce2":
                targetPool = obstacleIce2;
                break;
            case "ObstacleIce3":
                targetPool = obstacleIce3;
                break;
            case "ObstacleSting1":
                targetPool = obstacleSting1;               
                break;
            case "ObstacleSting2":
                targetPool = obstacleSting2;
                break;
            case "LastBlock":
                targetPool = lastblock;
                break;

        }
        for(int i =0; i<targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);               
                return targetPool[i];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "Obstacle1":
                targetPool = obstacle1;
                break;
            case "Obstacle2":
                targetPool = obstacle2;
                break;
            case "Obstacle3":
                targetPool = obstacle3;
                break;

            case "ObstacleIce1":
                targetPool = obstacleIce1;
                break;
            case "ObstacleIce2":
                targetPool = obstacleIce2;
                break;
            case "ObstacleIce3":
                targetPool = obstacleIce3;
                break;
            case "ObstacleSting1":
                targetPool = obstacleSting1;
                break;
            case "ObstacleSting2":
                targetPool = obstacleSting2;
                break;
            case "LastBlock":
                targetPool = lastblock;
                break;
        }
        return targetPool;
    }


}
