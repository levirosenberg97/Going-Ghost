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
    float startingLag;

    public bool isAlive;
    public float speed;
    public float jumpForce;
    public float landLag;
    public GameObject ghost;

    void Start ()
    {
        startingLag = landLag;

        ghost.transform.position = transform.position;

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

            //Triggers the Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
                //landLag = 0;
            }

            //Checks to see if at the peak of your jump and increases the fall rate;
            if (prevHeight > currHeight && isGrounded == false)
            {
                rb.AddForce(Vector2.down * (jumpForce * 1.1f), ForceMode.Acceleration);
            }
            else
            {
                prevHeight = currHeight;
            }

        }
        if(Input.GetKeyDown(KeyCode.E) && isAlive == true && isGrounded == true)
        {
            isAlive = false;
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
