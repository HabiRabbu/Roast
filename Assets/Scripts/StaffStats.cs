using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffStats : MonoBehaviour
{

    public int hunger;
    public int energy;
    public int money;

    public float timeToDecreaseHunger;
    public float timeToDecreaseEnergy;
    private float timeLeftHunger;
    private float timeLeftEnergy;
    // Start is called before the first frame update
    void Start()
    {
        hunger = Random.Range(20, 80);
        energy = Random.Range(20, 80);
        money = Random.Range(10, 100);
    }

    public void UpdateHunger()
    {
        if (timeLeftHunger > 0)
        {
            timeLeftHunger -= Time.deltaTime;
            return;
        }

        timeLeftHunger = timeToDecreaseHunger;
        hunger += 1;
    }

    public void UpdateEnergy(bool shouldNotUpdateEnergy)
    {
        if (shouldNotUpdateEnergy)
        {
            return;
        }

        if (timeLeftEnergy > 0)
        {
            timeLeftEnergy -= Time.deltaTime;
            return;
        }

        timeLeftEnergy = timeToDecreaseEnergy;
        energy -= 1;
    }
}
