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
    private FirstPersonController player;
    private GameObject HUD;
    private GameObject cutscene;
    private GameObject mission;

    private AudioSource soundSource;
    private AudioClip [] audioClips;
    private cameras [] cameraPosition;
    private GameObject [] camerasAvailable;

    public AudioSource soundSourceGet;
    public AudioClip [] audioClipsGet;
    public cameras [] cameraPositionGet;
    public GameObject [] camerasAvailableGet;
    public FirstPersonController playerGet;
    public GameObject HUDGet;
    public GameObject cutsceneGet;
    public GameObject missionGet;

    private static bool hasStarted;

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
        mission=missionGet;
    }

    // Update is called once per frame
    void Update()
    {
    } 

    public IEnumerator cutsceneStart(System.Action<bool> callback){
        if(hasStarted==false){
            hasStarted=true;
            player.SetActiveInternal(false);
            //player.SetActive(false);
            HUD.SetActive(false);
            cutscene.SetActive(true);

            //

            /*for(int j=0;j<4;j++){
                camerasAvailable[j].SetActive(false);
            }*/

            
            int i=0;

            while(i<audioClips.Length){

                
                soundSource.clip = audioClips[i];
                soundSource.Play();


                for(int j=0;j<4;j++){
                    if(j== ((int) cameraPosition[i])){
                        camerasAvailable[j].SetActive(true);
                    } else camerasAvailable[j].SetActive(false);
                }

                while (soundSource.isPlaying){
                    if(Input.anyKeyDown){
                        i = audioClips.Length;
                        break;
                    }
                    yield return null;
                }

                i++;
                

            }
            player.SetActiveInternal(true);
            callback(true);
            //player.SetActive(true);
            HUD.SetActive(true);
            mission.SetActive(true);
            cutscene.SetActive(false);
            hasStarted=false;
            //yield return "Finito";
        }
        yield return null;
    }
    
   
}
