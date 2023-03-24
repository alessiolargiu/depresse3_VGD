using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePotion : MonoBehaviour
{
    public Image equipSlot;
    public TMP_Text equipName;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        equip.ChangeEquipment(equipSlot, equipName);
        foreach (PotionEquip potion in player.GetInventory().GetPotions())
        {
            if (potion.index == index)
            {
                potion.gameObject.SetActive(true);
            }
        }
    }
}
