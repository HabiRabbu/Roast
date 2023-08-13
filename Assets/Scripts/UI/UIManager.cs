using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using TL.Core;
using TL.UtilityAI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameplayCanvas;

    #region Building Cache
    private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    [SerializeField] Animator bottomPopupAnimator;
    [SerializeField] GameObject buildingTypeButtonPrefab;
    [SerializeField] GameObject buildTypeContent;
    BuildingSystem buildingSystem;
    BuildingGhost buildingGhost;
    #endregion

    #region Time Cache
    [SerializeField] WorldTimeManager worldTimeManager;
    [SerializeField] Button pauseButton, speed1Button, speed2Button, speed3Button;
    [SerializeField] TMP_Text timeText, dateText;
    [SerializeField] Sprite timeButton, pressedTimeButton;
    #endregion

    #region Staff Info Cache
    [SerializeField] GameObject staffInfoPopupPrefab;
    GameObject staffInfoPopup = null;
    GameObject selectedStaff = null;
    #endregion

    #region Coffee Cache
    [SerializeField] GameObject coffeeDesignPopupPrefab;
    GameObject coffeeDesignPopup = null;
    WorkObject selectedWorkObject = null;
    #endregion

    private bool isBottomPopupOpen = false;
    private bool isStaffInfoPopupOpen = false;
    private bool isCoffeeDesignPopupOpen = false;



    private void Awake()
    {
        buildingSystem = GameObject.Find("BuildingSystem").GetComponent<BuildingSystem>();
        buildingGhost = GameObject.Find("BuildingGhost").GetComponent<BuildingGhost>();
    }
    // Start is called before the first frame update
    void Start()
    {
        placedObjectTypeSOList = buildingSystem.GetPlacedObjectTypeSOList();
        SetupBuildMenuItems();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForHotkeys();
        UpdateStaffInfoPopup();
        UpdateCoffeeDesignPopup();
    }

    public void BTNToggleCoffeeDesignPopup()
    {
        ToggleCoffeeDesignPopup(null);
    }
    public void ToggleCoffeeDesignPopup(WorkObject workObject)
    {
        //Open the popup
        if (!isCoffeeDesignPopupOpen)
        {
            coffeeDesignPopup = Instantiate(coffeeDesignPopupPrefab, gameplayCanvas.transform);
            //coffeeDesignPopup.GetComponent<Animator>().Play("CoffeeDesignPopupOpen");
            isCoffeeDesignPopupOpen = true;

            if (workObject != null)
            {
                selectedWorkObject = workObject;
            }
            else
            {
                selectedWorkObject = null;
            }
        }
        else
        //Close the popup
        {
            if (coffeeDesignPopup != null)
            {
                Destroy(coffeeDesignPopup);
                coffeeDesignPopup = null;
            }
            isCoffeeDesignPopupOpen = false;
        }
    }

    public void UpdateCoffeeDesignPopup()
    {
        if (isCoffeeDesignPopupOpen)
        {

        }
    }

    public void ToggleStaffInfoPopup(GameObject gameObject)
    {
        //Open the popup
        if (!isStaffInfoPopupOpen)
        {
            selectedStaff = gameObject;
            staffInfoPopup = Instantiate(staffInfoPopupPrefab, gameplayCanvas.transform);
            staffInfoPopup.GetComponent<Animator>().Play("StaffInfoPopupOpen");
            isStaffInfoPopupOpen = true;
        }
        else
        //Close the popup
        {
            if (staffInfoPopup != null)
            {
                Destroy(staffInfoPopup);
                staffInfoPopup = null;
            }
            isStaffInfoPopupOpen = false;
        }

    }

    private void CheckForHotkeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetDefaultUI();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Speed1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Speed2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Speed3();
        }
    }

    private void SetupBuildMenuItems()
    {
        foreach (PlacedObjectTypeSO placedObjectTypeSO in placedObjectTypeSOList)
        {
            GameObject buildTypeButton = Instantiate(buildingTypeButtonPrefab, buildTypeContent.transform);
            buildTypeButton.GetComponent<BuildingTypeButton>().setTypeSO(placedObjectTypeSO);
        }
    }

    public void UpdateStaffInfoPopup()
    {
        if (isStaffInfoPopupOpen)
        {
            StaffStats staffStats = selectedStaff.GetComponent<StaffStats>();
            staffInfoPopup.transform.GetChild(0).GetComponent<TMP_Text>().text = staffStats.staffName;//nameString
                                                                                                      //Image
            staffInfoPopup.transform.GetChild(2).GetComponent<TMP_Text>().text = "Age: " + staffStats.age.ToString();//Age
            staffInfoPopup.transform.GetChild(3).GetComponent<TMP_Text>().text = "Level: " + staffStats.level.ToString();//Level
                                                                                                                         //Needs
                                                                                                                         //CurrentActionTitle
            staffInfoPopup.transform.GetChild(6).GetComponent<TMP_Text>().text = "Hunger: " + staffStats.hunger.ToString();//Hunger
            staffInfoPopup.transform.GetChild(7).GetComponent<TMP_Text>().text = "Energy: " + staffStats.energy.ToString();//Energy
            staffInfoPopup.transform.GetChild(8).GetComponent<TMP_Text>().text = "Money: " + staffStats.money.ToString();//Money
            staffInfoPopup.transform.GetChild(9).GetComponent<TMP_Text>().text = selectedStaff.GetComponent<AIBrain>().bestAction.Name;//CurrentAction
        }
    }

    public void SetDefaultUI()
    {
        buildingSystem.isBuildingSelected = false;
        buildingGhost.setVisual(false);
        if (isBottomPopupOpen)
        {
            PopupToScreenAnimation();
        }
        if (isStaffInfoPopupOpen)
        {
            ToggleStaffInfoPopup(null);
        }
        if (isCoffeeDesignPopupOpen)
        {
            ToggleCoffeeDesignPopup(null);
        }
    }

    public void PopupToScreenAnimation()
    {
        if (isBottomPopupOpen)
        {
            bottomPopupAnimator.Play("BottomPopupClose");
            isBottomPopupOpen = false;
            buildTypeContent.SetActive(false);
        }
        else
        {
            buildTypeContent.SetActive(true);
            bottomPopupAnimator.Play("BottomPopupOpen");
            isBottomPopupOpen = true;
        }
    }

    #region TimeControls

    public void ShowTime(int hour, int day, int month, int year)
    {
        string time = hour + ":00";
        timeText.SetText(time);

        string date = "D: " + day + "  M: " + month + "  Y: " + year;
        dateText.SetText(date);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        resetTimeButtons();
        pauseButton.image.sprite = pressedTimeButton;
    }
    public void Speed1()
    {
        Time.timeScale = 1f;
        resetTimeButtons();
        speed1Button.image.sprite = pressedTimeButton;
    }

    public void Speed2()
    {
        Time.timeScale = 2f;
        resetTimeButtons();
        speed2Button.image.sprite = pressedTimeButton;
    }

    public void Speed3()
    {
        Time.timeScale = 3f;
        resetTimeButtons();
        speed3Button.image.sprite = pressedTimeButton;
    }

    public void resetTimeButtons()
    {
        pauseButton.image.sprite = timeButton;
        speed1Button.image.sprite = timeButton;
        speed2Button.image.sprite = timeButton;
        speed3Button.image.sprite = timeButton;
    }

    #endregion TimeControls
}
