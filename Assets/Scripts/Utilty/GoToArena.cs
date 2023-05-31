using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToArena : MonoBehaviour
{

    public GameObject minimap;
    public GameObject layerEquip;

    public void EnterArena()
    {
        //FindObjectOfType<GameManager>().StartLoading("ArenaDay", true);
        minimap.SetActive(true);
        layerEquip.SetActive(true);
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
