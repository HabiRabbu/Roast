using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoffeeDesignPopup : MonoBehaviour
{

    [SerializeField] GameObject coffeeDesignButtonPrefab;
    [SerializeField] GameObject coffeeTypeButtonPrefab;

    private CoffeeDesignManager coffeeDesignManager;
    public GameObject viewDesigns;
    public GameObject createDesigns;
    public GameObject tab1;
    public GameObject tab2;
    public GameObject tab3;
    public GameObject tab4;
    public GameObject tab5;

    #region Product Creation
    public string productType;
    public string designName;
    public CoffeeTypeSO coffeeType;
    #endregion

    private enum TabState
    {
        Tab1,
        Tab2,
        Tab3,
        Tab4,
        Tab5
    }

    private TabState currentTab = TabState.Tab1;

    // Start is called before the first frame update
    void Start()
    {
        coffeeDesignManager = GameObject.Find("CoffeeDesignManager").GetComponent<CoffeeDesignManager>();
        //viewDesigns = GameObject.Find("ViewDesigns");
        //createDesigns = GameObject.Find("CreateDesigns");

        ViewDesigns();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (createDesigns.activeSelf)
        {
            ShowTab();
        }
        */
    }

    #region Product Creation
    public void SetProductType(int selection)
    {
        switch (selection)
        {
            case 1:
                productType = "Bean";
                break;
            case 2:
                productType = "Dried";
                break;
            case 3:
                productType = "Roasted";
                break;
            case 4:
                productType = "Ground";
                break;
            case 5:
                productType = "Other";
                break;
        }
    }

    public void CreateDesign()
    {
        //TODO: Find and set all local properties from selections, drop downs, inputs, etc  (designName, coffeeType, etc)


        if (productType == "Bean")
        {
            CoffeeDesign beanDesign = coffeeDesignManager.CreateBean(designName, coffeeType);

            print("Adding: " + beanDesign);
            coffeeDesignManager.AddCoffeeDesign(beanDesign);
        }
        //if (productType == "Dried")
        //{
        //    CoffeeDesign beanDesign = coffeeDesignManager.CreateBean(designName, coffeeType);
        //    CoffeeDesign driedDesign = coffeeDesignManager.CreateDried(beanDesign);

        //    coffeeDesignManager.AddCoffeeDesign(driedDesign);
        //}
        //TODO: Add this sort of thing ^ for every design type, needs work in CoffeeDesignManager as well.
  
    }
    #endregion

    #region visuals
    public void ShowTab()
    {
        switch (currentTab)
        {
            case TabState.Tab1:
                tab1.SetActive(true);
                tab2.SetActive(false);
                tab3.SetActive(false);
                tab4.SetActive(false);
                tab5.SetActive(false);

                GameObject beanContent = GameObject.Find("CoffeeBeanContent");
                foreach (CoffeeTypeSO coffeeType in coffeeDesignManager.coffeeTypes)
                {
                    GameObject coffeeTypeButton = Instantiate(coffeeTypeButtonPrefab, beanContent.transform);
                    //TODO: Set image
                    coffeeTypeButton.transform.Find("TXT_Name").GetComponent<TMP_Text>().SetText(coffeeType.nameString);
                }

                break;
            case TabState.Tab2:
                tab1.SetActive(false);
                tab2.SetActive(true);
                tab3.SetActive(false);
                tab4.SetActive(false);
                tab5.SetActive(false);
                break;
            case TabState.Tab3:
                tab1.SetActive(false);
                tab2.SetActive(false);
                tab3.SetActive(true);
                tab4.SetActive(false);
                tab5.SetActive(false);
                break;
            case TabState.Tab4:
                tab1.SetActive(false);
                tab2.SetActive(false);
                tab3.SetActive(false);
                tab4.SetActive(true);
                tab5.SetActive(false);
                break;
            case TabState.Tab5:
                tab1.SetActive(false);
                tab2.SetActive(false);
                tab3.SetActive(false);
                tab4.SetActive(false);
                tab5.SetActive(true);
                break;
        }
    }

    public void SetTab(int tabIndex)
    {
        switch (tabIndex)
        {
            case 1:
                currentTab = TabState.Tab1;
                break;
            case 2:
                currentTab = TabState.Tab2;
                break;
            case 3:
                currentTab = TabState.Tab3;
                break;
            case 4:
                currentTab = TabState.Tab4;
                break;
            case 5:
                currentTab = TabState.Tab5;
                break;
        }
        ShowTab();
    }
    public void CreateDesigns()
    {
        viewDesigns.SetActive(false);
        createDesigns.SetActive(true);

        ShowTab();
    }

    public void ViewDesigns()
    {
        viewDesigns.SetActive(true);
        createDesigns.SetActive(false);

        Transform designContent = viewDesigns.transform.Find("DesignContent");
        foreach (CoffeeDesign coffeeDesign in coffeeDesignManager.coffeeDesigns)
        {
            GameObject coffeeDesignButton = Instantiate(coffeeDesignButtonPrefab, designContent);
            //Show name of design
            coffeeDesignButton.transform.Find("TXT_Name").GetComponent<TMP_Text>().SetText(coffeeDesign.Name);
            //Show name of type of product (e.g. Ground Coffee Beans)
            coffeeDesignButton.transform.Find("TXT_CoffeeProduct").GetComponent<TMP_Text>().SetText(coffeeDesign.Product.Name);
        }
    }
    #endregion
}
