using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControls : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float landLag;
    public Animator anim;

    float startingLag;
    float currHeight;
    float prevHeight;
    float xInput;
    Rigidbody rb;
    bool isGrounded;
    GameObject player;
    PlayerController playerController;

    void Start()
    {
        startingLag = landLag;

        anim = GetComponent<Animator>();

        currHeight = transform.position.y;
        prevHeight = currHeight;

        player = transform.parent.gameObject;
        playerController = transform.parent.GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }


    void Move()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        anim.SetBool("isWalking", true);
        transform.position += new Vector3(xInput, 0, 0).normalized * Time.deltaTime * speed;
    }


    void Update()
    {
        currHeight = transform.position.y;
        if (playerController.isAlive == false)
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

        if (Input.GetKeyDown(KeyCode.E) && playerController.isAlive == false &&
            transform.position.x > player.transform.position.x - 4 && transform.position.x < player.transform.position.x + 4)
        {
            playerController.isAlive = true;
        }
        if(playerController.isAlive == true)
        {
            transform.position = player.transform.position;
            playerController.anim.SetBool("isDead", false);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 13 && transform.position.y >= collision.collider.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == 8 || collision.collider.gameObject.layer == 13)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Light")
        {
            playerController.isAlive = true;
        }
    }
}
