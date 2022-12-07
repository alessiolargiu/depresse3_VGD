using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) {
            RaycastHit hitPoint;
            bool raycastRes = Physics.Raycast(transform.position, transform.forward, out (hitPoint), Mathf.Infinity);
            if (raycastRes)
            {
                Debug.Log("Collision at: " + hitPoint.distance);
                Debug.DrawRay(transform.position, transform.forward * 1000, Color.red);
                if (hitPoint.transform.CompareTag("Clone")){
                    Destroy(hitPoint.transform.gameObject);
                }
            }
        }
        
    }
}
