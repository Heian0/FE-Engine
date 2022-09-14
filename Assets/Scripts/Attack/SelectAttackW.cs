using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SelectAttackW : GameBaseState
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

    //EnemyAttackHandler Class
    private EnemyAttackHandler enemyAttackHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is select attack weapon (new gamestate mamager)");

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

        //EnemyAttackHandler Class
        enemyAttackHandler = map.GetComponent<EnemyAttackHandler>();

        //HealthBarHandler Class
        GameObject healthBars = GameObject.Find("Health Bars");
        healthBarHandler = healthBars.GetComponent<HealthBarHandler>();

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //AttackButtonHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        attackButtonHandler = SelectAttackCanvases.GetComponent<AttackButtonHandler>();

        //AttackHandler Class
        attackHandler = SelectAttackCanvases.GetComponent<AttackHandler>();

        canvasHandler.DisableCanvas(canvasHandler.attackCanvas);
        canvasHandler.EnableCanvas(canvasHandler.attackCanvasW);

        if (enemyAttackHandler.enemyAttack != true)
        {
            attackButtonHandler.SetWeaponButtons(unitHandler.selectedUnitInventory);
        }

        else
        {
            attackButtonHandler.SetWeaponButtons(enemyAttackHandler.target.unitInventory);
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameState.SwitchState(gameState.viewAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //attack with equipped weapon
            if (enemyAttackHandler.enemyAttack != true)
            {
                attackHandler.AttackW(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnitInventory.equippedWeapon, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap));

                unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
                unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

                gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));
            }

            else
            {
                attackHandler.AttackW(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitInventory.equippedWeapon, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);

                if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                {
                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                }

                else
                {
                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //attack with secondary weapon

            if (enemyAttackHandler.enemyAttack != true)
            {
                attackHandler.AttackW(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnitInventory.secondaryWeapon, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap));

                unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
                unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

                gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));
            }

            else
            {
                attackHandler.AttackW(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitInventory.secondaryWeapon, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);
                
                if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                {
                    //enemyAttackHandler.currentEnemyIndex++;
                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                }

                else
                {
                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                }
            }
        }
    }
}
