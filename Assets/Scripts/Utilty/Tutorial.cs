using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public int tutorialIndex;
    private GameObject HUD;
    [HideInInspector]
    public bool tutorialShowing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            tutorialShowing = true;
            HUD = GameObject.Find("HUD");
            HUD.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            GameObject.Find("TutorialCanvas").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("TutorialCanvas").transform.GetChild(0).GetChild(tutorialIndex).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && tutorialShowing)
        {
            tutorialShowing = false;
            HUD.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            GameObject.Find("TutorialCanvas").transform.GetChild(0).GetChild(tutorialIndex).gameObject.SetActive(false);
            GameObject.Find("TutorialCanvas").transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
