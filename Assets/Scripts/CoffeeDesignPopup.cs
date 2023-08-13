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

    public List<GameObject> tabButtons;

    List<GameObject> tabs = new List<GameObject>();
    public GameObject tab1;
    public GameObject tab2;
    public GameObject tab3;
    public GameObject tab4;
    public GameObject tab5;
    public bool isTab1Setup, isTab2Setup, isTab3Setup, isTab4Setup, isTab5Setup = false;

    #region Product Creation
    public string productType;
    public string designName;
    public CoffeeTypeSO selectedCoffeeType = null;
    Dictionary<string, GameObject> productTypeBTNs = new Dictionary<string, GameObject>();
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
        //Add Selection Buttons to Dictionary
        productTypeBTNs.Add("Bean", GameObject.Find("BTN_Bean"));
        productTypeBTNs.Add("Dried", GameObject.Find("BTN_Dried"));
        productTypeBTNs.Add("Roasted", GameObject.Find("BTN_Roasted"));
        productTypeBTNs.Add("Ground", GameObject.Find("BTN_Ground"));
        productTypeBTNs.Add("Other", GameObject.Find("BTN_Other"));

        //Add tabs to list
        tabs.Add(tab1);
        tabs.Add(tab2);
        tabs.Add(tab3);
        tabs.Add(tab4);
        tabs.Add(tab5);

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

        foreach (var item in productTypeBTNs)
        {
            if (productType == item.Key)
            {
                item.Value.GetComponent<Image>().color = Color.black;
            }
            else
            {
                item.Value.GetComponent<Image>().color = Color.white;
            }
        }
    }

    //Accessed by final button - Takes all tabs info and creates a design.
    public void CreateDesign()
    {
        //TODO: Find and set all local properties from selections, drop downs, inputs, etc  (designName, selectedCoffeeType, etc)
        designName = GameObject.Find("NameInput").GetComponent<TMP_InputField>().text;

        if (productType == "Bean")
        {
            if (designName.Length > 0 && selectedCoffeeType != null)
            {
                coffeeDesignManager.CreateBean(designName, selectedCoffeeType);
            }
            else
            {
                print("Bean design missing name or selected coffee type.");
            }
        }
        //if (productType == "Dried")
        //{
        //    CoffeeDesign beanDesign = coffeeDesignManager.CreateBean(designName, selectedCoffeeType);
        //    CoffeeDesign driedDesign = coffeeDesignManager.CreateDried(beanDesign);

        //    coffeeDesignManager.AddCoffeeDesign(driedDesign);
        //}
        //TODO: Add this sort of thing ^ for every design type, needs work in CoffeeDesignManager as well.
  
    }
    #endregion

    #region visuals
    public void ShowTab()
    {
        foreach(GameObject tab in tabs)
        {
            tab.SetActive(false);
        }
        foreach(GameObject tabButton in tabButtons)
        {
            tabButton.GetComponent<Outline>().enabled = false;
        }
        switch (currentTab)
        {
            case TabState.Tab1:
                tab1.SetActive(true);
                tabButtons[0].GetComponent<Outline>().enabled = true;
                if (!isTab1Setup)
                {
                    GameObject beanContent = GameObject.Find("CoffeeBeanContent");
                    foreach (CoffeeTypeSO coffeeTypes in coffeeDesignManager.coffeeTypes)
                    {
                        GameObject coffeeTypeButton = Instantiate(coffeeTypeButtonPrefab, beanContent.transform);
                        coffeeTypeButton.GetComponent<SetBTNCoffeeType>().coffeeType = coffeeTypes;
                        //TODO: Set image
                        coffeeTypeButton.transform.Find("TXT_Name").GetComponent<TMP_Text>().SetText(coffeeTypes.nameString);
                    }
                    isTab1Setup = true;
                }
                break;
            case TabState.Tab2:
                tab2.SetActive(true);
                tabButtons[1].GetComponent<Outline>().enabled = true;
                break;
            case TabState.Tab3:
                tab3.SetActive(true);
                tabButtons[2].GetComponent<Outline>().enabled = true;
                break;
            case TabState.Tab4:
                tab4.SetActive(true);
                tabButtons[3].GetComponent<Outline>().enabled = true;
                break;
            case TabState.Tab5:
                tab5.SetActive(true);
                tabButtons[4].GetComponent<Outline>().enabled = true;
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
        foreach (Transform child in designContent)
        {
            Destroy(child.gameObject);
        }
        foreach (CoffeeDesign coffeeDesign in coffeeDesignManager.coffeeDesigns)
        {
            GameObject coffeeDesignButton = Instantiate(coffeeDesignButtonPrefab, designContent);
            //Show name of design
            coffeeDesignButton.transform.Find("TXT_Name").GetComponent<TMP_Text>().SetText(coffeeDesign.nameString);
            //Show name of type of product (e.g. Ground Coffee Beans)
            coffeeDesignButton.transform.Find("TXT_CoffeeProduct").GetComponent<TMP_Text>().SetText(coffeeDesign.Product.nameString);
        }
    }
    #endregion
}
