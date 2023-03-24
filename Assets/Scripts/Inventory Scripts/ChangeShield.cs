using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShield : MonoBehaviour
{

    public Image equipSlot;
    public TMP_Text equipName;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        equip.ChangeEquipment(equipSlot, equipName);
        foreach (ShieldEquip shield in player.GetInventory().GetShields())
        {
            if (shield.index == index)
            {
                shield.gameObject.SetActive(true);
            }
        }
    }

}
