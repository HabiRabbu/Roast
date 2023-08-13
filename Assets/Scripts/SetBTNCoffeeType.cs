using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetBTNCoffeeType : MonoBehaviour
{
    CoffeeTypeSO coffeeType = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (coffeeType == null)
        {
            string name = transform.Find("TXT_Name").GetComponent<TMP_Text>().text;
            List<CoffeeTypeSO> coffeeTypes = GameObject.Find("CoffeeDesignManager").GetComponent<CoffeeDesignManager>().coffeeTypes;
            foreach (CoffeeTypeSO coffee in coffeeTypes)
            {
                if (name == coffee.nameString)
                {
                    coffeeType = coffee;
                }
            }
        }
    }

    public void selectCoffeeType()
    {
        GameObject.Find("CoffeeDesignPopup(Clone)").GetComponent<CoffeeDesignPopup>().coffeeType = coffeeType;
    }
}
