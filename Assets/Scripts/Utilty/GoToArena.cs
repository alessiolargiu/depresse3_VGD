using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToArena : MonoBehaviour
{
    
    public void EnterArena()
    {
        GameObject.Find("GoTo").SetActive(false);
        FindObjectOfType<GameManager>().StartLoading("ArenaDay", true);
    }

    public void StayInMap()
    {
        GameObject.Find("GoTo").SetActive(false);
        if(FindObjectOfType<PauseMenu>() != null)
        {
            FindObjectOfType<PauseMenu>().ResumeGame();
        }

        //FindObjectOfType<FirstPersonController>().gameObject.transform.position = GameObject.Find("SpawnFuoriArena").transform.position;
    }

}
