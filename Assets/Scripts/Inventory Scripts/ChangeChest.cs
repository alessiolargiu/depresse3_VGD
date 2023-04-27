using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeChest : MonoBehaviour
{
    public Image equipSlot;
    public TMP_Text equipName;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        equip.ChangeEquipment(equipSlot, equipName, -1);
        foreach (ChestEquip chest in player.GetInventory().GetChests())
        {
            if (chest.index == index)
            {
                chest.gameObject.SetActive(true);
            }
            else
            {
                chest.gameObject.SetActive(false);
            }
            
            
        }
    }
}
