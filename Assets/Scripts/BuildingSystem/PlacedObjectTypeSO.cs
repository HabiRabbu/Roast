using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]

public class PlacedObjectTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public Transform visual;
    public int xWidth;
    public int zWidth;


    #region Positioning
    public static Dir GetNextDir(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return Dir.Left;
            case Dir.Left: return Dir.Up;
            case Dir.Up: return Dir.Right;
            case Dir.Right: return Dir.Down;

        }
    }

    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
    }

    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return 0;
            case Dir.Left: return 90;
            case Dir.Up: return 180;
            case Dir.Right: return 270;
        }
    }

    public Vector3 GetRotationOffset(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down: return new Vector3(0, 0, 0);
            case Dir.Left: return new Vector3(0, 0, xWidth*10);
            case Dir.Up: return new Vector3(xWidth*10, 0, 0);
            case Dir.Right: return new Vector3(0, 0, 0);
        }
    }

    public List<Vector3> GetGridPositionList(Vector3 offset, Dir dir)
    {
        List<Vector3> gridPositionList = new List<Vector3>();
        //x is up, z is sideways... (bit fucked)

        switch (dir)
        {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < zWidth * 10; x += 10)
                {
                    for (int y = 0; y < xWidth * 10; y += 10)
                    {
                        gridPositionList.Add(offset + new Vector3(x, 0, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < xWidth * 10; x += 10)
                {
                    for (int y = 0; y < zWidth * 10; y += 10)
                    {
                        gridPositionList.Add(offset + new Vector3(x, 0, y));
                    }
                }
                break;
        }
        return gridPositionList;
    }

    #endregion Positioning
}
