using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlVideo : MonoBehaviour
{
    public GameObject [] cameras;
    public GameObject video;
    // Start is called before the first frame update
    void Start(){
        video.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        int actives=0;
        for(int i=0; i<cameras.Length; i++){
            if(cameras[i].activeSelf){
                video.SetActive(true);
                actives++;
            } else if(actives==0) {
                video.SetActive(false);
            }
        }
    }
}