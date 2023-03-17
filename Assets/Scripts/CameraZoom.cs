using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float movementSpeed = 5f;
    //Vector3 currPos = transform.position;

    void Update()
    {


        transform.position += transform.forward * Time.deltaTime * movementSpeed;

        //output to log the position change
        //Debug.Log(currPos.x);
    }
}
