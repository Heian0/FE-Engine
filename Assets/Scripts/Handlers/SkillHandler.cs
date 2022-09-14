using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHandler : MonoBehaviour
{
    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    //HealthBarHandler Class
    private HealthBarHandler healthBarHandler;

    //TileMap Class
    private TileMap tileMap;

    //PathHandler Class
    private PathHandler pathHandler;

    void Start()
    {
        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //TileMap Class
        tileMap = map.GetComponent<TileMap>();

        //PathHandler Class
        pathHandler = map.GetComponent<PathHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        GameObject healthBars = GameObject.Find("Canvases/HealthBarCanvas/Health Bars");
        healthBarHandler = healthBars.GetComponent<HealthBarHandler>();
    }

    public void SkyDive()
    {
        coordsHandler.SetUnitMvmtGrid(unitHandler.selectedUnitGO, unitHandler.selectedUnit);

        for (int x = coordsHandler.unitMovementRangeStartX; x < coordsHandler.unitMovementRangeEndX + 1; x++)
        {
            for (int z = coordsHandler.unitMovementRangeStartZ; z < coordsHandler.unitMovementRangeEndZ + 1; z++)
            {
                float pathCost = tileMap.GeneratePathTo(x, z, unitHandler.selectedUnitGO);

                if (pathCost - 1 <= 4)
                {
                    pathHandler.AddAccessibleTile(x, z);
                    pathHandler.DisplayAndAddSelectedTile(x, z, tileMap.blueTile);
                }
            }
        } 

    }
}
