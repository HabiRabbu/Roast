using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public int xCoord;
    [SerializeField] public int zCoord;
    [SerializeField] public int xLabel;
    [SerializeField] public int zLabel;
    private PlacedObject placedObject;
    TextMeshPro label;
    GridManager gridManager;

    void Awake()
    {
        this.label = transform.GetChild(0).GetComponent<TextMeshPro>();
        this.gridManager = GetComponent<GridManager>();
    }
    private void Start()
    {
        showCoords();
    }

    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
        showCoords();
    }

    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }
    public void ClearPlacedObject()
    {
        this.placedObject = null;
        showCoords();
    }

    public bool CanBuild()
    {
        return placedObject == null;
    }

    private void OnMouseEnter()
    {
        meshRenderer.enabled = false;
    }

    private void OnMouseExit()
    {
        meshRenderer.enabled = true;
    }

    private void OnMouseDown()
    {
        Tile tile = GameObject.Find("GridManager").GetComponent<GridManager>().GetTileAtPosition(new Vector3(xCoord, 0, zCoord));
        Debug.Log(tile.xLabel + ", " + tile.zLabel);
    }

    public void setCoords(Vector2 labelcoords, Vector2 coords)
    {
        xCoord = Mathf.RoundToInt(coords.x);
        zCoord = Mathf.RoundToInt(coords.y);
        xLabel = Mathf.RoundToInt(labelcoords.x);
        zLabel = Mathf.RoundToInt(labelcoords.y);
    }
    private void showCoords()
    {
        if (!CanBuild())
        {
            this.label.text = xLabel + ", " + zLabel + "\n" + placedObject;
        }
        else
        {
            this.label.text = xLabel + ", " + zLabel;
        }

    }
}
