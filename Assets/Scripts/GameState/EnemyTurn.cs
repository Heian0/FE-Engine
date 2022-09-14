using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //EnemyAttackHandler Class
    private EnemyAttackHandler enemyAttackHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //HealthBarHandler Class
    private HealthBarHandler healthBarHandler;

    //CameraHandler Class
    private CameraHandler cameraHandler;

    public override void EnterState(GameStateManager gameState)
    {
        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

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

        Debug.Log("gamestate is enemy turn");

        enemyAttackHandler.orderedEnemyGOList.Clear();

        //this list needs to change as enemy units die btw
        foreach (GameObject enemyUnitGO in unitHandler.enemyUnitGOList)
        {
            //cameraHandler.DisableCamera(cameraHandler.attackCamera);
            //cameraHandler.EnableCamera(cameraHandler.mainCamera);

            Debug.Log(enemyUnitGO.name);

            Unit enemyUnit = enemyUnitGO.GetComponent<UnitData>().unit;

            Unit target = enemyAttackHandler.GetTarget(enemyUnit);

            (int, int) optimalTile = enemyAttackHandler.GetOptimalTileForAtk(enemyUnit, target);

            Debug.Log(enemyUnit.name + " will attack: " + target.name);
            Debug.Log(enemyUnit.name + " will attack from tile: " + optimalTile);

            enemyUnit.target = target;
            enemyUnit.optimalTile = (optimalTile.Item1, optimalTile.Item2);

            if (optimalTile != (99, 99))
            {
                //dont forget to clear this when player turn over.
                enemyAttackHandler.orderedEnemyGOList.Add(enemyUnitGO);
                //enemyAttackHandler.MoveEnemyUnitTo(optimalTile.Item1, optimalTile.Item2, enemyUnitGO);

                enemyAttackHandler.listOfUnmovedEnemies.Add(enemyUnit);
            }

            /*

            //smth like while the turn is not completed, do not iterate again.

            cameraHandler.DisableCamera(cameraHandler.mainCamera);
            cameraHandler.EnableCamera(cameraHandler.attackCamera);

            //sets up health bars
            healthBarHandler.SetFriendlyMaxHealth(target);
            healthBarHandler.SetEnemyMaxHealth(enemyUnit);

            //sets up camera angles
            cameraHandler.SetAttackCamera(targetGO, enemyUnitGO, coordsHandler);

            canvasHandler.DisableCanvas(canvasHandler.attackCanvasW);
            canvasHandler.DisableCanvas(canvasHandler.attackCanvasS);
            canvasHandler.DisableCanvas(canvasHandler.attackCanvasA);
            canvasHandler.EnableCanvas(canvasHandler.attackCanvas);
            canvasHandler.DisableAllUnitUICanvases();
            */
        }

        if (enemyAttackHandler.orderedEnemyGOList.Count == 0)
        {
            gameState.SwitchState(gameState.turnStart);
        }

        else
        {
            gameState.SwitchState(gameState.enemyMovement);
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {

    }
}
