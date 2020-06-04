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
        //code for interaction
        if(Input.GetButtonDown("Interact"))
        {
            if (holding)                        // am i holding? --> drop it
            {
                heldObj.transform.parent = temp;    // no longer parent
                holding = false;                    // i am not holding
                heldObj = null;                     // reset holding variable
                return;
            }
            if (isOverObject)                   // am I over object?
            {
                if (currentObj.name.Equals("Plant_Seed(Clone)")) //is it a seed --> pick it up
                { 
                currentObj.transform.parent = this.transform;
                heldObj = currentObj;
                holding = true;
                return;
                }
                if(currentObj.name.Equals("apple") || currentObj.name.Equals("apple(Clone)")) //is it an apple --> eat it
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
        //every 5 seconds lose health
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
        if (that.CompareTag("IsInteractable")) //say i'm over an object and save object i'm over
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
