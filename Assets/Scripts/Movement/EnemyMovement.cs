using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //EnemyAttackHandler Class
    private EnemyAttackHandler enemyAttackHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

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

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        cameraHandler.DisableCamera(cameraHandler.attackCamera);
        cameraHandler.EnableCamera(cameraHandler.mainCamera);

        GameObject enemyUnitGO = enemyAttackHandler.orderedEnemyGOList[enemyAttackHandler.currentEnemyIndex];
        Unit enemyUnit = enemyUnitGO.GetComponent<UnitData>().unit;

        TileMap.tiles[coordsHandler.GetTileX(enemyUnitGO), coordsHandler.GetTileZ(enemyUnitGO)] = TileMap.terrainTiles[coordsHandler.GetTileX(enemyUnitGO), coordsHandler.GetTileZ(enemyUnitGO)];
        enemyAttackHandler.MoveEnemyUnitTo(enemyUnit.optimalTile.Item1, enemyUnit.optimalTile.Item2, enemyUnitGO);
        enemyAttackHandler.listOfUnmovedEnemies.Remove(enemyUnit);

        coordsHandler.SetUnitX(enemyUnitGO, enemyUnit.optimalTile.Item1);
        coordsHandler.SetUnitY(enemyUnitGO, 0);
        coordsHandler.SetUnitZ(enemyUnitGO, enemyUnit.optimalTile.Item2);

        TileMap.tiles[coordsHandler.GetTileX(enemyUnitGO), coordsHandler.GetTileZ(enemyUnitGO)] = 3;

        gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.enemyAttack, 2.0f));
    }

    public override void UpdateState(GameStateManager gameState)
    {

    }
}
