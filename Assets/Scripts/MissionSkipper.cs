using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionSkipper : MonoBehaviour
{

    public void MissionSkipperLaunch(){
        StartCoroutine(SearchSpawnPoint());
    }

    IEnumerator SearchSpawnPoint(){
        while(SceneManager.GetActiveScene().name != "TestALessioMappa")
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);
        FindObjectOfType<FirstPersonController>().transform.position = GameObject.Find("PlayerSpawnPoint").transform.position;
    }
}
