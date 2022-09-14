using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDict : MonoBehaviour
{
    public static Dictionary<int, string> MapUnitDict = new Dictionary<int, string>();

    void Start()
    {
        for (int i = 0; i < 101;i++)
        {
            //writes all tiles as "None" (No unit)
            MapUnitDict.Add(i, "None");
        }

        //manually writes tile 51 as containing "Player" (unit)
        MapUnitDict.Remove(91);
        MapUnitDict.Add(91, "Player");
    }

    void Update()
    {
        
    }
}
