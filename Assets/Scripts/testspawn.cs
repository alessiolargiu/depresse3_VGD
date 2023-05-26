using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testspawn : MonoBehaviour
{

    private FirstPersonController firstPersonController;
    // Start is called before the first frame update
    void Start()
    {
        //firstPersonController = GameObject.Find("PlayerProtagonista").GetComponent<FirstPersonController>();

    }

    private void Awake()
    {
        firstPersonController = GameObject.Find("PlayerProtagonista").GetComponent<FirstPersonController>();
        firstPersonController.transform.position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
