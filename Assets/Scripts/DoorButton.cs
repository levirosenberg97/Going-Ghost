using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public bool isActivated;
    public GameObject door;
    public float openSpeed;

    public Vector3 endPos;


    Vector3 startPos;

    private void Start()
    {
        startPos = door.transform.position;   
    }

    private void Update()
    {
        if (isActivated)
        {
            door.transform.position = Vector2.Lerp(door.transform.position, endPos, Time.deltaTime * openSpeed);
        }
        else if(!isActivated && door.transform.position != startPos)
        {
            door.transform.position = Vector2.Lerp(door.transform.position, startPos, Time.deltaTime * openSpeed);
        }
    }
}
