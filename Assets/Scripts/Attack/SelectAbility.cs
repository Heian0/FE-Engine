using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAbility : GameBaseState
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
        Debug.Log("game state is select ability (new gamestate mamager)");

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

        cameraHandler.EnableCamera(cameraHandler.mainCamera);
        cameraHandler.DisableCamera(cameraHandler.attackCamera);
        canvasHandler.DisableCanvas(canvasHandler.actionCanvas);
        canvasHandler.EnableCanvas(canvasHandler.abilityCanvas);

        attackButtonHandler.SetViewAbilityButtons(unitHandler.selectedUnitAbilityset);
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            pathHandler.DestroyATs();

            gameState.SwitchState(gameState.selectAction);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //not attacking with abilities
            switch (unitHandler.selectedUnit.unitAbilityset.abilityOne.abilityIndex)
            {
                //Dauntless Aura, does not need to go to view ability. no aiming
                case 0:

                    unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
                    unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

                    if (unitHandler.selectedUnit.unitAbilityset.abilityOne.abilityLevel == 0)
                    {
                        unitHandler.selectedUnit.attack = Mathf.RoundToInt(unitHandler.selectedUnit.attack * 1.25f + 0.01f);
                        unitHandler.selectedUnit.defense = Mathf.RoundToInt(unitHandler.selectedUnit.defense * 1.25f + 0.01f);
                        unitHandler.selectedUnit.magic = Mathf.RoundToInt(unitHandler.selectedUnit.magic * 1.25f + 0.01f);
                        unitHandler.selectedUnit.resistance = Mathf.RoundToInt(unitHandler.selectedUnit.resistance * 1.25f + 0.01f);
                        unitHandler.selectedUnit.speed = Mathf.RoundToInt(unitHandler.selectedUnit.speed * 1.25f + 0.01f);
                    }

                    if (unitHandler.selectedUnit.unitAbilityset.abilityOne.abilityLevel == 1)
                    {
                        unitHandler.selectedUnit.attack = Mathf.RoundToInt(unitHandler.selectedUnit.attack * 1.5f + 0.01f);
                        unitHandler.selectedUnit.defense = Mathf.RoundToInt(unitHandler.selectedUnit.defense * 1.5f + 0.01f);
                        unitHandler.selectedUnit.magic = Mathf.RoundToInt(unitHandler.selectedUnit.magic * 1.5f + 0.01f);
                        unitHandler.selectedUnit.resistance = Mathf.RoundToInt(unitHandler.selectedUnit.resistance * 1.5f + 0.01f);
                        unitHandler.selectedUnit.speed = Mathf.RoundToInt(unitHandler.selectedUnit.speed * 1.5f + 0.01f);
                    }

                    Debug.Log("Dauntless Aura active.");

                    gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));

                    break;

                //Zero Dawn, does need to go to view ability. aiming
                case 1:

                    int unitX = coordsHandler.GetTileX(unitHandler.GetUnitGO(unitHandler.selectedUnit));
                    int unitZ = coordsHandler.GetTileZ(unitHandler.GetUnitGO(unitHandler.selectedUnit));

                    for (int i = 0; i < TileMap.mapSizeX; i++)
                    {
                        pathHandler.DisplayAndAddAttackableTile(i, unitZ, tileMap.blueTile);
                    }

                    for (int i = 0; i < TileMap.mapSizeZ; i++)
                    {
                        pathHandler.DisplayAndAddAttackableTile(unitX, i, tileMap.blueTile);
                    }

                    gameState.SwitchState(gameState.viewAbility);

                    break;
            }
        }
    }
}
