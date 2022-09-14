using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnit : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //PathHandler Class
    private PathHandler pathHandler;

    //TileMap Class
    private TileMap tileMap;

    //MovementHandler Class
    private MovementHandler movementHandler;

    //UnitUIHandler Class
    private UnitUIHandler unitUIHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("move unit (new gamestate manager)");

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //TileMap Class
        GameObject map = GameObject.Find("Map");
        tileMap = map.GetComponent<TileMap>();

        //PathHandler Class
        pathHandler = map.GetComponent<PathHandler>();

        //MovementHandler Class
        movementHandler = map.GetComponent<MovementHandler>();

        //CoordsHandler Class
        coordsHandler = map.GetComponent<CoordsHandler>();

        //UnitUIHandler Class
        unitUIHandler = units.GetComponent<UnitUIHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        CanvasHandler canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //sets the units original coords at the start of the moveUnit state.
        coordsHandler.SetUnitOgX(unitHandler.selectedUnitGO);
        coordsHandler.SetUnitOgY(unitHandler.selectedUnitGO);
        coordsHandler.SetUnitOgZ(unitHandler.selectedUnitGO);

        //sets unit to be un moved at start of move state.
        coordsHandler.SetSelectedUnitMoved(false);

        canvasHandler.DisableCanvas(canvasHandler.actionCanvas);

        if (unitHandler.movedUnitsList.Contains(unitHandler.selectedUnit))
        {
            gameState.SwitchState(gameState.selectAction);
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO) + 1) == true)
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(0, 0, 1), unitHandler.selectedUnitGO));

            coordsHandler.IncreaseTileZ(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO) - 1) == true)
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(0, 0, -1), unitHandler.selectedUnitGO));

            coordsHandler.DecreaseTileZ(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile( coordsHandler.GetTileX(unitHandler.selectedUnitGO) + 1, coordsHandler.GetTileZ(unitHandler.selectedUnitGO) ) == true)
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(1, 0, 0), unitHandler.selectedUnitGO));

            coordsHandler.IncreaseTileX(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile( coordsHandler.GetTileX(unitHandler.selectedUnitGO) - 1, coordsHandler.GetTileZ(unitHandler.selectedUnitGO) ) == true)
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(-1, 0, 0), unitHandler.selectedUnitGO));

            coordsHandler.DecreaseTileX(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        //cancels unit movement and reverts to selectTile state.
        if (Input.GetKeyDown(KeyCode.S))
        {
            //destroys the blue selected tiles in the game world.
            pathHandler.DestroySTs();

            //transforms the selected unit back to its original position
            coordsHandler.ReturnToOg(unitHandler.selectedUnitGO);

            //Changes UnitData's X and Z coords to starting position
            coordsHandler.SetUnitX(unitHandler.selectedUnitGO, coordsHandler.selectedUnitOgX);
            coordsHandler.SetUnitZ(unitHandler.selectedUnitGO, coordsHandler.selectedUnitOgZ);

            //clears out the old list of accessible tiles.
            pathHandler.accessibleTiles.Clear();

            //PUTS BACK UNMOVED UNIT UI
            if (unitHandler.unmovedUnitsList.Contains(unitHandler.selectedUnit))
            {
                unitUIHandler.SetUnitMovableUI(tileMap.yellowTile, unitHandler.selectedUnitGO);
            }

            gameState.SwitchState(gameState.selectTile);
        }

        //continues to select action state.
        if (Input.GetKeyDown(KeyCode.A))
        {
            //changes the tile that was previously occupied to its original tile
            if (coordsHandler.selectedUnitMoved == true)
            {
                coordsHandler.ResetOrigTileTerrain(coordsHandler.selectedUnitOgX, coordsHandler.selectedUnitOgZ);
            }

            //writes that the tile at the unit's x and z after moving is occuppied (tiles[x, z] = 3)
            TileMap.tiles[coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO)] = 3;

            //destroys unmoved unit ui
            GameObject.Destroy(unitUIHandler.personalUnitUIDict[unitHandler.selectedUnitGO]);

            gameState.SwitchState(gameState.selectAction);
        }
    }
}
