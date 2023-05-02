using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    public bool getAllEquip;
    public bool infiniteStamina;

    public FirstPersonController player;
    public GameObject playerModel;
    public List<HelmetEquip> helmetList;
    public List<ChestEquip> chestList;
    public List<WeaponEquip> weaponList;
    public List<ShieldEquip> shieldList;
    public List<PotionEquip> potionList;


    private void Update()
    {/*

        player.infiniteStamina = this.infiniteStamina;


        if (getAllEquip)
        {
            foreach(HelmetEquip item in helmetList)
            {
                //item.transform.position = player.transform.position;
            }

            foreach (ChestEquip item in chestList)
            {
                //item.transform.position = player.transform.position;
            }

            foreach (WeaponEquip item in weaponList)
            {
                item.transform.position = player.transform.position;
            }

            foreach (ShieldEquip item in shieldList)
            {
                item.transform.position = player.transform.position;
            }

            foreach (PotionEquip item in potionList)
            {
                item.transform.position = player.transform.position;
            }
        }*/
    }

}
