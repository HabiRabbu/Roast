using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{

    private Transform visual;
    private PlacedObjectTypeSO placedObjectTypeSO;
    public float buildingGhostSpeed = 15f;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BuildingSystem buildingSystem;
    public bool showVisual = false;
    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    private void Start()
    {
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        buildingSystem = GameObject.Find("BuildingSystem").GetComponent<BuildingSystem>();
    }

    private void LateUpdate()
    {
        if (showVisual)
        {
            Vector3 targetPosition = BuildingSystem.GetMouseWorldPosition();
            Vector3 adjustedTargetPosition = new Vector3((Mathf.Round(targetPosition.x / 10)) * 10, 0, (Mathf.Round(targetPosition.z / 10)) * 10);

            //PlacedObject placedObject = gridManager.GetTileAtPosition(adjustedTargetPosition).GetPlacedObject();

            adjustedTargetPosition.y = 1f;
            Vector3 rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            adjustedTargetPosition += rotationOffset;

            transform.position = Vector3.Lerp(transform.position, adjustedTargetPosition, Time.deltaTime * buildingGhostSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0), Time.deltaTime * buildingGhostSpeed);
        }
    }

    public void SetDir(PlacedObjectTypeSO.Dir dirToSet)
    {
        dir = dirToSet;
    }
    public void setVisual(bool isVisible)
    {
        if (isVisible)
        {
            SetVisualObject();
            showVisual = true;
        }
        else
        {
            if (visual != null)
            {
                Destroy(visual.gameObject);
                visual = null;
            }
            showVisual = false;
        }
    }

    private void SetVisualObject()
    {
        placedObjectTypeSO = buildingSystem.GetPlacedObjectTypeSO();

        if (visual != null)
        {
            Destroy(visual.gameObject);
        }
        if (placedObjectTypeSO != null)
        {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
        }
    }
}
