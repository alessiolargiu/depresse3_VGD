using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetInfo : MonoBehaviour
{
    private string name;
    private int index;
    public HelmetEquip helmetEquip;

    private void Start()
    {
        Armor helmet = helmetEquip.GetHelmet();
        name = helmet.GetName();
        index = helmet.GetIndex(Armor.ArmorType.Helmet);
    }
}
