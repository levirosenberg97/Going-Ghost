using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.AddForce(Vector2.down * (15 * 2), ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DoorButton")
        {
            other.GetComponent<DoorButton>().isActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DoorButton")
        {
            other.GetComponent<DoorButton>().isActivated = false;
        }
    }
}
