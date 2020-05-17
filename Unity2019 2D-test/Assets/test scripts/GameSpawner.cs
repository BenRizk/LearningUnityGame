using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject spawnMe;
    Vector3 thatPos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        thatPos = new Vector3(Random.Range(1, 16), Random.Range(-3, 1), 0);
        if ((Random.Range(1, 1000)) < 5)
        {
            Instantiate(spawnMe, thatPos, Quaternion.identity);
        }


    }
}
