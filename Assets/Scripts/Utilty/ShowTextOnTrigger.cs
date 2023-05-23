using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{

    public TMP_Text text;
    private Camera camera;

    private void LateUpdate()
    {
        if(camera == null)
        {
            camera = GameObject.Find("PlayerProtagonista").GetComponentInChildren<Camera>();
        }
        transform.LookAt(transform.position - camera.transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            text.gameObject.SetActive(false);
        }
    }
}
