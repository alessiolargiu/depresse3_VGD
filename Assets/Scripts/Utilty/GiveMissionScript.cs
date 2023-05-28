using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GiveMissionScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string text;
    void Awake()
    {
        GameObject.Find("TestoMissione").GetComponent<TMP_Text>().text=text;
    }
}
