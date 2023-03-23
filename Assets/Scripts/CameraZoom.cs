using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float movementSpeed = 0.2f;
    Vector3 originalPos;
    Vector3 newPos;
    //Vector3 currPos = transform.position;

    void Start(){
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    void Update()
    {
        Debug.Log("CAZZO PALLE CULO" + this.enabled);
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        newPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

        if(originalPos.x-newPos.x>=2){
            gameObject.transform.position = originalPos;
        }

    }
}


