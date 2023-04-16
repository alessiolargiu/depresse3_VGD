using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangePotion : MonoBehaviour
{
    public Image equipSlot;
    public TMP_Text equipName;
    public TMP_Text potionNumber;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        equip.ChangeEquipmentPotion(equipSlot, equipName, potionNumber);
        foreach (PotionEquip potion in player.GetInventory().GetPotions())
        {
            if (potion.index == index)
            {
                potion.isEquiped = true;
            }
            else
            {
                potion.isEquiped = false;
            }
        }
    }
}
