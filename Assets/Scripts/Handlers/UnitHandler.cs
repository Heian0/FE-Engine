using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{
    public GameObject cassiusGO;
    public GameObject koroGO;
    public GameObject cynthiaGO;

    public GameObject enemyThiefGO;
    public GameObject enemyArcherGO;
    public GameObject enemyMSvntGO;

    public GameObject selectedUnitGO;
    public GameObject selectedEnemyUnitGO;

    public Unit selectedUnit;
    public Unit selectedEnemyUnit;

    public Unit unitAt;

    public GameObject friendly;
    public GameObject enemy;

    public List<GameObject> friendlyUnitGOList;
    public List<GameObject> enemyUnitGOList;

    public List<Unit> friendlyUnitList = new List<Unit>();
    public List<Unit> enemyUnitList = new List<Unit>();

    public List<Unit> unmovedUnitsList = new List<Unit>();
    public List<Unit> movedUnitsList = new List<Unit>();

    public UnitInventory selectedUnitInventory;
    public UnitInventory selectedEnemyUnitInventory;

    public UnitSkillset selectedUnitSkillset;
    public UnitSkillset selectedEnemyUnitSkillset;

    public UnitAbilityset selectedUnitAbilityset;
    public UnitAbilityset selectedEnemyUnitAbilityset;

    //CanvasHandler Class
    private CanvasHandler canvasHandler;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    void Start()
    {
        //CanvasHandler Class
        GameObject actionCanvas = GameObject.Find("Canvases");
        canvasHandler = actionCanvas.GetComponent<CanvasHandler>();

        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //puts all friendly Units in a list
        Unit[] unmovedUnitsArray = Resources.LoadAll<Unit>("Units/Friendly");
        unmovedUnitsList = new List<Unit>(unmovedUnitsArray);

        //puts all friendly units in a list
        foreach (Transform friendlyUnit in friendly.transform)
        {
            friendlyUnitGOList.Add(friendlyUnit.gameObject);
            //Debug.Log(friendlyUnit.gameObject);
        }

        //puts all enemy units in a list, ordered
        foreach (Transform enemyUnit in enemy.transform)
        {
            enemyUnitGOList.Add(enemyUnit.gameObject);
            //Debug.Log(enemyUnit.gameObject);
        }

        friendlyUnitList.Add(Resources.Load<Unit>("Units/Friendly/Cassius"));
        friendlyUnitList.Add(Resources.Load<Unit>("Units/Friendly/Koro"));
        friendlyUnitList.Add(Resources.Load<Unit>("Units/Friendly/Cynthia"));

        enemyUnitList.Add(Resources.Load<Unit>("Units/Enemy/EnemyThief"));
        enemyUnitList.Add(Resources.Load<Unit>("Units/Enemy/EnemyArcher"));
        enemyUnitList.Add(Resources.Load<Unit>("Units/Enemy/EnemyMSvnt"));

        //Legend: 0 = Cassius, 1 = Koro, 2 = Cynthia

        /*
        foreach (GameObject go in friendlyUnitGOList)
        {
            Debug.Log(go);
        }

        foreach (GameObject go in enemyUnitGOList)
        {
            Debug.Log(go);
        }
        */
    }

    public void SetSelectedUnit(int i, string unitCode)
    {
        selectedUnitGO = friendlyUnitGOList[i];
        selectedUnit = Resources.Load<Unit>(unitCode);
    }

    public void SetSelectedEnemy(int i, string unitCode)
    {
        selectedEnemyUnitGO = enemyUnitGOList[i];
        selectedEnemyUnit = Resources.Load<Unit>(unitCode);
    }

    public Unit GetUnitAt(int x, int z)
    {
        //GameObject units = GameObject.Find("Units/Friendly");

        foreach (GameObject unit in friendlyUnitGOList)
        {
            if (
                unit.GetComponent<UnitData>().tileX == x
                &&
                unit.GetComponent<UnitData>().tileZ == z
                )
            {
                unitAt = Resources.Load<Unit>("Units/Friendly/" + unit.gameObject.name);
                return unitAt;
            }
        }

        //units = GameObject.Find("Units/Enemy");

        foreach (GameObject unit in enemyUnitGOList)
        {
            if (
                unit.GetComponent<UnitData>().tileX == x
                &&
                unit.GetComponent<UnitData>().tileZ == z
                )
            {
                unitAt = Resources.Load<Unit>("Units/Enemy/" + unit.gameObject.name);
                return unitAt;
            }
        }

        unitAt = Resources.Load<Unit>("Units/None");
        return unitAt;
    }

    public void SetSelectedUnitInventory(string unitInventoryCode)
    {
        selectedUnitInventory = Resources.Load<UnitInventory>(unitInventoryCode);
    }

    public void SetSelectedEnemyUnitInventory(string unitInventoryCode)
    {
        selectedEnemyUnitInventory = Resources.Load<UnitInventory>(unitInventoryCode);
    }

    public void SetSelectedUnitSkillset(string unitSkillsetCode)
    {
        selectedUnitSkillset = Resources.Load<UnitSkillset>(unitSkillsetCode);
    }

    public void SetSelectedEnemyUnitSkillset(string unitSkillsetCode)
    {
        selectedEnemyUnitSkillset = Resources.Load<UnitSkillset>(unitSkillsetCode);
    }

    public void SetSelectedUnitAbilityset(string unitAbilitysetCode)
    {
        selectedUnitAbilityset = Resources.Load<UnitAbilityset>(unitAbilitysetCode);
    }

    public void SetSelectedEnemyUnitAbilityset(string unitAbilitysetCode)
    {
        selectedEnemyUnitAbilityset = Resources.Load<UnitAbilityset>(unitAbilitysetCode);
    }

    public void ResetMovedUnits()
    {
        movedUnitsList.Clear();
    }

    public void ResetUnmovedUnits()
    {
        unmovedUnitsList.Clear();

        Unit[] unmovedUnitsArray = Resources.LoadAll<Unit>("Units/Friendly");
        unmovedUnitsList = new List<Unit>(unmovedUnitsArray);
    }

    public GameObject GetUnitGO(Unit unit)
    {
        switch (unit.gameObjectIndex)
        {
            case 0:

                return cassiusGO;


            case 1:

                return koroGO;

            case 2:

                return cynthiaGO;

            case 3:

                return enemyThiefGO;

            case 4:

                return enemyArcherGO;

            case 5:

                return enemyMSvntGO;
        }

        return null;
    }

    public void UnitFallen(Unit unit)
    {
        if (unit.side == "Enemy")
        {
            GameObject go = GetUnitGO(unit);
            coordsHandler.ResetOrigTileTerrain(coordsHandler.GetTileX(go), coordsHandler.GetTileZ(go));
            enemyUnitList.Remove(unit);
            enemyUnitGOList.Remove(go);
            canvasHandler.RemoveUICanvasFromList(unit);
            GameObject.Destroy(go);
        }

        if (unit.side == "Friendly")
        {
            GameObject go = GetUnitGO(unit);
            coordsHandler.ResetOrigTileTerrain(coordsHandler.GetTileX(go), coordsHandler.GetTileZ(go));
            friendlyUnitList.Remove(unit);
            friendlyUnitGOList.Remove(go);
            canvasHandler.RemoveUICanvasFromList(unit);
            GameObject.Destroy(go);
        }
    }

    public int GetUnitBiggestAtkRange(Unit unit)
    {
        int biggestRange = 0;

        foreach(Weapon weapon in unit.unitInventory.weaponsList)
        {
            if (weapon.range > biggestRange)
                biggestRange = weapon.range;
        }

        foreach (Magic magic in unit.unitInventory.magicList)
        {
            if (magic.range > biggestRange)
                biggestRange = magic.range;
        }

        return biggestRange;
    }
}
