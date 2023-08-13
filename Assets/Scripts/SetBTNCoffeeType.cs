using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetBTNCoffeeType : MonoBehaviour
{
    public CoffeeTypeSO coffeeType = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void selectCoffeeType()
    {
        GameObject coffeeDesignPopup = GameObject.Find("CoffeeDesignPopup(Clone)");
        //Set Coffee type in popup
        coffeeDesignPopup.GetComponent<CoffeeDesignPopup>().selectedCoffeeType = coffeeType;

        //Set Outline of selected button
        SetBTNCoffeeType[] foundBTNs = FindObjectsOfType<SetBTNCoffeeType>();
        foreach (SetBTNCoffeeType button in foundBTNs)
        {
            button.gameObject.GetComponent<Outline>().enabled = false;
        }
        gameObject.GetComponent<Outline>().enabled = true;
    }
}
