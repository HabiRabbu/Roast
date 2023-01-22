using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffStats : MonoBehaviour
{

    public string staffName;
    public int age;

    public int hunger;
    public int energy;
    public int money;

    public int maxJobSatisfaction;
    public int jobSatisfaction;
    public int happiness;
    public int level;

    public float timeToDecreaseHunger;
    public float timeToDecreaseEnergy;
    private float timeLeftHunger;
    private float timeLeftEnergy;
    // Start is called before the first frame update
    void Start()
    {
        staffName = "TestName"; //TODO: Random from list.
        age = Random.Range(16, 70);

        hunger = Random.Range(20, 80);
        energy = Random.Range(20, 80);
        money = Random.Range(10, 100);

        //Think about how you're going to do these when better staff are available to the player
        happiness = 100;
        level = 1;
        maxJobSatisfaction = level * 100;
        jobSatisfaction = 0;
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
