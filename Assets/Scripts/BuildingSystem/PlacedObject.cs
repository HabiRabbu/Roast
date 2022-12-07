using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{

    private PlacedObjectTypeSO placedObjectTypeSO;
    private Vector3 origin;
    private PlacedObjectTypeSO.Dir dir;

    public static PlacedObject Create(Vector3 worldPosition, Vector3 origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    #region Positioning
    public List<Vector3> GetGridPositionList()
    {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

    public PlacedObjectTypeSO.Dir GetDir()
    {
        return dir;
    }
    #endregion Positioning
}
