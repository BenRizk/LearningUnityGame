using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject apples;
    Vector3 thatPos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        thatPos = new Vector3(Random.Range(1, 1000), Random.Range(1, 1000), 0);
        if ((Random.Range(1, 1000)) < 5)
        {
            Instantiate(apples, thatPos, Quaternion.identity);
        }


    }
}
