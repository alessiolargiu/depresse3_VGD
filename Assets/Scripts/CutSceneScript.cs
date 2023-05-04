using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour
{
    private FirstPersonController player;
    private GameObject playerContainer;
    private GameObject HUD;
    private GameObject cutscene;
    private GameObject mission;

    private AudioSource soundSource;
    private AudioClip [] audioClips;
    private int [] cameraPosition;
    private GameObject [] camerasAvailable;

    public AudioSource soundSourceGet;
    public AudioClip [] audioClipsGet;
    public int [] cameraPositionGet;
    public GameObject [] camerasAvailableGet;
    public FirstPersonController playerGet;
    public GameObject playerContainerGet;
    public GameObject HUDGet;
    public GameObject cutsceneGet;
    public GameObject missionGet;
    private MeshRenderer selfRender;

    private static bool hasStarted;


    
    

    // Start is called before the first frame update
    void Start()
    {   if(GetComponent<MeshRenderer>()!=null){
            selfRender = GetComponent<MeshRenderer>();
        } else selfRender=null;
        
        player = playerGet;
        playerContainer = playerContainerGet;
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
    public void DestroySelfCutscene(){
        DestroyObject(gameObject);
    }
    public IEnumerator cutsceneStart(System.Action<bool> callback){
        if(hasStarted==false){


            if(selfRender!=null){
                selfRender.enabled=false;
            }

            hasStarted=true;
            
            //player.SetActive(false);
            
            HUD.SetActive(false);
            cutscene.SetActive(true);
            player.SetActiveInternal(false);
            playerContainer.SetActive(false);

            //

            /*for(int j=0;j<4;j++){
                camerasAvailable[j].SetActive(false);
            }*/

            
            int i=0;

            while(i<audioClips.Length){

                
                soundSource.clip = audioClips[i];
                soundSource.Play();


                for(int j=0;j<camerasAvailable.Length;j++){
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
            playerContainer.SetActive(true);
            player.SetActiveInternal(true);
            callback(true);
            //player.SetActive(true);
            HUD.SetActive(true);
            mission.SetActive(true);
            cutscene.SetActive(false);
            hasStarted=false;
            //yield return "Finito";
            DestroySelfCutscene();
        }
        yield return null;
    }
    
   
}
