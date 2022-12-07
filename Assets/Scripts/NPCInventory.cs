using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInventory : Inventory
{
    public int _maxCapacity;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseInventory();
        SetMaxCapacity(_maxCapacity);
    }

    private void SetMaxCapacity(int maximumCapacity)
    {
        maxCapacity = maximumCapacity;
    }

    public void AddResource(CoffeeTypeSO c, int amount)
    {
        int amountInInventory = CheckInventoryCount();
        if (amountInInventory + amount > maxCapacity)
        {
            int amountCanAdd = maxCapacity - amountInInventory;
            inventory[c] += amountCanAdd;
        }
        else
        {
            inventory[c] += amount;
        }
    }

    public void RemoveAllResource()
    {
        List<CoffeeTypeSO> types = new List<CoffeeTypeSO>();
        foreach (CoffeeTypeSO c in inventory.Keys)
        {
            types.Add(c);
        }

        foreach (CoffeeTypeSO c in types)
        {
            inventory[c] = 0;
        }
    }

    public void RemoveResource(CoffeeTypeSO c, int amount)
    {
        if (inventory[c] - amount < 0)
        {
            inventory[c] = 0;
        }
        else
        {
            inventory[c] -= amount;
        }
    }
}
