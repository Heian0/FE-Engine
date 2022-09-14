using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableTile : MonoBehaviour
{
    public int tileX;
    public int tileZ;
    public TileMap map;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnMouseUp()
    {
        Debug.Log("Click!");

        //map.GeneratePathTo(tileX, tileZ);
    }
}
