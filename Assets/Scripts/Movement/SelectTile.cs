using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectTile : GameBaseState
{
    //unit the cursor is hovering above
    private GameObject unitHovered;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //CursorHandler Class
    private CursorHandler cursorHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    //UnitUIHandler Class
    private UnitUIHandler unitUIHandler;

    //CameraHandler Class
    private CameraHandler cameraHandler;

    //TileMap Class
    private TileMap tileMap;

    public override void EnterState(GameStateManager gameState)
    {
        Debug.Log("select tile (new gamestate manager)");

        //CameraHandler Class
        GameObject cameras = GameObject.Find("Cameras");
        cameraHandler = cameras.GetComponent<CameraHandler>();

        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        CanvasHandler canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //CursorHandler Class
        GameObject cursor = GameObject.Find("Cursor");
        cursorHandler = cursor.GetComponent<CursorHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //UnitUIHandler Class
        unitUIHandler = units.GetComponent<UnitUIHandler>();

        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //TileMap Class
        tileMap = map.GetComponent<TileMap>();

        cameraHandler.EnableCamera(cameraHandler.mainCamera);
        cameraHandler.DisableCamera(cameraHandler.attackCamera);
        canvasHandler.DisableCanvas(canvasHandler.actionCanvas);
        canvasHandler.DisableCanvas(canvasHandler.abilityCanvas);
        canvasHandler.EnableAllUnitUICanvases();

        if (unitHandler.unmovedUnitsList.Any() == false)
        {
            gameState.SwitchState(gameState.turnStart);
        }
    }

    public override void UpdateState(GameStateManager gameState)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !cursorHandler.IsMoving && coordsHandler.GetTileZ(cursorHandler.cursor) != TileMap.mapSizeZ - 1)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(0, 0, 1)));
            coordsHandler.IncreaseTileZ(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !cursorHandler.IsMoving && coordsHandler.GetTileZ(cursorHandler.cursor) != 0)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(0, 0, -1)));
            coordsHandler.DecreaseTileZ(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !cursorHandler.IsMoving && coordsHandler.GetTileX(cursorHandler.cursor) != TileMap.mapSizeX - 1)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(1, 0, 0)));
            coordsHandler.IncreaseTileX(cursorHandler.cursor);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !cursorHandler.IsMoving && coordsHandler.GetTileX(cursorHandler.cursor) != 0)
        {
            gameState.StartCoroutine(cursorHandler.MoveCursor(new Vector3(-1, 0, 0)));
            coordsHandler.DecreaseTileX(cursorHandler.cursor);
        }

        //----------------------------------------------SELECT UNIT--------------------------------------------------//

        if (Input.GetKeyDown(KeyCode.A))
        {
            unitHovered = GameObject.Find("Cassius");

            if (
                coordsHandler.GetTileX(unitHovered) == coordsHandler.GetTileX(cursorHandler.cursor)
                &&
                coordsHandler.GetTileZ(unitHovered) == coordsHandler.GetTileZ(cursorHandler.cursor)
                )
            {
                unitHandler.SetSelectedUnit(0, "Units/Friendly/Cassius");
                unitHandler.SetSelectedUnitInventory("UnitInventories/Friendly/CassiusInventory");
                unitHandler.SetSelectedUnitSkillset("UnitSkillSets/CassiusSkillset");
                unitHandler.SetSelectedUnitAbilityset("UnitAbilitySets/CassiusAbilityset");

                //destroys unmoved unit ui
                GameObject.Destroy(unitUIHandler.personalUnitUIDict[unitHovered]);

                gameState.SwitchState(gameState.unitSelected);
            }

            unitHovered = GameObject.Find("Koro");

            if (
                coordsHandler.GetTileX(unitHovered) == coordsHandler.GetTileX(cursorHandler.cursor)
                &&
                coordsHandler.GetTileZ(unitHovered) == coordsHandler.GetTileZ(cursorHandler.cursor)
                )
            {
                unitHandler.SetSelectedUnit(1, "Units/Friendly/Koro");
                unitHandler.SetSelectedUnitInventory("UnitInventories/Friendly/KoroInventory");
                unitHandler.SetSelectedUnitSkillset("UnitSkillSets/KoroSkillset");
                unitHandler.SetSelectedUnitAbilityset("UnitAbilitySets/KoroAbilityset");

                //destroys unmoved unit ui
                GameObject.Destroy(unitUIHandler.personalUnitUIDict[unitHovered]);

                gameState.SwitchState(gameState.unitSelected);
            }

            unitHovered = GameObject.Find("Cynthia");

            if (
            coordsHandler.GetTileX(unitHovered) == coordsHandler.GetTileX(cursorHandler.cursor)
            &&
            coordsHandler.GetTileZ(unitHovered) == coordsHandler.GetTileZ(cursorHandler.cursor)
                )
            {
                unitHandler.SetSelectedUnit(2, "Units/Friendly/Cynthia");
                unitHandler.SetSelectedUnitInventory("UnitInventories/Friendly/CynthiaInventory");
                unitHandler.SetSelectedUnitSkillset("UnitSkillSets/CynthiaSkillset");
                unitHandler.SetSelectedUnitAbilityset("UnitAbilitySets/CynthiaAbilityset");

                //destroys unmoved unit ui
                GameObject.Destroy(unitUIHandler.personalUnitUIDict[unitHovered]);

                gameState.SwitchState(gameState.unitSelected);
            }
            //add ability to select an empty tile and go to select action state, kinda like a menu.
        }

        //for testing only
        if (Input.GetKeyDown(KeyCode.F))
        {
            gameState.SwitchState(gameState.enemyTurn);
        }
    }
}
