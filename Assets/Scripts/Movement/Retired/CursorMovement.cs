/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    private bool IsMoving;
    private Vector3 OrigPos, UnitTargetPos;
    private float TimeToMove = 0.2f;

    public GameObject cursor;
    
    public TileMap map;

    public static string unitSelected;

    public int CursorX;
    public int CursorZ;

    void Start()
    {
        unitSelected = "None";

        CursorX = 0;
        CursorZ = 0;
    }


    void Update()
    {
        //----------------------------------------------MOVE CURSOR----------------------------------------------------------//

        //change last bool tester eventually to if the neighbour node above does not equal not traversable then run the if statement
        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsMoving && cursor.GetComponent<UnitData>().tileZ != TileMap.mapSizeZ - 1
            &&  StateController.CurrentState == GameState.SelectTile)
        {
            StartCoroutine(MoveUnit(new Vector3(0, 0, 1)));

            //increases z coordinate of cursor by 1
            cursor.GetComponent<UnitData>().tileZ = cursor.GetComponent<UnitData>().tileZ + 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !IsMoving && cursor.GetComponent<UnitData>().tileZ != 0
            && StateController.CurrentState == GameState.SelectTile)
        {
            StartCoroutine(MoveUnit(new Vector3(0, 0, -1)));

            //minuses z coordinate of cursor by 1
            cursor.GetComponent<UnitData>().tileZ = cursor.GetComponent<UnitData>().tileZ - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !IsMoving && cursor.GetComponent<UnitData>().tileZ != TileMap.mapSizeX - 1
            && StateController.CurrentState == GameState.SelectTile)
        {
            StartCoroutine(MoveUnit(new Vector3(1, 0, 0)));

            //increases z coordinate of cursor by 1
            cursor.GetComponent<UnitData>().tileX = cursor.GetComponent<UnitData>().tileX + 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !IsMoving && cursor.GetComponent<UnitData>().tileX != 0
            && StateController.CurrentState == GameState.SelectTile)
        {
            StartCoroutine(MoveUnit(new Vector3(-1, 0, 0)));

            //minuses x coordinate of cursor by 1
            cursor.GetComponent<UnitData>().tileX = cursor.GetComponent<UnitData>().tileX - 1;
        }

        //----------------------------------------------SELECT UNIT--------------------------------------------------//

        if (Input.GetKeyDown(KeyCode.A))
        {
            StateController.CurrentState = GameState.UnitSelected;
            UnitSelected.selectedUnit = GameObject.Find("UnitOne");
            UnitSelected.selectedUnitRange = 3;
            Debug.Log("Game State is Unit Selected, unit1 selected");
            /*
            CursorX = GetCursorX();
            CursorZ = GetCursorZ();

            //Debug.Log(CursorX);
            //Debug.Log(CursorZ);

            if (CursorX == UnitOneMovement.UnitOneX 
                && CursorZ == UnitOneMovement.UnitOneZ)
            {
                StateController.CurrentState = GameState.UnitSelected;
                unitSelected = "Unit One";
                Debug.Log("Game State is Unit Selected");

                return;
            }

            if (CursorX == UnitTwoMovement.UnitTwoX
                && CursorZ == UnitTwoMovement.UnitTwoZ)
            {
                StateController.CurrentState = GameState.UnitSelected;
                Debug.Log("Game State is Unit Selected");
                unitSelected = "Unit Two";

                return;
            }

            else
            {
                StateController.CurrentState = GameState.SelectAction;
                Debug.Log("Game State is Select Action");
                unitSelected = "None";

                return;
            }

            //unitMovement.selectedUnit = GameObject.FindWithTag("UnitOne");
            //map.selectedUnit = GameObject.FindWithTag("UnitOne");
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StateController.CurrentState = GameState.UnitSelected;
            UnitSelected.selectedUnit = GameObject.Find("UnitTwo");
            UnitSelected.selectedUnitRange = 2;
            Debug.Log("Game State is Unit Selected, unit2 selected");
        }

        if (Input.GetKeyDown(KeyCode.S)
            && StateController.CurrentState == GameState.SelectAction)
        {
            StateController.CurrentState = GameState.MoveUnit;
            Debug.Log("Game State is Move Unit");
        }

        if (Input.GetKeyDown(KeyCode.S)
        && StateController.CurrentState == GameState.MoveUnit
        && unitSelected == "None")
        {
            StateController.CurrentState = GameState.SelectTile;
            Debug.Log("Game State is Select Tile");
        }
    }

    public int GetCursorX()
    {
        return cursor.GetComponent<UnitData>().tileX;
    }

    public int GetCursorZ()
    {
        return cursor.GetComponent<UnitData>().tileZ;
    }

    private IEnumerator MoveUnit(Vector3 direction)
    {
        IsMoving = true;

        float ElapsedTime = 0;

        OrigPos = transform.position;
        UnitTargetPos = OrigPos + direction;

        while (ElapsedTime < TimeToMove)
        {
            transform.position = Vector3.Lerp(OrigPos, UnitTargetPos, (ElapsedTime / TimeToMove));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = UnitTargetPos;
        IsMoving = false;
    }
}
*/
