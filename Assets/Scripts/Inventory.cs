using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public Dictionary<CoffeeTypeSO, int> inventory { get; set; }
    public int maxCapacity { get; set; }

    public void InitialiseInventory()
    {
        inventory = new Dictionary<CoffeeTypeSO, int>();
    }


    public int CheckInventoryCount()
    {
        int sum = 0;
        foreach (CoffeeTypeSO c in inventory.Keys)
        {
            sum += inventory[c];
        }
        return sum;
    }

    public bool DoesInventoryHaveItems()
    {
        foreach (CoffeeTypeSO c in inventory.Keys)
        {
            if (inventory[c] > 0)
            {
                return true;
            }
        }
        return false;
    }

    public float HowFullIsStorage()
    {
        float total = CheckInventoryCount();
        return total / maxCapacity;
    }
}
