using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    [SerializeField] Animator bottomPopupAnimator;
    [SerializeField] GameObject buildingTypeButtonPrefab;
    [SerializeField] GameObject buildTypeContent;

    BuildingSystem buildingSystem;
    BuildingGhost buildingGhost;

    [SerializeField] WorldTimeManager worldTimeManager;
    [SerializeField] Button pauseButton, speed1Button, speed2Button, speed3Button;
    [SerializeField] TMP_Text timeText, dateText;
    [SerializeField] Sprite timeButton, pressedTimeButton;

    private List<PlacedObjectTypeSO> placedObjectTypeSOList;

    private bool isBottomPopupOpen = false;



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

    public void SetDefaultUI()
    {
        buildingSystem.isBuildingSelected = false;
        buildingGhost.setVisual(false);
        if (isBottomPopupOpen)
        {
            PopupToScreenAnimation();
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
