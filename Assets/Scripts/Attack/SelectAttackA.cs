using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SelectAttackA : GameBaseState
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

    //AttackButtonHandler Class
    private AttackButtonHandler attackButtonHandler;

    //AttackHandlerClass
    private AttackHandler attackHandler;

    //AbilityHandler Class
    public AbilityHandler abilityHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is select attack abilities (new gamestate mamager)");

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

        //AttackButtonHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        attackButtonHandler = SelectAttackCanvases.GetComponent<AttackButtonHandler>();

        //AttackHandler Class
        attackHandler = SelectAttackCanvases.GetComponent<AttackHandler>();

        //AbilityHandler Class
        abilityHandler = SelectAttackCanvases.GetComponent<AbilityHandler>();

        canvasHandler.DisableCanvas(canvasHandler.attackCanvas);
        canvasHandler.EnableCanvas(canvasHandler.attackCanvasA);

        attackButtonHandler.SetAbilityButtons(unitHandler.selectedUnitAbilityset);
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameState.SwitchState(gameState.viewAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //attack with 1st ability
            unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
            unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

            attackHandler.AttackA(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnit.unitAbilityset.abilityOne, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap));

            gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));
        }
    }
}
