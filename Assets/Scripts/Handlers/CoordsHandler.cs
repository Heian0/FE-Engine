using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordsHandler : MonoBehaviour
{
    //UnitHandler Class
    private UnitHandler unitHandler;

    //TileMap Class
    private TileMap tileMap;

    public int selectedUnitOgX;
    public int selectedUnitOgY;
    public int selectedUnitOgZ;

    public int unitMovementRangeStartX;
    public int unitMovementRangeStartZ;
    public int unitMovementRangeEndX;
    public int unitMovementRangeEndZ;

    public int unitAttackRangeStartX;
    public int unitAttackRangeStartZ;
    public int unitAttackRangeEndX;
    public int unitAttackRangeEndZ;

    public int unitMvAndAtkRangeStartX;
    public int unitMvAndAtkRangeStartZ;
    public int unitMvAndAtkRangeEndX;
    public int unitMvAndAtkRangeEndZ;

    public int targetMvAndAtkRangeStartX;
    public int targetMvAndAtkRangeStartZ;
    public int targetMvAndAtkRangeEndX;
    public int targetMvAndAtkRangeEndZ;

    public bool selectedUnitMoved;

    void Start()
    {
        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //TileMap Class
        GameObject map = GameObject.Find("Map");
        tileMap = map.GetComponent<TileMap>();
    }

    public void IncreaseTileX(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileX = go.GetComponent<UnitData>().tileX + 1;
    }

    public void DecreaseTileX(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileX = go.GetComponent<UnitData>().tileX - 1;
    }

    public void IncreaseTileY(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileY = go.GetComponent<UnitData>().tileY + 1;
    }

    public void DecreaseTileY(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileY = go.GetComponent<UnitData>().tileY + 1;
    }
    public void IncreaseTileZ(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileZ = go.GetComponent<UnitData>().tileZ + 1;
    }

    public void DecreaseTileZ(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return;
        }

        go.GetComponent<UnitData>().tileZ = go.GetComponent<UnitData>().tileZ - 1;
    }

    public int GetTileX(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return 0;
        }

        return go.GetComponent<UnitData>().tileX;
    }

    public int GetTileY(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return 0;
        }

        return go.GetComponent<UnitData>().tileY;
    }

    public int GetTileZ(GameObject go)
    {
        if (go.GetComponent<UnitData>() == null)
        {
            Debug.Log("this gameobject does not have the component Unit Data");

            return 0;
        }

        return go.GetComponent<UnitData>().tileZ;
    }

    public void SetUnitX(GameObject go, int i)
    {
        go.GetComponent<UnitData>().tileX = i;
    }

    public void SetUnitY(GameObject go, int i)
    {
        go.GetComponent<UnitData>().tileY = i;
    }

    public void SetUnitZ(GameObject go, int i)
    {
        go.GetComponent<UnitData>().tileZ = i;
    }

    public void SetUnitOgX(GameObject go)
    {
        selectedUnitOgX = go.GetComponent<UnitData>().tileX;
    }

    public void SetUnitOgY(GameObject go)
    {
        selectedUnitOgY = go.GetComponent<UnitData>().tileY;
    }

    public void SetUnitOgZ(GameObject go)
    {
        selectedUnitOgZ = go.GetComponent<UnitData>().tileZ;
    }

    public void SetUnitMvmtGrid(GameObject selectedUnitGO, Unit selectedUnit)
    {
        //Debug.Log("Tile x is: " + GetTileX(selectedUnitGO));
        //Debug.Log("Tile z is: " + GetTileZ(selectedUnitGO));

        unitMovementRangeStartX = GetTileX(selectedUnitGO) - selectedUnit.movementRange;
        unitMovementRangeStartZ = GetTileZ(selectedUnitGO) - selectedUnit.movementRange;
        unitMovementRangeEndX = GetTileX(selectedUnitGO) + selectedUnit.movementRange;
        unitMovementRangeEndZ = GetTileZ(selectedUnitGO) + selectedUnit.movementRange;

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

        //Debug.Log(unitMovementRangeStartX);
        //Debug.Log(unitMovementRangeStartZ);
        //Debug.Log(unitMovementRangeEndX);
        //Debug.Log(unitMovementRangeEndZ);
    }
    public void SetUnitAtkGrid(GameObject selectedUnitGO, UnitInventory selectedUnitInventory, Unit selectedUnit)
    {
        int largestRange = GetLargestRange(selectedUnit);

        unitAttackRangeStartX = GetTileX(selectedUnitGO) - largestRange;
        unitAttackRangeStartZ = GetTileZ(selectedUnitGO) - largestRange;
        unitAttackRangeEndX = GetTileX(selectedUnitGO) + largestRange;
        unitAttackRangeEndZ = GetTileZ(selectedUnitGO) + largestRange;

        if (unitAttackRangeStartX < 0)
        {
            unitAttackRangeStartX = 0;
        }

        if (unitAttackRangeStartZ < 0)
        {
            unitAttackRangeStartZ = 0;
        }

        if (unitAttackRangeEndX > TileMap.mapSizeX - 1)
        {
            unitAttackRangeEndX = TileMap.mapSizeX - 1;
        }

        if (unitAttackRangeEndZ > TileMap.mapSizeZ - 1)
        {
            unitAttackRangeEndZ = TileMap.mapSizeZ - 1;
        }
    }

    public void SetUnitAtkMvmtGrid(int x, int z, int range)
    {
        unitAttackRangeStartX = x - range;
        unitAttackRangeStartZ = z - range;
        unitAttackRangeEndX = x + range;
        unitAttackRangeEndZ = z + range;

        if (unitAttackRangeStartX < 0)
        {
            unitAttackRangeStartX = 0;
        }

        if (unitAttackRangeStartZ < 0)
        {
            unitAttackRangeStartZ = 0;
        }

        if (unitAttackRangeEndX > TileMap.mapSizeX - 1)
        {
            unitAttackRangeEndX = TileMap.mapSizeX - 1;
        }

        if (unitAttackRangeEndZ > TileMap.mapSizeZ - 1)
        {
            unitAttackRangeEndZ = TileMap.mapSizeZ - 1;
        }
    }

    public void SetUnitMvAndAtkGrid(GameObject selectedUnitGO, UnitInventory selectedUnitInventory, Unit selectedUnit)
    {
        unitMvAndAtkRangeStartX = GetTileX(selectedUnitGO) - selectedUnit.movementRange - selectedUnitInventory.equippedWeapon.range;
        unitMvAndAtkRangeStartZ = GetTileZ(selectedUnitGO) - selectedUnit.movementRange - selectedUnitInventory.equippedWeapon.range;
        unitMvAndAtkRangeEndX = GetTileX(selectedUnitGO) + selectedUnit.movementRange + selectedUnitInventory.equippedWeapon.range;
        unitMvAndAtkRangeEndZ = GetTileZ(selectedUnitGO) + selectedUnit.movementRange + selectedUnitInventory.equippedWeapon.range;

        if (unitMvAndAtkRangeStartX < 0)
        {
            unitMvAndAtkRangeStartX = 0;
        }

        if (unitMvAndAtkRangeStartZ < 0)
        {
            unitMvAndAtkRangeStartZ = 0;
        }

        if (unitMvAndAtkRangeEndX > TileMap.mapSizeX - 1)
        {
            unitMvAndAtkRangeEndX = TileMap.mapSizeX - 1;
        }

        if (unitMvAndAtkRangeEndZ > TileMap.mapSizeZ - 1)
        {
            unitMvAndAtkRangeEndZ = TileMap.mapSizeZ - 1;
        }
    }

    public void SetTargetMvAndAtkGrid(Unit target, Unit enemyUnit)
    {
        targetMvAndAtkRangeStartX = GetTileX(target.unitGameObject) - enemyUnit.movementRange - enemyUnit.unitInventory.equippedWeapon.range;
        targetMvAndAtkRangeStartZ = GetTileZ(target.unitGameObject) - enemyUnit.movementRange - enemyUnit.unitInventory.equippedWeapon.range;
        targetMvAndAtkRangeEndX = GetTileX(target.unitGameObject) + enemyUnit.movementRange + enemyUnit.unitInventory.equippedWeapon.range;
        targetMvAndAtkRangeEndZ = GetTileZ(target.unitGameObject) + enemyUnit.movementRange + enemyUnit.unitInventory.equippedWeapon.range;

        if (unitMvAndAtkRangeStartX < 0)
        {
            targetMvAndAtkRangeStartX = 0;
        }

        if (unitMvAndAtkRangeStartZ < 0)
        {
            targetMvAndAtkRangeStartZ = 0;
        }

        if (unitMvAndAtkRangeEndX > TileMap.mapSizeX - 1)
        {
            targetMvAndAtkRangeEndX = TileMap.mapSizeX - 1;
        }

        if (unitMvAndAtkRangeEndZ > TileMap.mapSizeZ - 1)
        {
            targetMvAndAtkRangeEndZ = TileMap.mapSizeZ - 1;
        }
    }

    public int GetLargestRange(Unit unit)
    {
        int largestRange = 0;

        foreach (Weapon weapon in unit.unitInventory.weaponsList)
        {
            if (largestRange < weapon.range)
            {
                largestRange = weapon.range;
            }
        }

        foreach (Skill skill in unit.unitSkillset.skillsList)
        {
            if (largestRange < skill.range)
            {
                largestRange = skill.range;
            }
        }

        return largestRange;
    }

    public void SetSelectedUnitMoved(bool moved)
    {
        selectedUnitMoved = moved;
    }

    public void ReturnToOg(GameObject selectedUnitGO)
    {
        selectedUnitGO.transform.position = new Vector3(selectedUnitOgX, 0.5f, selectedUnitOgZ);
    }

    public void ResetOrigTileTerrain(int x, int z)
    {
        TileMap.tiles[x, z] =
        TileMap.terrainTiles[x, z];
    }

    public void SetTileTerrainOccupied(int x, int z)
    {
        TileMap.tiles[x, z] = 3;
    }

    public bool CanEnemyAttack(GameObject selectedEnemyGO, UnitInventory selectedEnemyUnitInventory, int unitX, int unitZ, TileMap tileMap)
    {
        float pathCost = tileMap.GenerateAttackPathTo(unitX, unitZ, selectedEnemyGO);

        if (pathCost <= selectedEnemyUnitInventory.equippedWeapon.range + 1)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public Unit GetUnitAt(int x, int z)
    {
        foreach (GameObject unitGO in unitHandler.friendlyUnitGOList)
        {
            if (x == GetTileX(unitGO)
                && z == GetTileZ(unitGO))
            {
                return unitGO.GetComponent<UnitData>().unit;
            }
        }

        return null;
        //return Resources.Load<Unit>("Units/None");
    }

    //returns a list of enemy units that can attack a tile at the passed coordinates.
    public List<Unit> GetListOfEnemiesThatCanAtkAt(int x, int z)
    {
        List<Unit> listOfEnemiesThatCanAtk = new List<Unit>();


        foreach (GameObject enemyUnitGO in unitHandler.enemyUnitGOList)
        {
            List<(int, int)> mvmtTilesList = new List<(int, int)>();
            Unit enemyUnit = enemyUnitGO.GetComponent<UnitData>().unit;

            //Get tiles each unit can move to and put them in mvmtTilesList
            SetUnitMvmtGrid(enemyUnitGO, enemyUnit);

            for (int stX = unitMovementRangeStartX; stX < unitMovementRangeEndX + 1; stX++)
            {
                for (int stZ = unitMovementRangeStartZ; stZ < unitMovementRangeEndZ + 1; stZ++)
                {
                    float pathCost = tileMap.GeneratePathTo(x, z, enemyUnitGO);

                    if (pathCost - 1 <= enemyUnit.movementRange)
                    {
                        (int, int) tile = (stX, stZ);
                        mvmtTilesList.Add(tile);
                    }
                }
            }

            //for each tile in each unit's mvmtTilesList, see if that unit can attack a tile at x, z with their equipped weapon from the tile location
            foreach ((int, int) tile in mvmtTilesList)
            {
                SetUnitAtkMvmtGrid(tile.Item1, tile.Item2, unitHandler.GetUnitBiggestAtkRange(enemyUnit));

                float pathCost = tileMap.GenerateMvAtkPathTo(x, z, tile.Item1, tile.Item2, enemyUnitGO);

                if (pathCost <= enemyUnit.unitInventory.equippedWeapon.range + 1)
                {
                    listOfEnemiesThatCanAtk.Add(enemyUnit);
                }
            }
        }

        return listOfEnemiesThatCanAtk;
    }

    //returns a list of friendly units that can attack a tile at the passed coordinates.
    public List<Unit> GetListOfFriendliesThatCanAtk(int x, int z)
    {
        List<Unit> listOfFriendliesThatCanAtk = new List<Unit>();

        foreach (GameObject friendlyUnitGO in unitHandler.friendlyUnitGOList)
        {
            List<(int, int)> mvmtTilesList = new List<(int, int)>();
            Unit friendlyUnit = friendlyUnitGO.GetComponent<UnitData>().unit;

            //for some reason this does not take into account movement.
            SetUnitMvmtGrid(friendlyUnitGO, friendlyUnit);

            //Debug.Log("Tile x is: " + GetTileX(friendlyUnitGO));
            //Debug.Log("Tile z is: " + GetTileZ(friendlyUnitGO));

            //Debug.Log(unitMovementRangeStartX);
            //Debug.Log(unitMovementRangeStartZ);
            //Debug.Log(unitMovementRangeEndX);
            //Debug.Log(unitMovementRangeEndZ);

            for (int X = unitMovementRangeStartX; X < unitMovementRangeEndX + 1; X++)
            {
                for (int Z = unitMovementRangeStartZ; Z < unitMovementRangeEndZ; Z++)
                {
                    float pathCost = tileMap.GeneratePathTo(X, Z, friendlyUnitGO);

                    if (pathCost - 1 <= friendlyUnit.movementRange)
                    {
                        mvmtTilesList.Add((X, Z));
                    }
                }
            }

            foreach ((int, int) tile in mvmtTilesList)
            {
                float pathCost = tileMap.GenerateMvAtkPathTo(x, z, tile.Item1, tile.Item2, friendlyUnitGO);

                if (pathCost - 1 <= friendlyUnit.unitInventory.equippedWeapon.range)
                {
                    listOfFriendliesThatCanAtk.Add(friendlyUnit);
                    mvmtTilesList.Clear();
                    break;
                }
            }
        }

        return listOfFriendliesThatCanAtk;
    }
}
