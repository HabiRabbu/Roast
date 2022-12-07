using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CoffeeType")]
public class CoffeeTypeSO : ScriptableObject
{
    public string nameString;
    public float growthTimeInHours;
    public int yieldAmount;

    //Strength/Region/Price/Sprite etc
}
