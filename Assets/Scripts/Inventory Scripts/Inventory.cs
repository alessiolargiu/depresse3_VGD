using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory
{

    private List<Weapon> weapons;
    private List<Shield> shields;
    private List<Potion> potions;
    private List<Armor> helmets;
    private List<Armor> chests;
    private List<Armor> shoes;


    public Inventory()
    {
        weapons = new List<Weapon>();
        shields = new List<Shield>();
        potions = new List<Potion>();
        helmets = new List<Armor>();
        chests = new List<Armor>();
        shoes = new List<Armor>();
    }

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }
    public List<Shield> GetShields()
    {
        return shields;
    }
    public List<Potion> GetPotions()
    {
        return potions;
    }
    public List<Armor> GetHelmets()
    {
        return helmets;
    }
    public List<Armor> GetChests()
    {
        return chests;
    }
    public List<Armor> GetShoes()
    {
        return shoes;
    }

    public void AddWeapon(Weapon item)
    {
        weapons.Add(item);
    }
    public void AddShield(Shield item)
    {
        shields.Add(item);
    }
    public void AddPotion(Potion item)
    {
        potions.Add(item);
    }
    public void AddHelmet(Armor item)
    {
        helmets.Add(item);
    }
    public void AddChest(Armor item)
    {
        chests.Add(item);
    }
    public void AddShoe(Armor item)
    {
        shoes.Add(item);
    }

}
