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
        player = FindObjectOfType<FirstPersonController>();
        equip.ChangeEquipment(equipSlot, equipName, 1);
        foreach (ShieldEquip shield in player.GetInventory().GetShields())
        {
            if (index == 0)
            {
                shield.gameObject.SetActive(false);
            }
            else
            {
                if (shield.index == index)
                {
                    shield.gameObject.SetActive(true);
                }
                else
                {
                    shield.gameObject.SetActive(false);
                }
            }

        }
    }

}
