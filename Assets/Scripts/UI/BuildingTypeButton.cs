using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingTypeButton : MonoBehaviour
{
    [SerializeField] private PlacedObjectTypeSO placedObjectTypeSO;


    public void setTypeSO(PlacedObjectTypeSO placedObjectTypeSOToAdd)
    {
        placedObjectTypeSO = placedObjectTypeSOToAdd;
        Debug.Log("Done");
        gameObject.GetComponentInChildren<TMP_Text>().SetText(placedObjectTypeSO.nameString);
        //Set Image
    }

    public void setBuildTypeFromButton()
    {
        GameObject.Find("BuildingSystem").GetComponent<BuildingSystem>().SetBuildingType(placedObjectTypeSO);
        GameObject.Find("UIManager").GetComponent<UIManager>().PopupToScreenAnimation();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
