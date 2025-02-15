using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHelmet : MonoBehaviour
{

    public Image equipSlot;
    public TMP_Text equipName;
    public int index;
    public FirstPersonController player;


    public void ChangeEquipmentInfo(Equipment equip)
    {
        player = FindObjectOfType<FirstPersonController>();
        equip.ChangeEquipment(equipSlot, equipName, -1);
        foreach(HelmetEquip helmet in player.GetInventory().GetHelmets())
        {
            if (helmet.index == index)
            {
                helmet.gameObject.SetActive(true);
            }
            else
            {
                helmet.gameObject.SetActive(false);
            }
        }
    }




}
