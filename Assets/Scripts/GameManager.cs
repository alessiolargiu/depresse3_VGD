using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int sensibilita;
    public bool vitaInfinita;
    public bool staminaInfinita;
    public bool fullEquip;

    public void Save()
    {
        Debug.Log("DA FARE MOLTO BENE!!!!!");

        PlayerPrefs.SetInt("sensibilita", sensibilita);

        PlayerPrefs.Save();
    }


}
