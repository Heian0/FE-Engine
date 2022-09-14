using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewAbility : GameBaseState
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

    //CursorHandler Class
    private CursorHandler cursorHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is view ability (new gamestate mamager)");

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

        //CursorHandler Class
        GameObject cursor = GameObject.Find("Cursor");
        cursorHandler = cursor.GetComponent<CursorHandler>();

        //moves cursor to be above selected unit
        cursorHandler.SetCursorPosition(coordsHandler.GetTileX(unitHandler.selectedUnitGO), 5, coordsHandler.GetTileZ(unitHandler.selectedUnitGO));
        //changed the cursor's data to its new position
        coordsHandler.SetUnitX(cursorHandler.cursor, coordsHandler.GetTileX(unitHandler.selectedUnitGO));
        coordsHandler.SetUnitY(cursorHandler.cursor, 5);
        coordsHandler.SetUnitZ(cursorHandler.cursor, coordsHandler.GetTileZ(unitHandler.selectedUnitGO));

        attackButtonHandler.ClearViewAbilityButtons();
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            pathHandler.DestroyATs();

            gameState.SwitchState(gameState.selectAction);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !cursorHandler.IsMoving
            && pathHandler.GetAtkTile(cursorHandler.cursor.GetComponent<UnitData>().tileX, cursorHandler.cursor.GetComponent<UnitData>().tileZ + 1) == true)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(0, 0, 1)));
            coordsHandler.IncreaseTileZ(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !cursorHandler.IsMoving
            && pathHandler.GetAtkTile(cursorHandler.cursor.GetComponent<UnitData>().tileX, cursorHandler.cursor.GetComponent<UnitData>().tileZ - 1) == true)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(0, 0, -1)));
            coordsHandler.DecreaseTileZ(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !cursorHandler.IsMoving
            && pathHandler.GetAtkTile(cursorHandler.cursor.GetComponent<UnitData>().tileX + 1, cursorHandler.cursor.GetComponent<UnitData>().tileZ) == true)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(1, 0, 0)));
            coordsHandler.IncreaseTileX(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !cursorHandler.IsMoving
            && pathHandler.GetAtkTile(cursorHandler.cursor.GetComponent<UnitData>().tileX - 1, cursorHandler.cursor.GetComponent<UnitData>().tileZ) == true)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(-1, 0, 0)));
            coordsHandler.DecreaseTileX(cursorHandler.cursor);
        }

        //------------------------------------

        if (Input.GetKeyDown(KeyCode.A))
        {
            int cursorX = coordsHandler.GetTileX(cursorHandler.cursor);
            int cursorZ = coordsHandler.GetTileZ(cursorHandler.cursor);

            //Debug.Log(attackHandler.IsUnitAttackable(unitHandler.selectedUnit, unitHandler.GetUnitAt(cursorX, cursorZ)));

            GameObject unitHovered;
            unitHovered = GameObject.Find("Enemy/EnemyThief");

            if (coordsHandler.GetTileX(unitHovered) == cursorX
                &&
                coordsHandler.GetTileZ(unitHovered) == cursorZ
                &&
                attackHandler.IsUnitAttackable(unitHandler.selectedUnit, unitHandler.GetUnitAt(cursorX, cursorZ))
                )
            {
                unitHandler.SetSelectedEnemy(0, "Units/Enemy/EnemyThief");
                unitHandler.SetSelectedEnemyUnitInventory("UnitInventories/Enemy/EnemyThiefInventory");
                Debug.Log("enemy Thief selected");

                unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
                unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

                attackHandler.AttackA(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnit.unitAbilityset.abilityOne, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap));

                gameState.SwitchState(gameState.viewAttack);
                gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));

                return;
            }

            unitHovered = GameObject.Find("Enemy/EnemyArcher");

            if (coordsHandler.GetTileX(unitHovered) == cursorX
                &&
                coordsHandler.GetTileZ(unitHovered) == cursorZ
                &&
                attackHandler.IsUnitAttackable(unitHandler.selectedUnit, unitHandler.GetUnitAt(cursorX, cursorZ))
                )
            {
                unitHandler.SetSelectedEnemy(1, "Units/Enemy/EnemyArcher");
                unitHandler.SetSelectedEnemyUnitInventory("UnitInventories/Enemy/EnemyArcherInventory");
                Debug.Log("enemy Archer selected");

                unitHandler.movedUnitsList.Add(unitHandler.selectedUnit);
                unitHandler.unmovedUnitsList.Remove(unitHandler.selectedUnit);

                attackHandler.AttackA(unitHandler.selectedUnit, unitHandler.selectedEnemyUnit, unitHandler.selectedUnit.unitAbilityset.abilityOne, unitHandler.selectedEnemyUnitInventory.equippedWeapon, healthBarHandler,
                coordsHandler.CanEnemyAttack(unitHandler.selectedEnemyUnitGO, unitHandler.selectedEnemyUnitInventory, coordsHandler.GetTileX(unitHandler.selectedUnitGO), coordsHandler.GetTileZ(unitHandler.selectedUnitGO), tileMap));

                gameState.SwitchState(gameState.viewAttack);
                gameState.StartCoroutine(gameState.SwitchStateDelay(gameState.selectTile, 2.0f));

                return;
            }

            else
            {
                Debug.Log("No enemy selected to attack");
            }
        }
    }
}
