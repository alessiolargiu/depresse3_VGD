using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public GameObject helmetBG;
    public static bool helmetInvIsShowing = false;
    public GameObject chestBG;
    public static bool chestInvIsShowing = false;
    public GameObject swordBG;
    public static bool swordInvIsShowing = false;
    public GameObject shieldBG;
    public static bool shieldInvIsShowing = false;
    public GameObject potionBG;
    public static bool potionInvIsShowing = false;

    public void ShowInventory(GameObject button)
    {
        switch (button.name)
        {
            case "Helmet":
                if (helmetInvIsShowing)
                {
                    helmetBG.SetActive(false);
                }
                else
                {
                    helmetBG.SetActive(true);
                    chestBG.SetActive(false);
                    swordBG.SetActive(false);
                    shieldBG.SetActive(false);
                    potionBG.SetActive(false);
                }
                break;
            case "Chest":
                if (helmetInvIsShowing)
                {
                    chestBG.SetActive(false);
                }
                else
                {
                    helmetBG.SetActive(false);
                    chestBG.SetActive(true);
                    swordBG.SetActive(false);
                    shieldBG.SetActive(false);
                    potionBG.SetActive(false);
                }
                break;
            case "Weapon":
                if (helmetInvIsShowing)
                {
                    swordBG.SetActive(false);
                }
                else
                {
                    helmetBG.SetActive(false);
                    chestBG.SetActive(false);
                    swordBG.SetActive(true);
                    shieldBG.SetActive(false);
                    potionBG.SetActive(false);
                }
                break;
            case "Shield":
                if (helmetInvIsShowing)
                {
                    shieldBG.SetActive(false);
                }
                else
                {
                    helmetBG.SetActive(false);
                    chestBG.SetActive(false);
                    swordBG.SetActive(false);
                    shieldBG.SetActive(true);
                    potionBG.SetActive(false);
                }
                break;
            case "Potion":
                if (helmetInvIsShowing)
                {
                    potionBG.SetActive(false);
                }
                else
                {
                    helmetBG.SetActive(false);
                    chestBG.SetActive(false);
                    swordBG.SetActive(false);
                    shieldBG.SetActive(false);
                    potionBG.SetActive(true);
                }
                break;
        }

    }

}
