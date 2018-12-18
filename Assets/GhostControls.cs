using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControls : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    Vector2 startingPos;
    float currHeight;
    float prevHeight;
    float xInput;
    Rigidbody rb;
    bool isGrounded;
    GameObject player;
    PlayerController playerController;

    void Start()
    {
        currHeight = transform.position.y;
        prevHeight = currHeight;
        startingPos = transform.position;

        player = transform.parent.gameObject;
        playerController = transform.parent.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(xInput, 0, 0).normalized * Time.deltaTime * speed;
    }


    void Update()
    {
        if (playerController.isAlive == false)
        {
            //Triggers the Jump
            if (Input.GetKey(KeyCode.Space) && isGrounded == true)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            }

            //Checks to see if at the peak of your jump and increases the fall rate;
            if (prevHeight > currHeight && isGrounded == false)
            {
                rb.AddForce(Vector2.down * (jumpForce * 2), ForceMode.Acceleration);
            }
            else
            {
                prevHeight = currHeight;
            }


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8)
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
