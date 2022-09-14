/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTwoMovement : MonoBehaviour
{
    private bool IsMoving;
    private Vector3 OrigPos, UnitTargetPos;
    private float TimeToMove = 0.2f;

    public GameObject selectedUnit;

    public int unitMovementRangeStartX;
    public int unitMovementRangeStartZ;
    public int unitMovementRangeEndX;
    public int unitMovementRangeEndZ;

    public int UnitTwoRange = 2;

    public static int UnitTwoX;
    public static int UnitTwoZ;

    public TileMap map;
    public List<Node> currentPath = null;
    public TileType[] tileTypes;

    public List<AccTile> accessibleTiles = new List<AccTile>();

    void Start()
    {
        UnitTwoX = selectedUnit.GetComponent<UnitData>().tileX;
        UnitTwoZ = selectedUnit.GetComponent<UnitData>().tileZ;
    }

    void Update()
    {
        if (StateController.CurrentState == GameState.UnitSelected
            && CursorMovement.unitSelected == "Unit Two")
        {
            //sets the smaller graph of max possible movement (no obstacles) so that program has less tiles to check.
            unitMovementRangeStartX = selectedUnit.GetComponent<UnitData>().tileX - UnitTwoRange;
            unitMovementRangeStartZ = selectedUnit.GetComponent<UnitData>().tileZ - UnitTwoRange;
            unitMovementRangeEndX = selectedUnit.GetComponent<UnitData>().tileX + UnitTwoRange;
            unitMovementRangeEndZ = selectedUnit.GetComponent<UnitData>().tileZ + UnitTwoRange;

            if (unitMovementRangeStartX < 0)
            {
                unitMovementRangeStartX = 0;
            }

            if (unitMovementRangeStartZ < 0)
            {
                unitMovementRangeStartZ = 0;
            }

            if (unitMovementRangeEndX > TileMap.mapSizeX - 1)
            {
                unitMovementRangeEndX = TileMap.mapSizeX - 1;
            }

            if (unitMovementRangeEndZ > TileMap.mapSizeZ - 1)
            {
                unitMovementRangeEndZ = TileMap.mapSizeZ - 1;
            }

            for (int x = unitMovementRangeStartX; x < unitMovementRangeEndX + 1; x++)
            {
                for (int z = unitMovementRangeStartZ; z < unitMovementRangeEndZ + 1; z++)
                {
                    float pathCost = map.GeneratePathTo(x, z, selectedUnit);
                    //Debug.Log(pathCost);

                    if (pathCost - 1 <= UnitTwoRange)
                    {
                        //Debug.Log("in range");

                        accessibleTiles.Add(new AccTile { AccTileX = x, AccTileZ = z });

                        TileType tt = map.GetTileType(x, z);

                        GameObject tileGO = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 1, z), Quaternion.identity);
                    }
                }
            }

            StateController.CurrentState = GameState.MoveUnit;
        }

        //"deselects" the unit
        if (Input.GetKeyDown(KeyCode.S)
            && StateController.CurrentState == GameState.MoveUnit
            && CursorMovement.unitSelected == "Unit Two")
        {
            for (int x = unitMovementRangeStartX; x < unitMovementRangeEndX + 1; x++)
            {
                for (int z = unitMovementRangeStartZ; z < unitMovementRangeEndZ + 1; z++)
                {
                    //visuals for deselction, will be changed from raised tiles back to normal ones (later blue to original)
                    TileType tt = map.GetTileType(x, z);

                    GameObject tileGO = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);
                }
            }

            StateController.CurrentState = GameState.SelectTile;
            Debug.Log("Game State is Select Tile");

            CursorMovement.unitSelected = "None";
        }

        //change last bool tester eventually to if the neighbour node above does not equal not traversable then run the if statement
        if (Input.GetKeyDown(KeyCode.UpArrow) && !IsMoving
            && CursorMovement.unitSelected == "Unit Two"
            && StateController.CurrentState == GameState.MoveUnit
            && GetAccTile(selectedUnit.GetComponent<UnitData>().tileX, selectedUnit.GetComponent<UnitData>().tileZ + 1) == true)
        {
            StartCoroutine(MoveUnit(new Vector3(0, 0, 1)));

            //increases z coordinate of selected unit by 1
            selectedUnit.GetComponent<UnitData>().tileZ = selectedUnit.GetComponent<UnitData>().tileZ + 1;
            UnitTwoZ = UnitTwoZ + 1;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !IsMoving
            && CursorMovement.unitSelected == "Unit Two"
            && StateController.CurrentState == GameState.MoveUnit
            && GetAccTile(selectedUnit.GetComponent<UnitData>().tileX, selectedUnit.GetComponent<UnitData>().tileZ - 1) == true)
        {
            StartCoroutine(MoveUnit(new Vector3(0, 0, -1)));

            //minuses z coordinate of selected unit by 1
            selectedUnit.GetComponent<UnitData>().tileZ = selectedUnit.GetComponent<UnitData>().tileZ - 1;
            UnitTwoZ = UnitTwoZ - 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && !IsMoving
            && CursorMovement.unitSelected == "Unit Two"
            && StateController.CurrentState == GameState.MoveUnit
            && GetAccTile(selectedUnit.GetComponent<UnitData>().tileX + 1, selectedUnit.GetComponent<UnitData>().tileZ) == true)
        {
            StartCoroutine(MoveUnit(new Vector3(1, 0, 0)));

            //increases z coordinate of selected unit by 1
            selectedUnit.GetComponent<UnitData>().tileX = selectedUnit.GetComponent<UnitData>().tileX + 1;
            UnitTwoX = UnitTwoX + 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !IsMoving
            && CursorMovement.unitSelected == "Unit Two"
            && StateController.CurrentState == GameState.MoveUnit
            && GetAccTile(selectedUnit.GetComponent<UnitData>().tileX - 1, selectedUnit.GetComponent<UnitData>().tileZ) == true)
        {
            StartCoroutine(MoveUnit(new Vector3(-1, 0, 0)));

            //minuses x coordinate of selected unit by 1
            selectedUnit.GetComponent<UnitData>().tileX = selectedUnit.GetComponent<UnitData>().tileX - 1;
            UnitTwoX = UnitTwoX - 1;
        }
    }

    public bool GetAccTile(int x, int z)
    {
        foreach (AccTile AT in accessibleTiles)
        {
            if (AT.AccTileX == x && AT.AccTileZ == z)
            {
                return true;
            }
        }

        return false;
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