using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerRotate : MonoBehaviour
{

    public Transform hips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, hips.eulerAngles.y, transform.eulerAngles.z);
        //var eulerRotation = Quaternion.Euler(transform.eulerAngles.x, hips.eulerAngles.y, transform.eulerAngles.z);
 
        transform.rotation = Quaternion.Euler(eulerRotation);

        //transform.rotation = Quaternion.Lerp(transform.rotation, eulerRotation, Time.deltaTime);

        
    }
}
