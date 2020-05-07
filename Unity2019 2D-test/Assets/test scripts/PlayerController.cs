using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public CharacterController2D myControl;
    public Rigidbody2D myBody;
    public float mySpeed = 40f;

    public bool isOverObject = false;
    public bool holding = false;
    Transform temp;

    public GameObject currentObj;
    public GameObject heldObj;
    public GameObject digHoles;

    Vector2 moves;
    public Animator animator;
    float holdDir = 0;
    // Update is called once per frame
    void Update()
    {
        moves.x = Input.GetAxisRaw("Horizontal") * mySpeed;
        moves.y = Input.GetAxisRaw("Vertical") * mySpeed;

        animator.SetFloat("horizontal", moves.x);
        animator.SetFloat("Vertical", moves.y);
        animator.SetFloat("Speed", moves.sqrMagnitude);
        if(moves.x != 0)
        {
            holdDir = moves.x;
        }
        
        animator.SetFloat("LastPosition", holdDir);

        if(Input.GetButtonDown("Interact"))
        {
            if (holding)
            {
                currentObj.transform.parent = temp;
                holding = false;
                return;
            }
            if (isOverObject)
            {
                currentObj.transform.parent = this.transform;
                holding = true;
                return;
            }
            else
            {
                Vector3 tempV = this.transform.position;
                Instantiate(digHoles, tempV, Quaternion.identity);
            }
        }
       

    }

    void FixedUpdate()
    {
        myBody.MovePosition(myBody.position + moves * mySpeed);
    }

    void OnTriggerEnter2D(Collider2D that){
        if (that.CompareTag("IsInteractable"))
        {
            isOverObject = true;
            currentObj = that.gameObject;
        }
    }
    
    void OnTriggerExit2D(Collider2D that)
    {
        if (that.CompareTag("IsInteractable") && isOverObject == true)
        {
            isOverObject = false;
        }
    }

}
