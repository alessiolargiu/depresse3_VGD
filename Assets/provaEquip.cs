using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class provaEquip : MonoBehaviour
{

    public FirstPersonController player;
    private Weapon prova;
    public Sprite sprite;
    public HUDInventoryWeapon hudWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        prova = new Weapon(20, 10, "Spada", sprite);
        Inventory inv = player.GetInventory();
        inv.AddWeapon(prova);
        hudWeapon.SetInventory(inv);
    }

}
