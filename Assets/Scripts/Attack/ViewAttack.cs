using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewAttack : GameBaseState
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

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is view attack (new gamestate mamager)");

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

        //removes the blue attackable tiles from world
        pathHandler.DestroyATs();

        cameraHandler.DisableCamera(cameraHandler.mainCamera);
        cameraHandler.EnableCamera(cameraHandler.attackCamera);

        //sets up health bars
        healthBarHandler.SetFriendlyMaxHealth(unitHandler.selectedUnit);
        healthBarHandler.SetEnemyMaxHealth(unitHandler.selectedEnemyUnit);

        //sets up camera angles
        cameraHandler.ResetAttackCamera();
        cameraHandler.SetAttackCamera(unitHandler.selectedUnitGO, unitHandler.selectedEnemyUnitGO, coordsHandler);

        canvasHandler.DisableCanvas(canvasHandler.attackCanvasW);
        canvasHandler.DisableCanvas(canvasHandler.attackCanvasS);
        canvasHandler.DisableCanvas(canvasHandler.attackCanvasA);
        canvasHandler.EnableCanvas(canvasHandler.attackCanvas);
        canvasHandler.DisableAllUnitUICanvases();
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach (AccTile AT in pathHandler.attackableTiles)
            {
                pathHandler.DisplayAttackableTile(AT.AccTileX, AT.AccTileZ, tileMap.blueTile);
            }

            gameState.SwitchState(gameState.attack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameState.SwitchState(gameState.selectAttackW);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameState.SwitchState(gameState.selectAttackS);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameState.SwitchState(gameState.selectAttackA);
        }
    }
}
