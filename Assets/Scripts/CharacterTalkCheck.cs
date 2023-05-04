using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTalkCheck : MonoBehaviour
{
    public GameObject [] cameras;
    private Animator anim;
    // Start is called before the first frame update
    void Start(){
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        int actives=0;
        for(int i=0; i<cameras.Length; i++){
            if(cameras[i].activeSelf){
                anim.SetBool("Action", true);
                actives++;
            } else if(actives==0) {anim.SetBool("Action", false);}
        }
    }
}
