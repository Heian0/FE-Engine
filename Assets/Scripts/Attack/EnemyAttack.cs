using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //UnitUIHandler Class
    private UnitUIHandler unitUIHandler;

    //EnemyAttackHandler Class
    private EnemyAttackHandler enemyAttackHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //HealthBarHandler Class
    private HealthBarHandler healthBarHandler;

    //CameraHandler Class
    private CameraHandler cameraHandler;

    //CanvasHandler Class
    private CanvasHandler canvasHandler;

    public override void EnterState(GameStateManager gameState)
    {
        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //UnitUIHandler Class
        unitUIHandler = units.GetComponent<UnitUIHandler>();

        //EnemyAttackHandler Class
        GameObject map = GameObject.Find("Map");
        enemyAttackHandler = map.GetComponent<EnemyAttackHandler>();

        //CoordsHandler Class
        coordsHandler = map.GetComponent<CoordsHandler>();

        //HealthBarHandler Class
        GameObject healthBars = GameObject.Find("Health Bars");
        healthBarHandler = healthBars.GetComponent<HealthBarHandler>();

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        Debug.Log("gamestate is enemy attack");

        GameObject enemyUnitGO = enemyAttackHandler.orderedEnemyGOList[enemyAttackHandler.currentEnemyIndex];
        Unit enemyUnit = enemyUnitGO.GetComponent<UnitData>().unit;
        Unit target = enemyUnit.target;
        GameObject targetGO = unitHandler.GetUnitGO(target);

        enemyAttackHandler.enemyUnit = enemyUnit;
        enemyAttackHandler.enemyUnitGO = enemyUnitGO;

        enemyAttackHandler.target = target;
        enemyAttackHandler.targetGO = targetGO;

        cameraHandler.DisableCamera(cameraHandler.mainCamera);
        cameraHandler.EnableCamera(cameraHandler.attackCamera);

        //sets up health bars
        healthBarHandler.SetFriendlyMaxHealth(target);
        healthBarHandler.SetEnemyMaxHealth(enemyUnit);

        //sets up camera angles
        cameraHandler.ResetAttackCamera();
        cameraHandler.SetAttackCamera(targetGO, enemyUnitGO, coordsHandler);

        canvasHandler.DisableCanvas(canvasHandler.attackCanvasW);
        canvasHandler.DisableCanvas(canvasHandler.attackCanvasS);
        canvasHandler.DisableCanvas(canvasHandler.attackCanvasA);
        canvasHandler.EnableCanvas(canvasHandler.attackCanvas);
        canvasHandler.DisableAllUnitUICanvases();

        unitUIHandler.ClearAllUnitMovableUI();
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            enemyAttackHandler.enemyAttack = true;
            enemyAttackHandler.recAtkType = 0;
            gameState.SwitchState(gameState.enemySelectAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyAttackHandler.enemyAttack = true;
            enemyAttackHandler.recAtkType = 1;
            gameState.SwitchState(gameState.enemySelectAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            enemyAttackHandler.enemyAttack = true;
            enemyAttackHandler.recAtkType = 2;
            gameState.SwitchState(gameState.enemySelectAttack);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (enemyAttackHandler.currentEnemyIndex < enemyAttackHandler.orderedEnemyGOList.Count - 1)
            {
                enemyAttackHandler.currentEnemyIndex++;
                gameState.SwitchState(gameState.enemyMovement);
            }
        }
    }
}
