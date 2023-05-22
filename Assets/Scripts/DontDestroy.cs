using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestroy>()[i].name == gameObject.name)
                {
                    
                    Destroy(gameObject);
                    if(GameObject.Find("LoadingScreen") != null)
                    {
                        GameObject.Find("LoadingScreen").SetActive(false);
                    }
                }
            }
        }

        DontDestroyOnLoad(gameObject);
    }
}
