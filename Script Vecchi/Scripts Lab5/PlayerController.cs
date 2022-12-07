using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public SerializableVector3 position;
}

[System.Serializable]
public struct SerializableVector3
{
    float x, y, z;

    public SerializableVector3(Vector3 v)
    {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    public Vector3 toVector3()
    {
        return new Vector3(x, y, z);
    }
}

public class PlayerController : MonoBehaviour
{
    //movimento
    private Rigidbody rb;
    private float moveSpeed = 10f;
    private string saveDataPath;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject oldObject = GameObject.Find("Sphere");
        if(oldObject != this.gameObject)
        {
            Destroy(oldObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            save();
        }
        if (Input.GetKeyDown("o")){
            load();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float moveVertical = Input.GetAxis("Vertical") * moveSpeed;
        rb.AddForce(new Vector3(moveHorizontal, 0f, moveVertical));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("NextLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
            if (PlayerPrefs.GetInt("CurrentLevel") == 0)
            {
                Destroy(this.gameObject);
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }

        if (collision.gameObject.CompareTag("PredLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", (SceneManager.GetActiveScene().buildIndex - 1) % SceneManager.sceneCountInBuildSettings);
            if(PlayerPrefs.GetInt("CurrentLevel") == 0)
            {
                Destroy(this.gameObject);
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));

        }

        if (collision.gameObject.CompareTag("MenuLevel"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    void save( ){
        saveDataPath = Application.persistentDataPath + "/data.vgd";

        GameData gamedata = new GameData();
        gamedata.position = new SerializableVector3(this.transform.position);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Open(saveDataPath, FileMode.Create);

        formatter.Serialize(fileStream, gamedata);
    }

    void load()
    {
        if (File.Exists(saveDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream filestream = File.Open(saveDataPath, FileMode.Open);

            GameData gamedata = (GameData)formatter.Deserialize(filestream);

            transform.position = gamedata.position.toVector3();
        }
    }
}
