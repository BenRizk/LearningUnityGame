using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public int timer = 0;
    public int killTime = 500;
    public string myClone;


    // Update is called once per frame
    void Update()
    {
        if(timer > killTime)
        {
            timer = 0;
            if(this != null)
            {
                if (this.gameObject.name.Equals(myClone))
                {
                    Destroy(this.gameObject);
                }
            }
        }
        else if(this.gameObject.name.Equals(myClone))
        {
            timer++;
        }
    }
}
