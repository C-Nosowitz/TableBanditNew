using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{

    public GameObject obstacles1;
    public GameObject obstacles2;
    public GameObject obstacles3;
    public GameObject obstacles4;

    private int ObjecttoGenerate;
    private int ObjectQuantity;
    int spawnLocation = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {

        /*int spawnLocation1 = UnityEngine.Random.Range(0,3);
        int spawnLocation2 = UnityEngine.Random.Range(0, 3);
        int spawnLocation3 = UnityEngine.Random.Range(0, 3);
        int spawnLocation4 = UnityEngine.Random.Range(0, 3);
        Transform spawnPoint1 = transform.GetChild(spawnLocation1);
        Transform spawnPoint2 = transform.GetChild(spawnLocation2);
        Transform spawnPoint3 = transform.GetChild(spawnLocation3);
        Transform spawnPoint4 = transform.GetChild(spawnLocation4);*/


        while (ObjectQuantity < 4)
        {
            Transform spawnPoint = transform.GetChild(spawnLocation).transform;

            ObjecttoGenerate = UnityEngine.Random.Range(1, 5);

            if (ObjecttoGenerate == 1)
            {
                Instantiate(obstacles1, spawnPoint.position, Quaternion.identity);
            }

            if (ObjecttoGenerate == 2)
            {
                Instantiate(obstacles2, spawnPoint.position, Quaternion.identity);
            }

            if (ObjecttoGenerate == 3)
            {
                Instantiate(obstacles3, spawnPoint.position, Quaternion.identity);
            }

            if (ObjecttoGenerate == 4)
            {
                Instantiate(obstacles4, spawnPoint.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.1f);
            ObjectQuantity += 1;
            spawnLocation++;

        }




    }
}