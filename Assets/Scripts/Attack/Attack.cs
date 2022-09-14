using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : GameBaseState
{
    //CursorHandler Class
    private CursorHandler cursorHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //PathHandler Class
    private PathHandler pathHandler;

    //CanvasHandler Class
    private CanvasHandler canvasHandler;

    //CameraHandler Class
    private CameraHandler cameraHandler;

    //unit the cursor is hovering above
    private GameObject unitHovered;

    //AttackHandler Class
    private AttackHandler attackHandler;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("game state is attack (new gamestate mamager)");

        //CursorHandler Class
        GameObject cursor = GameObject.Find("Cursor");
        cursorHandler = cursor.GetComponent<CursorHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //PathHandler Class
        pathHandler = map.GetComponent<PathHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        //AttackHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        attackHandler = SelectAttackCanvases.GetComponent<AttackHandler>();

        //moves cursor to be above selected unit
        cursorHandler.SetCursorPosition(coordsHandler.GetTileX(unitHandler.selectedUnitGO), 5, coordsHandler.GetTileZ(unitHandler.selectedUnitGO));
        //changed the cursor's data to its new position
        coordsHandler.SetUnitX(cursorHandler.cursor, coordsHandler.GetTileX(unitHandler.selectedUnitGO));
        coordsHandler.SetUnitY(cursorHandler.cursor, 5);
        coordsHandler.SetUnitZ(cursorHandler.cursor, coordsHandler.GetTileZ(unitHandler.selectedUnitGO));

        cameraHandler.DisableCamera(cameraHandler.attackCamera);
        cameraHandler.EnableCamera(cameraHandler.mainCamera);

        canvasHandler.EnableAllUnitUICanvases();
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

        //----------------------------------------------SELECT Enemy to attack--------------------------------------------------//

        if (Input.GetKeyDown(KeyCode.A))
        {
            int cursorX = coordsHandler.GetTileX(cursorHandler.cursor);
            int cursorZ = coordsHandler.GetTileZ(cursorHandler.cursor);

            Unit target = unitHandler.GetUnitAt(cursorX, cursorZ);

            switch(target.gameObjectIndex)
            {
                case 3: //Enemy Thief

                    if (attackHandler.IsUnitAttackable(unitHandler.selectedUnit, target))
                    {
                        unitHandler.SetSelectedEnemy(0, "Units/Enemy/EnemyThief");
                        unitHandler.SetSelectedEnemyUnitInventory("UnitInventories/Enemy/EnemyThiefInventory");
                        Debug.Log("enemy Thief selected");

                        gameState.SwitchState(gameState.viewAttack);
                    }

                    else
                    {
                        Debug.Log("No enemy selected to attack");
                    }

                    break;

                case 4: //Enemy Archer

                    if (attackHandler.IsUnitAttackable(unitHandler.selectedUnit, target))
                    {
                        unitHandler.SetSelectedEnemy(1, "Units/Enemy/EnemyArcher");
                        unitHandler.SetSelectedEnemyUnitInventory("UnitInventories/Enemy/EnemyArcherInventory");
                        Debug.Log("enemy Archer selected");

                        gameState.SwitchState(gameState.viewAttack);
                    }

                    else
                    {
                        Debug.Log("No enemy selected to attack");
                    }

                    break;

                case 5: //Enemy Mortal Savant

                    if (attackHandler.IsUnitAttackable(unitHandler.selectedUnit, target))
                    {
                        unitHandler.SetSelectedEnemy(2, "Units/Enemy/EnemyMSvnt");
                        unitHandler.SetSelectedEnemyUnitInventory("UnitInventories/Enemy/EnemyMSvntInventory");
                        Debug.Log("enemy Mortal Savant selected");

                        gameState.SwitchState(gameState.viewAttack);
                    }

                    else
                    {
                        Debug.Log("No enemy selected to attack");
                    }

                    break;

                case 99: //None

                    Debug.Log("No enemy selected to attack");

                    break;
            }

            /*
            //Debug.Log(attackHandler.IsUnitAttackable(unitHandler.selectedUnit, unitHandler.GetUnitAt(cursorX, cursorZ)));

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

                gameState.SwitchState(gameState.viewAttack);

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

                gameState.SwitchState(gameState.viewAttack);

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

                gameState.SwitchState(gameState.viewAttack);

                return;
            }

            else
            {
                Debug.Log("No enemy selected to attack");
            }
            */
        }
    }
}
