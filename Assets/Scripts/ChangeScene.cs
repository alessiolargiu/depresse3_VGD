using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("player")){
            if(sceneName == "TestALessioMappa")
            {
                if(GameObject.Find("Layer equip")==null && GameObject.Find("Minimap Background")==null){
                    GameObject.Find("HUD").transform.Find("Layer equip").gameObject.SetActive(true);
                    GameObject.Find("HUD").transform.Find("Minimap Backgruond").gameObject.SetActive(true);
                }
                GameObject.Find("GameManager").GetComponent<GameManager>().StartLoading(sceneName, true);
            }
            if(sceneName == "ArenaDay")
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameObject.Find("GoToArenaCanvas").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("Contatore").GetComponent<TMP_Text>().text="";
            }
        }
    }
   
}
