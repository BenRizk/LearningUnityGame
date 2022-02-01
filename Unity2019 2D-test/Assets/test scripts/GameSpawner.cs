using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public long spawnRate = 5;
    public long maxObjects = 5;
    long currentObjects = 0;
    public GameObject spawnMe;
    public Sprite[] ListedSprites = new Sprite[4];
    Vector3 thatPos = new Vector3();

    // Update is called once per frame
    void Update()
    {
        thatPos = new Vector3(Random.Range(1, 16), Random.Range(-3, 1), 0); // choose random lcoation to spawn
        if ((Random.Range(1, 1000)) < spawnRate && currentObjects <= maxObjects)                            // randomly spawn object
        {        
            if (spawnMe.name.Equals("Plant_Seed"))           //If I am a seed then assign values to choose plant to spawn
            {
                spawnMe.GetComponent<PlantingMechanic>().mySprite = ListedSprites[Random.Range(0, 5)];
                spawnMe.GetComponent<PlantingMechanic>().score = Random.Range(1, 15);
            }

            Instantiate(spawnMe, thatPos, Quaternion.identity);
            currentObjects++;
        }


    }

    void ChoosePlant()
    {

    }

}
