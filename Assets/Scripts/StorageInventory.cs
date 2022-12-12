using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInventory : Inventory
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

            if (!inventory.ContainsKey(c))
            {
                inventory.Add(c, amountCanAdd);
            }
            else
            {
                inventory[c] += amountCanAdd;
            }
        }
        else
        {
            if (!inventory.ContainsKey(c))
            {
                inventory.Add(c, amount);
            }
            else
            {
                inventory[c] += amount;
            }
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
