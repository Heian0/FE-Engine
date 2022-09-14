using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class EnemySelectAttack : GameBaseState
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
        Debug.Log("game state is select attack enemy (new gamestate mamager)");

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

        switch (enemyAttackHandler.recAtkType)
        {
            case 0:

                canvasHandler.EnableCanvas(canvasHandler.attackCanvasW);
                attackButtonHandler.SetWeaponButtons(enemyAttackHandler.target.unitInventory);

                break;

            case 1:

                canvasHandler.EnableCanvas(canvasHandler.attackCanvasS);
                attackButtonHandler.SetSkillButtons(enemyAttackHandler.target.unitSkillset);

                break;

            case 2:

                canvasHandler.EnableCanvas(canvasHandler.attackCanvasA);
                attackButtonHandler.SetAbilityButtons(enemyAttackHandler.target.unitAbilityset);

                break;
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameState.SwitchState(gameState.enemyAttack);
        }

        switch (enemyAttackHandler.recAtkType)
        {
            case 0: //weapon

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    //attack with equipped weapon
                    attackHandler.AttackW(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitInventory.equippedWeapon, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);

                    if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                    {
                        enemyAttackHandler.currentEnemyIndex++;
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                    }

                    else
                    {
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                    }

                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    //attack with secondary weapon

                    attackHandler.AttackW(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitInventory.secondaryWeapon, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);

                    if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                    {
                        enemyAttackHandler.currentEnemyIndex++;
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                    }

                    else
                    {
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                    }
                }

                break;

            case 1: //skill

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    //attack with skill one

                    attackHandler.AttackS(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitSkillset.skillOne, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);

                    if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                    {
                        enemyAttackHandler.currentEnemyIndex++;
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                    }

                    else
                    {
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                    }
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    //attack with skill two

                    attackHandler.AttackS(enemyAttackHandler.target, enemyAttackHandler.enemyUnit, enemyAttackHandler.target.unitSkillset.skillTwo, enemyAttackHandler.enemyUnit.unitInventory.equippedWeapon, healthBarHandler, true);

                    if (enemyAttackHandler.listOfUnmovedEnemies.Count != 0)
                    {
                        enemyAttackHandler.currentEnemyIndex++;
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyMovement, 2.0f));
                    }

                    else
                    {
                        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.turnStart, 2.0f));
                    }
                }

                break;
        }
    }
}
