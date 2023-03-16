using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory
{

    private List<Item> weapons;
    private List<Item> shields;
    private List<Item> potions;
    private List<Item> helmets;
    private List<Item> chests;
    private List<Item> shoes;


    public Inventory()
    {
        weapons = new List<Item>();
        shields = new List<Item>();
        potions = new List<Item>();
        helmets = new List<Item>();
        chests = new List<Item>();
        shoes = new List<Item>();
    }

}
