using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;

public class CoffeePlant : MonoBehaviour
{

    [SerializeField] private CoffeeTypeSO coffeeTypeSO;
    private WorldTimeManager worldTimeManager;
    public StorageInventory inventory;

    private bool isHarvestable;

    private int amountHarvestable;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        worldTimeManager = GameObject.Find("WorldTimeManager").GetComponent<WorldTimeManager>();
        inventory = GetComponent<StorageInventory>();
        amountHarvestable = 0;
        timer = 0f;
        isHarvestable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHarvestable)
        {
            Grow();
        }
    }

    private void Grow()
    {
        timer += Time.deltaTime;
        if(timer / worldTimeManager.secondsPerHour >= coffeeTypeSO.growthTimeInHours)
        {
            isHarvestable = true;
            amountHarvestable += coffeeTypeSO.yieldAmount;
        }
    }

    public void RemoveAmount(int amountToRemove, NPCController npc)
    {
        if (amountToRemove <= amountHarvestable)
        {
            amountHarvestable -= amountToRemove;
            npc.inventory.AddResource(coffeeTypeSO, amountToRemove);
        }

        if (amountToRemove > amountHarvestable)
        {
            npc.inventory.AddResource(coffeeTypeSO, amountHarvestable);
            amountHarvestable = 0;
        }
    }
}
