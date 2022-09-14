using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSkill : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //TileMap Class
    private TileMap tileMap;

    //PathHandler Class
    private PathHandler pathHandler;

    //HealthBarHandler Class
    private HealthBarHandler healthBarHandler;

    //CameraHandler Class
    private CameraHandler cameraHandler;

    //CanvasHandler Class
    private CanvasHandler canvasHandler;

    //MovementHandler Class
    private MovementHandler movementHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is view skill (new gamestate mamager)");

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //TileMap Class
        GameObject map = GameObject.Find("Map");
        tileMap = map.GetComponent<TileMap>();

        //CoordsHandler Class
        coordsHandler = map.GetComponent<CoordsHandler>();

        //PathHandler Class
        pathHandler = map.GetComponent<PathHandler>();

        //HealthBarHandler Class
        GameObject healthBars = GameObject.Find("Health Bars");
        healthBarHandler = healthBars.GetComponent<HealthBarHandler>();

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        CanvasHandler canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //MovementHandler Class
        movementHandler = map.GetComponent<MovementHandler>();

        //sets the units original coords at the start of the state.
        coordsHandler.SetUnitOgX(unitHandler.selectedUnitGO);
        coordsHandler.SetUnitOgY(unitHandler.selectedUnitGO);
        coordsHandler.SetUnitOgZ(unitHandler.selectedUnitGO);

        cameraHandler.EnableCamera(cameraHandler.mainCamera);
        cameraHandler.DisableCamera(cameraHandler.attackCamera);

        canvasHandler.DisableCanvas(canvasHandler.actionCanvas);
        canvasHandler.EnableAllUnitUICanvases();
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO) + 1))
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(0, 0, 1), unitHandler.selectedUnitGO));

            coordsHandler.IncreaseTileZ(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO) - 1))
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(0, 0, -1), unitHandler.selectedUnitGO));

            coordsHandler.DecreaseTileZ(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO) + 1, coordsHandler.GetTileZ(unitHandler.selectedUnitGO)))
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(1, 0, 0), unitHandler.selectedUnitGO));

            coordsHandler.IncreaseTileX(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !movementHandler.IsMoving
            && pathHandler.GetAccTile(coordsHandler.GetTileX(unitHandler.selectedUnitGO) - 1, coordsHandler.GetTileZ(unitHandler.selectedUnitGO)))
        {
            gameState.StartCoroutine(movementHandler.MoveSelectedUnit(new Vector3(-1, 0, 0), unitHandler.selectedUnitGO));

            coordsHandler.DecreaseTileX(unitHandler.selectedUnitGO);
            coordsHandler.SetSelectedUnitMoved(true);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //changes the tile that was previously occupied to its original tile
            if (coordsHandler.selectedUnitMoved == true)
            {
                coordsHandler.ResetOrigTileTerrain(coordsHandler.selectedUnitOgX, coordsHandler.selectedUnitOgZ);
            }

            //writes that the tile at the unit's x and z after moving is occuppied (tiles[x, z] = 3)
            TileMap.tiles[coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO)] = 3;

            //destroys the move unit visuals.
            pathHandler.DestroySTs();

            unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
            unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

            gameState.SwitchState(gameState.selectTile);
        }
    }
}
