using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    //HealthBarHandler Class
    private HealthBarHandler healthBarHandler;

    public List<GameObject> abilityAttackableTilesList = new List<GameObject>();

    void Start()
    {
        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        GameObject healthBars = GameObject.Find("Canvases/HealthBarCanvas/Health Bars");
        healthBarHandler = healthBars.GetComponent<HealthBarHandler>();
    }

    //make graphics better for this, cant see chained attacks.
    public void ZeroDawn(Unit unit, Unit enemy)
    {
        Ability zeroDawn = Resources.Load<Ability>("Ability/Zero Dawn");
        List<int> attackLane = GetAttackLane(unit, enemy);
        int hitCount = 0;

        List<(int, int)> enemiesInLaneCoordsList = new List<(int, int)>();
        List<Unit> targetList = new List<Unit>();
        List<int> targetDamageList = new List<int>();

        //lane type
        if (attackLane[2] == 0) //column
        {
            for (int i = attackLane[1] + 1; i < TileMap.mapSizeZ; i++) //z coord of unit
            {
                enemiesInLaneCoordsList.Add((attackLane[0], i)); //x, z
            }
        }
        
        foreach ((int, int) tile in enemiesInLaneCoordsList)
        {
            Unit target = unitHandler.GetUnitAt(tile.Item1, tile.Item2);
            {
                if (target.name != "None" && target.side == "Enemy")
                {
                    Debug.Log(target);
                    targetList.Add(target);
                }
            }
        }

        foreach (Unit target in targetList)
        {
            if (hitCount == 0)
            {
                float outgoingDamageFloat = ((((2 * unit.lvl) / 5 + 2) * zeroDawn.mt * (unit.attack / target.defense)) / 50) + 2;

                int outgoingDamage = Mathf.RoundToInt(outgoingDamageFloat);
                targetDamageList.Add(outgoingDamage);
            }

            if (hitCount == 1)
            {
                float outgoingDamageFloat = 0.667f * (((((2 * unit.lvl) / 5 + 2) * zeroDawn.mt * (unit.attack / target.defense)) / 50) + 2);
                int outgoingDamage = Mathf.RoundToInt(outgoingDamageFloat);
                targetDamageList.Add(outgoingDamage);
            }

            if (hitCount == 2)
            {
                float outgoingDamageFloat = 0.333f * (((((2 * unit.lvl) / 5 + 2) * zeroDawn.mt * (unit.attack / target.defense)) / 50) + 2);
                int outgoingDamage = Mathf.RoundToInt(outgoingDamageFloat);
                targetDamageList.Add(outgoingDamage);
            }

            if (hitCount > 2)
            {
                float outgoingDamageFloat = 0.10f * (((((2 * unit.lvl) / 5 + 2) * zeroDawn.mt * (unit.attack / target.defense)) / 50) + 2);
                int outgoingDamage = Mathf.RoundToInt(outgoingDamageFloat);
                targetDamageList.Add(outgoingDamage);
            }

            hitCount++;
        }

        if (targetList.Count != targetDamageList.Count)
        {
            Debug.Log("error, not same number of targets as damage in ability: Zero Dawn");
        }

        for (int i = 0; i < targetList.Count; i++)
        {
            targetList[i].hp = targetList[i].hp - targetDamageList[i];
            healthBarHandler.SetEnemyCurrHealth(targetList[i]);
            if (targetList[i].hp <= 0) Debug.Log(targetList[i].name + " has fallen.");
            if (targetList[i].hp <= 0) unitHandler.UnitFallen(targetList[i]);
            //Debug.Log(targetDamageList[i]);
        }
    }

    //make attack lane a class later this list shit is a pain in the ass no cappppp
    public List<int> GetAttackLane(Unit unit, Unit enemy)
    {
        int laneCoordX;
        int laneCoordZ;
        int laneType; //0 is column, 1 = row
        int laneDirection = 5; //0 =  north, 1 = east, 2 = south, 3 = west, 5 = ERROR

        List<int> laneList = new List<int>();

        int unitX = coordsHandler.GetTileX(unitHandler.GetUnitGO(unit));
        int unitZ = coordsHandler.GetTileZ(unitHandler.GetUnitGO(unit));

        int enemyX = coordsHandler.GetTileX(unitHandler.GetUnitGO(enemy));
        int enemyZ = coordsHandler.GetTileZ(unitHandler.GetUnitGO(enemy));

        if (enemyX == unitX)
        {
            laneCoordX = unitX;
            laneCoordZ = unitZ;
            laneType = 0;

            if (enemyZ > unitZ)
            {
                laneDirection = 0;
            }

            if (enemyZ < unitZ)
            {
                laneDirection = 2;
            }

            laneList.Add(laneCoordX);
            laneList.Add(laneCoordZ);
            laneList.Add(laneType);
            laneList.Add(laneDirection);

            return laneList;
        }

        if (enemyZ == unitZ)
        {
            laneCoordX = unitX;
            laneCoordZ = unitZ;
            laneType = 1;

            if (enemyX > unitX)
            {
                laneDirection = 1;
            }

            if (enemyX < unitX)
            {
                laneDirection = 3;
            }

            laneList.Add(laneCoordX);
            laneList.Add(laneCoordZ);
            laneList.Add(laneType);
            laneList.Add(laneDirection);

            return laneList;
        }

        else
        {
            //ERROR
            return null;
        }
    }
}
