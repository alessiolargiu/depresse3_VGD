using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSecondColosseo : MonoBehaviour
{
    private GameObject GameManager;
    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(findManager());
    }

    IEnumerator findManager(){
        while(GameObject.Find("GameManager")==null){
            yield return null;
        }
        GameManager = GameObject.Find("GameManager");
        Resources.FindObjectsOfTypeAll<HealthBarGigante>()[0].gameObject.SetActive(true);
        GameObject.Find("Compass").SetActive(false);
        GameManager.GetComponent<GameManager>().StartLoading("ArenaNight", true);
        yield return null;
    }
}
