using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float xInput;
    Rigidbody rb;
    bool isGrounded;
    float currHeight;
    float prevHeight;
    Vector2 startingGhostPos;

    public bool isAlive;
    public float speed;
    public float jumpForce;
    public float gravity;
    public GameObject ghost;

    void Start ()
    {
        ghost.transform.position = transform.position;
        //startingGhostPos = ghost.transform.position;
        currHeight = transform.position.y;
        prevHeight = currHeight;
        isAlive = true;
        rb = GetComponent<Rigidbody>();
	}
	
    void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(xInput,0, 0).normalized * Time.deltaTime * speed;
    }


	void Update ()
    {
        currHeight = transform.position.y;

        if (isAlive == true)
        {
            Move();
        }

        //Triggers the Jump
        if(Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }

        //Checks to see if at the peak of your jump and increases the fall rate;
        if( prevHeight > currHeight && isGrounded == false)
        {
            rb.AddForce(Vector2.down * (jumpForce * 2), ForceMode.Acceleration);
        }
        else
        {
            prevHeight = currHeight;
        }

        if(Input.GetKey(KeyCode.E) && isAlive == true)
        {
            isAlive = false;
        }
        //else if(Input.GetKey(KeyCode.E) && isAlive == false &&
        //        ghost.transform.position.x > startingGhostPos.x - 3 && ghost.transform.position.x < startingGhostPos.x + 3)
        //{
        //    isAlive = true;
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
        {
            isGrounded = false;
        }
    }

}
