using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenCameraIsActive : MonoBehaviour
{
    public GameObject cutscene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        if(cutscene!=null){
            if(cutscene.activeSelf){
                meshDisabler(transform);
            } else {
                meshEnabler(transform);
            }
        }
        
    }

    private void meshDisabler(Transform t) {
        if (t.childCount > 0) {
            foreach (Transform child in t) {
                meshDisabler(child);
            }
        }
        if(t.gameObject.GetComponent<Renderer>()!=null){
            t.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    private void meshEnabler(Transform t) {
        if (t.childCount > 0) {
            foreach (Transform child in t) {
                meshEnabler(child);
            }
        }
        if(t.gameObject.GetComponent<Renderer>()!=null){
            t.gameObject.GetComponent<Renderer>().enabled = true;
        }
        
    }
}
