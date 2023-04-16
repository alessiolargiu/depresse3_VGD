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
        equip.ChangeEquipment(equipSlot, equipName);
        foreach(HelmetEquip helmet in player.GetInventory().GetHelmets())
        {
            if(index == 0)
            {
                helmet.gameObject.SetActive(false);
            }
            else
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




}
