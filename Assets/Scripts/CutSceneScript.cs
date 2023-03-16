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

    public static AudioSource char1;
    public static AudioSource char2;
    public static AudioClip [] audioClips;
    public static cameras [] cameraPosition;
    public static GameObject [] camerasAvailable;

    public AudioSource char1Get;
    public AudioSource char2Get;
    public AudioClip [] audioClipsGet;
    public cameras [] cameraPositionGet;
    public GameObject [] camerasAvailableGet;

    public GameObject playerGet;
    public GameObject HUDGet;
    public GameObject cutsceneGet;

    // Start is called before the first frame update
    void Start()
    {
        player = playerGet;
        HUD = HUDGet;
        cutscene = cutsceneGet;
        camerasAvailable = camerasAvailableGet;
        char1 = char1Get;
        char2 = char2Get;
        audioClips = audioClipsGet;
        cameraPosition = cameraPositionGet;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public static IEnumerator cutsceneStart(){
        Debug.Log("stronxo");
        player.SetActive(false);
        HUD.SetActive(false);
        cutscene.SetActive(true);

        //

        /*for(int j=0;j<4;j++){
            camerasAvailable[j].SetActive(false);
        }*/

        
                yield return null;
        



        for(int i=0; i<audioClips.Length; i++){

            
            char1.clip = audioClips[i];
            char1.Play();
            Debug.Log("IL VALORE DI I è  : " + i);


            for(int j=0;j<4;j++){
                if(j== ((int) cameraPosition[i])){
                    camerasAvailable[j].SetActive(true);
                } else camerasAvailable[j].SetActive(false);
            }
            /*switch((int) cameraPosition[i]){
                case 0:
                    side.SetActive(true);
                    side2.SetActive(false);
                    back.SetActive(false);
                    back2.SetActive(false);
                    break;
                case 1:
                    side2.SetActive(true);
                    side.SetActive(false);
                    back.SetActive(false);
                    back2.SetActive(false);
                    break;
                case 2:
                    back.SetActive(true);
                    side2.SetActive(false);
                    side.SetActive(false);
                    back2.SetActive(false);
                    break;
                case 3:
                    back2.SetActive(true);
                    side2.SetActive(false);
                    back.SetActive(false);
                    side.SetActive(false);
                    break;      
            }*/
            yield return new WaitForSeconds(audioClips[i].length);
            /*
            while (char1.isPlaying){
                yield return null;
            }*/
            

        }

        
    }
    
   
}
