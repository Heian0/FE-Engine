using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //AttackHandler Class
    private AttackHandler attackHandler;

    //PathHandler Class
    private PathHandler pathHandler;

    //TileMap Class
    private TileMap tileMap;

    //CanvasHandler Class
    private CanvasHandler canvasHandler;

    //UnitUIHandler Class
    private UnitUIHandler unitUIHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("select action (new gamestate manager)");

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

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //AttackHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        attackHandler = SelectAttackCanvases.GetComponent<AttackHandler>();

        //UnitUIHandler Class
        unitUIHandler = units.GetComponent<UnitUIHandler>();

        pathHandler.ClearAccessibleTiles();
        pathHandler.ClearAttackableTiles();
        pathHandler.ClearAttackableTilesWithEnemy();
        canvasHandler.DisableCanvas(canvasHandler.abilityCanvas);
        canvasHandler.EnableCanvas(canvasHandler.actionCanvas);
        canvasHandler.DisableButton(canvasHandler.attackButton);
        canvasHandler.DisableButton(canvasHandler.itemsButton);
        canvasHandler.DisableButton(canvasHandler.waitButton);
        canvasHandler.DisableButton(canvasHandler.abilitiesButton);

        if (unitHandler.unmovedUnitsList.Contains(unitHandler.selectedUnit))
        {
            //checks if there is an attackable unit in range
            coordsHandler.SetUnitAtkGrid(unitHandler.selectedUnitGO, unitHandler.selectedUnitInventory, unitHandler.selectedUnit);

            for (int x = coordsHandler.unitAttackRangeStartX; x < coordsHandler.unitAttackRangeEndX + 1; x++)
            {
                for (int z = coordsHandler.unitAttackRangeStartZ; z < coordsHandler.unitAttackRangeEndZ + 1; z++)
                {
                    float pathCost = tileMap.GenerateAttackPathTo(x, z, unitHandler.selectedUnitGO);

                    int largestRange = coordsHandler.GetLargestRange(unitHandler.selectedUnit);

                    //add case where there is no equipped weapon
                    if (pathCost <= largestRange + 1)
                    {
                        Unit unitAt = unitHandler.GetUnitAt(x, z);
                        //Debug.Log(unitAt.name);
                        //adds each tile with a pathcost below or equal to the selected unit's attack range to a list called attackableTiles and is not the attacking unit's tile and is occupied.
                        if (pathCost != 0
                            && TileMap.tiles[x, z] == 3
                            && unitAt.side == "Enemy"
                            && attackHandler.IsUnitAttackable(unitHandler.selectedUnit, unitAt)
                            )
                        {
                            pathHandler.AddAttackableTile(x, z);

                            canvasHandler.EnableButton(canvasHandler.attackButton);

                            break;
                        }
                    }
                }
            }

            canvasHandler.EnableButton(canvasHandler.itemsButton);
            canvasHandler.EnableButton(canvasHandler.abilitiesButton);
            canvasHandler.EnableButton(canvasHandler.waitButton);
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        //Selects the attack action, moves to attack state.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //destroys the move unit visuals.
            pathHandler.DestroySTs();

            for (int x = coordsHandler.unitAttackRangeStartX; x < coordsHandler.unitAttackRangeEndX + 1; x++)
            {
                for (int z = coordsHandler.unitAttackRangeStartZ; z < coordsHandler.unitAttackRangeEndZ + 1; z++)
                {
                    float pathCost = tileMap.GenerateAttackPathTo(x, z, unitHandler.selectedUnitGO);

                    //add case where there is no equipped weapon
                    if (pathCost - 1 <= unitHandler.selectedUnitInventory.equippedWeapon.range)
                    {
                        //adds each tile with a pathcost below or equal to the selected unit's attack range to a list called attackableTiles.
                        pathHandler.AddAttackableTile(x, z);

                        //Writes the visual blue tiles into the world
                        //adds each GameObject tileGO that has a pathcost below or equal to the selected unit's attack range to a list called atList.
                        pathHandler.DisplayAndAddAttackableTile(x, z, tileMap.blueTile);
                    }
                }
            }

            //changing the tile that was previously occupied to its original tile is not neccesary here because in order to get out of attack state and back to unit selected,
            //you must go through "S" in selectAction, which changes the tile that unit occupied after moving but then cancelled back to its original tile.

            gameState.SwitchState(gameState.attack);
        }

        //selects the wait action and returns to select tile state.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //destroys the move unit visuals.
            pathHandler.DestroySTs();

            unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
            unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

            //removes the unmoved unit ui from below the unit

            gameState.SwitchState(gameState.selectTile);
        }

        //cancels action and returns to the select tile state.
        if (Input.GetKeyDown(KeyCode.S))
        {
            //destroys the move unit visuals.
            pathHandler.DestroySTs();

            //changes the tile that unit occupied after moving but then cancelled back to its original tile.
            //sets tile that unit occupied before moving to be 3
            if (coordsHandler.selectedUnitMoved == true)
            {
                coordsHandler.ResetOrigTileTerrain(coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO));
                coordsHandler.SetTileTerrainOccupied(coordsHandler.selectedUnitOgX, coordsHandler.selectedUnitOgZ);
            }

            //transforms the selected unit back to its original position
            coordsHandler.ReturnToOg(unitHandler.selectedUnitGO);

            //Changes UnitData's X and Z coords to starting position
            coordsHandler.SetUnitX(unitHandler.selectedUnitGO, coordsHandler.selectedUnitOgX);
            coordsHandler.SetUnitZ(unitHandler.selectedUnitGO, coordsHandler.selectedUnitOgZ);

            //PUTS BACK UNMOVED UNIT UI
            if (unitHandler.unmovedUnitsList.Contains(unitHandler.selectedUnit))
            {
                unitUIHandler.SetUnitMovableUI(tileMap.yellowTile, unitHandler.selectedUnitGO);
            }

            gameState.SwitchState(gameState.selectTile);
        }

        //Selects the attack action, moves to view ability state.
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //destroys the move unit visuals.
            pathHandler.DestroySTs();
            
            gameState.SwitchState(gameState.selectAbility);
        }
    }
}
