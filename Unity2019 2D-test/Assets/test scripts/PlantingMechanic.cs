using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingMechanic : MonoBehaviour
{

    public GameObject theHole;
    public GameObject plantCreated;
    public bool isTriggered;

    void OnTriggerEnter2D(Collider2D that)
    {
        if (that.gameObject.name.Equals("Plant_Seed(Clone)") && theHole != null) //did the hole and seed touch?
        {
            isTriggered = true;
            Destroy(that.gameObject);                                      //delete the plant and seed
            Destroy(theHole);

            Vector3 tempV = this.transform.position;                        //get my position
            Instantiate(plantCreated, tempV, Quaternion.identity);          //create plant
        }
    }
}
