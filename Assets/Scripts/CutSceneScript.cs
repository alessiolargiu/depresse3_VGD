using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cameras {
    side1,
    side2,
    back1,
    back2
}
public class CutSceneScript : MonoBehaviour
{

    public static GameObject player;
    public static GameObject HUD;
    public static GameObject cutscene;
    public static GameObject side;
    public static GameObject side2;
    public static GameObject back;
    public static GameObject back2;

    public static AudioSource char1;
    public static AudioSource char2;
    public static AudioClip [] audioClips;
    public static cameras [] cameraPosition;

    public AudioSource char1Get;
    public AudioSource char2Get;
    public AudioClip [] audioClipsGet;
    public cameras [] cameraPositionGet;

    public GameObject playerGet;
    public GameObject HUDGet;
    public GameObject cutsceneGet;
    public GameObject sideGet;
    public GameObject side2Get;
    public GameObject backGet;
    public GameObject back2Get;
    // Start is called before the first frame update
    void Start()
    {
        player = playerGet;
        HUD = HUDGet;
        cutscene = cutsceneGet;
        side = sideGet;
        side2 = side2Get;
        back =  backGet;
        back2 = back2Get;
        char1 = char1Get;
        char2 = char2Get;
        audioClips = audioClipsGet;
        cameraPosition = cameraPositionGet;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public static void cutsceneStart(){
        Debug.Log("stronxo");
        player.SetActive(false);
        HUD.SetActive(false);
        cutscene.SetActive(true);

        for(i=0; i<=audioClips.Lenght; i++){
            char1.PlayOneShot(audioClips[i], 1f);
            switch(cameraPosition[i]){
                case 0:
                    side.SetActive(true);
                    break;
                case 1:
                    side1.SetActive(true);
                    break;
                case 2:
                    back.SetActive(true);
                    break;
                case 2:
                    back.SetActive(true);
                    break;

                
            }
        }

        

    }
}
