using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShoe : MonoBehaviour
{
    public Image equipSlot;
    public TMP_Text equipName;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        equip.ChangeEquipment(equipSlot, equipName);
        foreach (ShoesEquip shoe in player.GetInventory().GetShoes())
        {
            if (shoe.index == index)
            {
                shoe.gameObject.SetActive(true);
            }
        }
    }
}
