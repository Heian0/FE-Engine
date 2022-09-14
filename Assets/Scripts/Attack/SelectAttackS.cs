using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SelectAttackS : GameBaseState
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

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is select attack skills (new gamestate mamager)");

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

        canvasHandler.DisableCanvas(canvasHandler.attackCanvas);
        canvasHandler.EnableCanvas(canvasHandler.attackCanvasS);

        attackButtonHandler.SetSkillButtons(unitHandler.selectedUnitSkillset);
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameState.SwitchState(gameState.viewAttack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //attack with skill one

            attackHandler.AttackS(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnitSkillset.skillOne, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap) );

            unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
            unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

            //if skill need to move unit afer execution switch state to view skill
            switch(unitHandler.selectedUnitSkillset.skillOne.skillIndex)
            {
                case 0: case 1: case 2:

                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));

                    break;

                case 3:

                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.viewSkill, 2.0f));

                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //attack with skill two

            attackHandler.AttackS(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnitSkillset.skillTwo, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap) );

            unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
            unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

            switch (unitHandler.selectedUnitSkillset.skillOne.skillIndex)
            {
                case 0: case 1: case 2:

                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));

                    break;

                case 3:

                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.viewSkill, 2.0f));

                    break;
            }
        }
    }
}
