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
    private FirstPersonController playerGet;
    private GameObject playerContainerGet;
    private GameObject HUDGet;
    public GameObject cutsceneGet;
    public GameObject missionGet;
    private MeshRenderer selfRender;

    private static bool hasStarted;


    public bool changeSpawn;
    public Transform playerT;
    public Transform spawn;


    public bool missionCheck;

    private bool skippable=false;
    
    void Start(){
        
    }
    // Start is called before the first frame update
    void Awake()
    {   if(GetComponent<MeshRenderer>()!=null){
            selfRender = GetComponent<MeshRenderer>();
        } else selfRender=null;


        playerGet = GameObject.Find("PlayerProtagonista").GetComponent<FirstPersonController>();
        playerContainerGet = GameObject.Find("PlayerContainer");
        playerT =  GameObject.Find("PlayerProtagonista").transform;
        HUDGet = GameObject.Find("HUD");


        player = playerGet;
        playerContainer = playerContainerGet;
        HUD = HUDGet;
        cutscene = cutsceneGet;
        camerasAvailable = camerasAvailableGet;
        soundSource = soundSourceGet;
        audioClips = audioClipsGet;
        cameraPosition = cameraPositionGet;

        if(missionCheck){
            mission=missionGet;
        }
        
        

        
    }

    // Update is called once per frame
    void Update()
    {

    } 
    public void DestroySelfCutscene(){

        if(changeSpawn){
            playerT.position=spawn.position;
        }
        
        if(cutscene==gameObject){
            DestroyObject(gameObject);
        } else {
            DestroyObject(cutscene);
            DestroyObject(gameObject);
        }

        
        
    }

    public IEnumerator countTimeToSkip(float time){
        yield return new WaitForSeconds(time);
        skippable=true;
    }

    public IEnumerator cutsceneStart(System.Action<bool> callback){
        if(hasStarted==false){

            StartCoroutine(countTimeToSkip(2));
            if(selfRender!=null){
                selfRender.enabled=false;
            }

            hasStarted=true;
            
            //player.SetActive(false);
            

            cutscene.SetActive(true);
            HUD.SetActive(false);
            
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
                    if(Input.anyKeyDown && skippable){
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
            if(missionCheck){
                mission.SetActive(true);
            }
            cutscene.SetActive(false);
            hasStarted=false;
            //yield return "Finito";
            DestroySelfCutscene();
        }
        yield return null;
    }
    
   
}
