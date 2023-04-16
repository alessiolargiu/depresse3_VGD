using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{

    public Image equipSlot;
    public TMP_Text equipName;
    public TMP_Text potionNumber;

    public void ChangeEquipment(Image equipImage, TMP_Text equipText)
    {
        equipSlot.sprite = equipImage.sprite;
        equipName.text = equipText.text;
    }

    public void ChangeEquipmentPotion(Image equipImage, TMP_Text equipText, TMP_Text potionNumber)
    {
        equipSlot.sprite = equipImage.sprite;
        equipName.text = equipText.text;
        this.potionNumber.text = potionNumber.text;
    }

}
