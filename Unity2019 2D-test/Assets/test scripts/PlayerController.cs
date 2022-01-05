using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //movement variables
    public Rigidbody2D myBody;
    public float mySpeed = 40f;
    //interaction variables
    public bool isOverObject = false;
    public bool holding = false;
    Transform temp;
    public GameObject currentObj;
    public GameObject heldObj;
    public GameObject digHoles;
    public GameObject dummyObject;
    //animation variables
    Vector2 moves;
    public Animator animator;
    float holdDir = 0;
    //hunger variables
    public int currentHunger;
    public HungerBar myHunger;
    int time;

    void Start() // called when game beings
    {
        currentHunger = 50;
        myHunger.SetHunger(currentHunger);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {   //code to update position
        moves.x = Input.GetAxisRaw("Horizontal") * mySpeed;
        moves.y = Input.GetAxisRaw("Vertical") * mySpeed;
        // code to update animator
        animator.SetFloat("horizontal", moves.x);
        animator.SetFloat("Vertical", moves.y);
        animator.SetFloat("Speed", moves.sqrMagnitude);
        if(moves.x != 0) 
        {
            holdDir = moves.x;
        }
        
        animator.SetFloat("LastPosition", holdDir);

        //code for state handling

        if(heldObj == null && holding)      // always check for heldobj deletion --> if deleted then reset variables
        {
            holding = false;                    
            heldObj = null;                     
            currentObj = null;
        }


        //code for interaction
        if(Input.GetButtonDown("Interact"))
        {
            if (holding)                        // am i holding item? --> drop it
            {
                if(heldObj != null)             //check if my held item still exists(held item can be destroyed by other means)
                {
                    heldObj.transform.parent = temp;    // no longer parent(will no longer follow player)                   
                }
                holding = false;                    // i am not holding
                heldObj = null;                     // reset holding variable
                currentObj = null;
                return;
                
            }
            if (isOverObject)                   // am I over object?
            {     
                if(currentObj != null)          //did my current obj get deleted?
                {
                    if (currentObj.name.Equals("Plant_Seed(Clone)")) //is it a seed --> pick it up
                    {
                        currentObj.transform.parent = this.transform;
                        heldObj = currentObj;
                        holding = true;
                        return;
                    }
                    if (currentObj.name.Equals("apple") || currentObj.name.Equals("apple(Clone)")) //is it an apple --> eat it
                    {
                        if (currentHunger < 100)
                        {
                            currentHunger += 10;
                        }
                        myHunger.SetHunger(currentHunger);
                        Destroy(currentObj);
                    }
                }
                else   // otherwise dig a hole
                {
                    Vector3 tempV = this.transform.position;//get my position
                    Instantiate(digHoles, tempV, Quaternion.identity);//dig hole at position
                }

            }
            else   // otherwise dig a hole
            {
                Vector3 tempV = this.transform.position;//get my position
                Instantiate(digHoles, tempV, Quaternion.identity);//dig hole at position
            }
        }
        //every 5 seconds lose health (300 = 5 seconds)
        if(time < 300)
        {
            time++;
        }
        else
        {
            time = 0;
            currentHunger -= 1;
            myHunger.SetHunger(currentHunger);
        }
        
    }

    void FixedUpdate()
    {
        myBody.MovePosition(myBody.position + moves * mySpeed);
    }

    void OnTriggerEnter2D(Collider2D that){
        if (that.CompareTag("IsInteractable")) //when i'm over an object, save object i'm over
        {
            isOverObject = true;
            currentObj = that.gameObject;
        }
    }
    
    void OnTriggerExit2D(Collider2D that)
    {
        if (that.CompareTag("IsInteractable") && isOverObject == true) // did I stop being over object
        {
            isOverObject = false;
        }
    }

}
