using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnStart : GameBaseState
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //UnitUIHandler Class
    private UnitUIHandler unitUIHandler;

    //TileMap Class
    private TileMap tileMap;

    private bool firstFrame = true;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("Player Turn.");

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //UnitUIHandler Class
        unitUIHandler = units.GetComponent<UnitUIHandler>();

        //TileMap Class
        GameObject map = GameObject.Find("Map");
        tileMap = map.GetComponent<TileMap>();

        if (firstFrame == false)
        {
            unitUIHandler.ResetAllUnitMovableUI(tileMap.yellowTile);
            unitHandler.ResetMovedUnits();
            unitHandler.ResetUnmovedUnits();
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (firstFrame == true)
        {
            unitUIHandler.SetAllUnitMovableUI(tileMap.yellowTile);

            firstFrame = false;
        }

        gameState.SwitchState(gameState.selectTile);
    }
}
