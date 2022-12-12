using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TL.Core;
using TL.UtilityAI;

public enum GrowthState
{
    needsWater,
    isGrowing,
    harvestable
}

public class CoffeePlant : WorkObject
{

    [SerializeField] private CoffeeTypeSO coffeeTypeSO;
    private WorldTimeManager worldTimeManager;
    public StorageInventory inventory;
    public ActionManager actionManager;

    private int amountHarvestable;

    private float timer;

    public int workAmountNeeded;

    public GrowthState currentGrowthState { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
        worldTimeManager = GameObject.Find("WorldTimeManager").GetComponent<WorldTimeManager>();
        inventory = GetComponent<StorageInventory>();
        amountHarvestable = 0;
        timer = 0f;
        workAmountNeeded = 0;
        isWorkAvailable = false;

        currentGrowthState = GrowthState.needsWater;
    }

    // Update is called once per frame
    void Update()
    {
        GrowthTick();
    }

    private void GrowthTick()
    {
        if (currentGrowthState == GrowthState.needsWater)
        {
            if (!isWorkAvailable)
            {
                workAmountNeeded = workPerTask;
                isWorkAvailable = true;
                actionManager.AddWorkTask(1);
            }

            if (workAmountNeeded == 0)
            {
                currentGrowthState = GrowthState.isGrowing;
                isWorkAvailable = false;
                actionManager.AddWorkTask(-1);
            }
        }
        else if (currentGrowthState == GrowthState.isGrowing)
        {
            timer += Time.deltaTime;
            if (timer / worldTimeManager.secondsPerHour >= coffeeTypeSO.growthTimeInHours)
            {
                currentGrowthState = GrowthState.harvestable;
            }

        }
        else if (currentGrowthState == GrowthState.harvestable)
        {
            if (!isWorkAvailable)
            {
                workAmountNeeded = workPerTask;
                isWorkAvailable = true;
                actionManager.AddWorkTask(1);
            }

            if (workAmountNeeded == 0)
            {
                amountHarvestable += coffeeTypeSO.yieldAmount;
                currentGrowthState = GrowthState.needsWater;
                isWorkAvailable = false;
                actionManager.AddWorkTask(-1);
            }
        }
    }

    public void WorkAction(NPCController npc)
    {
        Debug.Log("I'm doing one point of work");

        if (amountHarvestable > 0)
        {
            RemoveAmount(amountHarvestable, npc);
            Debug.Log("My inventory is " + npc.inventory.HowFullIsStorage() + "% full.");
        }

        if (isWorkAvailable)
        {
            workAmountNeeded -= 1;
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
