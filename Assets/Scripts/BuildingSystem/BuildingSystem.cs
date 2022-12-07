using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
{

    GridManager gridManager;
    BuildingGhost buildingGhost;

    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private PlacedObjectTypeSO placedObjectTypeSO;

    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    public List<PlacedObjectTypeSO> GetPlacedObjectTypeSOList()
    {
        return placedObjectTypeSOList;
    }

    public bool isBuildingSelected = false;

    private void Awake()
    {
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        buildingGhost = GameObject.Find("BuildingGhost").GetComponent<BuildingGhost>();
        placedObjectTypeSO = placedObjectTypeSOList[0];
    }

    void Update()
    {
        if (isBuildingSelected)
        {
            PlaceBuilding();
        }

    }

    public void SetBuildingType(PlacedObjectTypeSO selectedPlacedObjectTypeSO)
    {
        placedObjectTypeSO = selectedPlacedObjectTypeSO;
        isBuildingSelected = true;
        buildingGhost.setVisual(true);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void PlaceBuilding()
    {

        //Placing currently selected building
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            Vector3 tileXZ = GetMouseWorldPosition();
            Vector3 adjustedTileXZ = new Vector3((Mathf.Round(tileXZ.x / 10)) * 10, 0, (Mathf.Round(tileXZ.z / 10)) * 10);

            List<Vector3> gridPositionList = placedObjectTypeSO.GetGridPositionList(adjustedTileXZ, dir);
            Tile selectedTile = gridManager.GetTileAtPosition(adjustedTileXZ);

            bool canBuild = true;
            foreach (Vector3 gridPosition in gridPositionList)
            {
                if (!gridManager.GetTileAtPosition(new Vector3(gridPosition.x, 0, gridPosition.z)).CanBuild())
                {
                    canBuild = false;
                    break;
                }
            }


            if (canBuild)
            {
                Vector3 rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 tileCoords = new Vector3(selectedTile.xCoord, 0, selectedTile.zCoord);
                Vector3 placedObjectWorldPosition = tileCoords + rotationOffset;


                PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, tileCoords, dir, placedObjectTypeSO);

                foreach (Vector3 gridPosition in gridPositionList)
                {
                    selectedTile = gridManager.GetTileAtPosition(new Vector3(gridPosition.x, 0, gridPosition.z));
                    selectedTile.SetPlacedObject(placedObject);
                    Debug.Log(selectedTile);

                }
            }
            else
            {
                Debug.Log("Spot taken.");
            }
        }

        //Delete building at mouse location
        if (Input.GetMouseButtonDown(1) && !IsMouseOverUI())
        {
            Vector3 tileXZ = GetMouseWorldPosition();
            Vector3 adjustedTileXZ = new Vector3((Mathf.Round(tileXZ.x / 10)) * 10, 0, (Mathf.Round(tileXZ.z / 10)) * 10);
            Tile selectedTile = gridManager.GetTileAtPosition(adjustedTileXZ);
            PlacedObject placedObject = selectedTile.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector3> gridPositionList = placedObject.GetGridPositionList();
                foreach (Vector3 gridPosition in gridPositionList)
                {
                    selectedTile = gridManager.GetTileAtPosition(new Vector3(gridPosition.x, 0, gridPosition.z));
                    selectedTile.ClearPlacedObject();
                }
            }
        }

        //Rotate currently selected building
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            GameObject.Find("BuildingGhost").GetComponent<BuildingGhost>().SetDir(dir);
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
                return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return placedObjectTypeSO;
    }
}
