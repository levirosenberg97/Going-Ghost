﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xInput;
    Rigidbody rb;
    bool isGrounded;
    float currHeight;
    float prevHeight;
    float startingLag;

    public bool isAlive;
    public float speed;
    public float jumpForce;
    public float landLag;
    public GameObject ghost;
    public Animator anim;

    void Start ()
    {
        startingLag = landLag;

        ghost.transform.position = transform.position;

        currHeight = transform.position.y;
        prevHeight = currHeight;

        anim = GetComponent<Animator>();
        isAlive = true;
        rb = GetComponent<Rigidbody>();
	}
	
    void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("isWalking", true);
        transform.position += new Vector3(xInput,0, 0).normalized * Time.deltaTime * speed;
    }


	void Update ()
    {
        currHeight = transform.position.y;

        if (isAlive == true)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                Move();
            }
            else
            {
                anim.SetBool("isWalking", false);
            }

            //Triggers the Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                anim.SetBool("isJumping", true);
                //landLag = 0;
            }

            //Checks to see if at the peak of your jump and increases the fall rate;
            if (prevHeight > currHeight && isGrounded == false)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", true);
                rb.AddForce(Vector2.down * jumpForce, ForceMode.Force);
            }
            else
            {
                anim.SetBool("isFalling", false);
                prevHeight = currHeight;
            }

        }
        if(Input.GetKeyDown(KeyCode.E) && isAlive == true && isGrounded == true)
        {
            isAlive = false;
            anim.SetBool("isDead", true);
            ghost.SetActive(true);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 12 && transform.position.y >= collision.collider.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 12)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
