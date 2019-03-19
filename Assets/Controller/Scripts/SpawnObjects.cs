using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    //points where stuff will spawn :)
    public Transform[] StuffSpawnPoints;
    public GameObject[] Bonus; // points model
    //obstacle gameobjects
    public GameObject[] Obstacles;


    void CreateObject(Vector3 position, GameObject prefab)
    {
        Quaternion rotation = Quaternion.identity;
        rotation *= Quaternion.Euler(0, 180, 0);
        Instantiate(prefab, position, rotation);
    }

    void Start()
    {
        bool placeObstacle = Random.Range(0, 2) == 0; //50% chances
        int obstacleIndex = -1;
        if (placeObstacle)
        {
            //select a random spawn point, apart from the first one
            //since we do not want an obstacle there
            obstacleIndex = Random.Range(1, StuffSpawnPoints.Length);
            CreateObject(StuffSpawnPoints[obstacleIndex].position, Obstacles[Random.Range(0, Obstacles.Length)]);
        }

        for (int i = 0; i < StuffSpawnPoints.Length; i++)
       {
            //don't instantiate if there's an obstacle
            if (i == obstacleIndex) continue;
            if (Random.Range(0, 3) == 0) //33% chances to create candy
            {  
                CreateObject(StuffSpawnPoints[i].position, Bonus[Random.Range(0, Bonus.Length)]);
            }
        }

    }
}
