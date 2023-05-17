using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavePlayerData : MonoBehaviour
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public int currentHealth;
    public float currentStamina;
    public List<int> availableWeapons = new List<int>();
    public List<int> availableShields = new List<int>();
    public List<int> availableChests = new List<int>();
    public List<int> availableHelmets = new List<int>();
    public List<int> availablePotions = new List<int>();

}
