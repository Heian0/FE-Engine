using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TileType
{
    public string tileName;
    public GameObject tileVisualPrefab;

    public int aerialMvmtCost;
    public float movementCost = 1;
}
