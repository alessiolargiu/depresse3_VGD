using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    // Start is called before the first frame update



    void Start()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.time = Random.Range(0f, source.clip.length);
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
