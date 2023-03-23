using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cameras {
    side1,
    side2,
    back1,
    back2,
    zoom
}
public class CutSceneScript : MonoBehaviour
{
    private GameObject player;
    private GameObject HUD;
    private GameObject cutscene;

    private AudioSource soundSource;
    private AudioClip [] audioClips;
    private cameras [] cameraPosition;
    private GameObject [] camerasAvailable;

    public AudioSource soundSourceGet;
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
        soundSource = soundSourceGet;
        audioClips = audioClipsGet;
        cameraPosition = cameraPositionGet;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    public IEnumerator cutsceneStart(System.Action<bool> callback){

        
        Debug.Log("stronxo");
        player.SetActive(false);
        HUD.SetActive(false);
        cutscene.SetActive(true);

        //

        /*for(int j=0;j<4;j++){
            camerasAvailable[j].SetActive(false);
        }*/

        
        int i=0;

        Debug.Log("IL VALORE DI AUDIOCLIPS è " +audioClips.Length);

        while(i<audioClips.Length){


            soundSource.clip = audioClips[i];
            soundSource.Play();
            Debug.Log("IL VALORE DI I è  : " + i);


            for(int j=0;j<4;j++){
                if(j== ((int) cameraPosition[i])){
                    camerasAvailable[j].SetActive(true);
                } else camerasAvailable[j].SetActive(false);
            }

            while (soundSource.isPlaying){
                yield return null;
            }

            i++;
            

        }
        callback(true);
        player.SetActive(true);
        HUD.SetActive(true);
        cutscene.SetActive(false);

        //yield return "Finito";

        
    }
    
   
}
