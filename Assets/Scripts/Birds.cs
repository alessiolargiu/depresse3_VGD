using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    [SerializeField] private int rotationSpeed;
    void Start(){
    }
    // Update is called once per frame
    void Update()
    {   

        transform.Rotate( new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
