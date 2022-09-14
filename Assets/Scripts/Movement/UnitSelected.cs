using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelected : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //pathHandler Class
    private PathHandler pathHandler;

    //TileMap Class
    private TileMap tileMap;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("unit selected (new gamestate manager)");

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        CanvasHandler canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //TileMap Class
        GameObject map = GameObject.Find("Map");
        tileMap = map.GetComponent<TileMap>();

        //PathHandler Class
        pathHandler = map.GetComponent<PathHandler>();

        //CoordsHandler Class
        coordsHandler = map.GetComponent<CoordsHandler>();

        canvasHandler.DisableCanvas(canvasHandler.actionCanvas);
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (unitHandler.unmovedUnitsList.Contains(unitHandler.selectedUnit))
        {
            coordsHandler.SetUnitMvmtGrid(unitHandler.selectedUnitGO, unitHandler.selectedUnit);

            for (int x = coordsHandler.unitMovementRangeStartX; x < coordsHandler.unitMovementRangeEndX + 1; x++)
            {
                for (int z = coordsHandler.unitMovementRangeStartZ; z < coordsHandler.unitMovementRangeEndZ + 1; z++)
                {
                    float pathCost = tileMap.GeneratePathTo(x, z, unitHandler.selectedUnitGO);

                    if (pathCost - 1 <= unitHandler.selectedUnit.movementRange)
                    {
                        pathHandler.AddAccessibleTile(x, z);
                        pathHandler.DisplayAndAddSelectedTile(x, z, tileMap.blueTile);
                    }
                }
            }
        }

        gameState.SwitchState(gameState.moveUnit);
    }
}
