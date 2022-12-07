using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]


public class CoordLabel : MonoBehaviour
{
    [SerializeField] int gridSize = 5;
    TextMeshPro label;
    Vector2Int coords = new Vector2Int(0,0);

    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            ShowCoords();
        }
        
    }

    private void ShowCoords()
    {
        coords.x = Mathf.RoundToInt(transform.parent.position.x/gridSize);
        coords.y = Mathf.RoundToInt(transform.parent.position.z/gridSize);
        label.text = coords.x + ", " + coords.y;
    }
}
