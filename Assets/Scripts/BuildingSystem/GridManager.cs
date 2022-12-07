using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int xLength = 100;
    [SerializeField] private int zLength = 100;
    [SerializeField] private int cellSize = 1;
    [SerializeField] private Tile tilePrefab;

    public NavMeshSurface navMeshSurface;

    //Changing cellSize (gridSpacing) is scary - think of ObjectTypeSO's Grid Positioning etc
    //(Look out for *10 or +10 in foreach) (Plus all "adjusted" values

    private Dictionary<Vector3, Tile> tiles;

    private void Awake()
    {
        GenerateGrid();
        GenerateNavMesh();
    }

    public void GenerateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector3, Tile>();
        tilePrefab.transform.localScale = new Vector3(cellSize, tilePrefab.transform.localScale.y, cellSize);
        int gridSpacing = cellSize * 10;
        int adjustedxLength = xLength *= gridSpacing;
        int adjustedzLength = zLength *= gridSpacing;

        for (int x = 0; x < adjustedxLength; x+= gridSpacing)
        {
            for (int z = 0; z < adjustedzLength; z += gridSpacing)
            {
                int labelx = x / gridSpacing;
                int labelz = z / gridSpacing;
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity, GameObject.Find("NavMeshSurface").transform);
                spawnedTile.name = $"Tile {labelx},{labelz}";
                spawnedTile.setCoords(new Vector2(labelx, labelz), new Vector2(x,z));

                tiles[new Vector3(x, 0, z)] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }

    public Vector2 GetTileXZ(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            Vector2 tileXZ = new Vector2(tile.xCoord, tile.zCoord);
            return tileXZ;
        }
        else
        {
            return Vector2.zero;
        }
    }
}
