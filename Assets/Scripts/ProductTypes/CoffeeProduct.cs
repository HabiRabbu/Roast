using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeProduct : MonoBehaviour
{
    public string Name { get; set; }
    public CoffeeTypeSO coffeeType;
    public float Quality { get; set; }
    //public float Complexity { get; set; }
    //public float Freshness { get; set; }
    //public float RoastLevel { get; set; }
    //public float Packaging { get; set; }
    //public float Rating { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private float CalculateRating()
    {
        //Add randomisation?
        float rating = (Quality + Packaging) / 2;
        return rating;
    }
    */
}
