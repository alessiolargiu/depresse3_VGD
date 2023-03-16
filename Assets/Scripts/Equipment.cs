using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{

    public Image equipSlot;

    public void ChangeEquipment(Image equipImage)
    {
        equipSlot.sprite = equipImage.sprite;
    }

}
