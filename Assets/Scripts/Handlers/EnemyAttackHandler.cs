using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour
{
    public int currentEnemyIndex = 0;
    public List<GameObject> orderedEnemyGOList = new List<GameObject>();

    public List<Unit> listOfUnmovedEnemies = new List<Unit>();

    public bool enemyAttack;

    public int recAtkType;

    public Unit enemyUnit;
    public GameObject enemyUnitGO;
    public Unit target;
    public GameObject targetGO;

    //CoordsHandler Class
    private CoordsHandler coordsHandler;

    //TileMap Class
    private TileMap tileMap;

    //MovementHandler Class
    private MovementHandler movementHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    //AttackHandlerClass
    private AttackHandler attackHandler;

    private List<(int, int)> listOfOptimalTiles = new List<(int, int)>();
    
    void Start()
    {
        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //TileMap Class
        tileMap = map.GetComponent<TileMap>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //MovementHandler Class
        movementHandler = map.GetComponent<MovementHandler>();

        //AttackHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        attackHandler = SelectAttackCanvases.GetComponent<AttackHandler>();

    }

    public Unit GetTarget(Unit enemyUnit)
    {
        Unit currentTarget = Resources.Load<Unit>("Units/None");

        List<Unit> targetList = new List<Unit>();

        int biggestRange = unitHandler.GetUnitBiggestAtkRange(enemyUnit);
        Debug.Log(enemyUnit.name + biggestRange);

        List<(int, int)> mvmtTilesList = new List<(int, int)>();

        coordsHandler.SetUnitMvmtGrid(enemyUnit.unitGameObject, enemyUnit);

        for (int x = coordsHandler.unitMovementRangeStartX; x < coordsHandler.unitMovementRangeEndX + 1; x++)
        {
            for (int z = coordsHandler.unitMovementRangeStartZ; z < coordsHandler.unitMovementRangeEndZ + 1; z++)
            {
                float pathCost = tileMap.GeneratePathTo(x, z, enemyUnit.unitGameObject);

                if (pathCost <= enemyUnit.movementRange + 1)
                {
                    (int, int) tile = (x, z);
                    mvmtTilesList.Add(tile);
                    //Debug.Log(enemyUnit.name);
                    //Debug.Log(tile);
                }
            }
        }

        foreach ((int, int) tile in mvmtTilesList)
        {
            coordsHandler.SetUnitAtkMvmtGrid(tile.Item1, tile.Item2, biggestRange);

            for (int x = coordsHandler.unitAttackRangeStartX; x < coordsHandler.unitAttackRangeEndX + 1; x++)
            {
                for (int z = coordsHandler.unitAttackRangeStartZ; z < coordsHandler.unitAttackRangeEndZ + 1; z++)
                {
                    float pathCost = tileMap.GenerateMvAtkPathTo(x, z, tile.Item1, tile.Item2, enemyUnit.unitGameObject);
                    //Debug.Log(enemyUnit.name + " From: " + tile.Item1 + ", " + tile.Item2 + " To: " + x + ", " + z + " Pathcost is: "  + pathCost);

                    if (pathCost - 1 <= biggestRange
                        && coordsHandler.GetUnitAt(x, z) != null)
                    {
                        Unit target = coordsHandler.GetUnitAt(x, z);

                        if (!targetList.Contains(target) && attackHandler.IsUnitAttackable(enemyUnit, target))
                        {
                            targetList.Add(target);
                            Debug.Log(enemyUnit.name + " " + target.name);
                        }
                    }
                }
            }
        }

        if (enemyUnit.attackType == 0)
        {
            int potentialDamage = -999;

            foreach (Unit target in targetList)
            {
                int attackDamage = (((((2 * enemyUnit.lvl) / 5) + 2) * enemyUnit.unitInventory.equippedWeapon.mt * (enemyUnit.attack / target.defense)) / 50) + 2;
                if (attackDamage > potentialDamage)
                {
                    potentialDamage = attackDamage;

                    currentTarget = target;
                }
            }
        }

        else if (enemyUnit.attackType == 1)
        {
            int potentialDamage = -999;

            foreach (Unit target in targetList)
            {
                float attackDamage = ((((2 * enemyUnit.lvl) / 5 + 2) * enemyUnit.unitInventory.equippedWeapon.mt * (enemyUnit.magic / target.resistance)) / 50) + 2;

                if ((int)attackDamage > potentialDamage)
                {
                    potentialDamage = (int)attackDamage;

                    currentTarget = target;
                }
            }
        }

        else if (enemyUnit.attackType == 2)
        {
            int potentialDamage = -999;

            foreach (Unit target in targetList)
            {
                //Debug.Log(target);
                float attackDamage = (((((2 * enemyUnit.lvl) / 5) + 2) * enemyUnit.unitInventory.equippedMagic.mt * (enemyUnit.attack / target.defense)) / 50) + 2;

                if ((int)attackDamage > potentialDamage)
                {
                    potentialDamage = (int)attackDamage;
                    enemyUnit.recAtkType = 0;
                    currentTarget = target;
                }

                attackDamage = ((((2 * enemyUnit.lvl) / 5 + 2) * enemyUnit.unitInventory.equippedWeapon.mt * (enemyUnit.magic / target.resistance)) / 50) + 2;

                if ((int)attackDamage > potentialDamage)
                {
                    potentialDamage = (int)attackDamage;
                    enemyUnit.recAtkType = 1;
                    currentTarget = target;
                }
            }
        }

        else
        {
            //this only returns if we have a bug aka inputed wrong attack type (not 0 or 1).
            currentTarget = Resources.Load<Unit>("Units/None");
        }

        return currentTarget;
    }

    //create a function that will return the most advantageous position for the enemy unit to attack the target.
    public (int, int) GetOptimalTileForAtk(Unit enemyUnit, Unit target)
    {
        int reciprocalAtks = 99;
        (int, int) optimalTile = (99, 99);

        int biggestRange = unitHandler.GetUnitBiggestAtkRange(enemyUnit);

        List<(int, int)> mvmtTilesList = new List<(int, int)>();

        coordsHandler.SetUnitMvmtGrid(enemyUnit.unitGameObject, enemyUnit);

        for (int x = coordsHandler.unitMovementRangeStartX; x < coordsHandler.unitMovementRangeEndX + 1; x++)
        {
            for (int z = coordsHandler.unitMovementRangeStartZ; z < coordsHandler.unitMovementRangeEndZ + 1; z++)
            {
                float pathCost = tileMap.GeneratePathTo(x, z, enemyUnit.unitGameObject);

                if (pathCost <= enemyUnit.movementRange + 1)
                {
                    (int, int) tile = (x, z);
                    mvmtTilesList.Add(tile);
                }
            }
        }

        foreach ((int, int) tile in mvmtTilesList)
        {
            if (enemyUnit.recAtkType == 0)
            {
                coordsHandler.SetUnitAtkMvmtGrid(tile.Item1, tile.Item2, enemyUnit.unitInventory.equippedWeapon.range);

                for (int x = coordsHandler.unitAttackRangeStartX; x < coordsHandler.unitAttackRangeEndX + 1; x++)
                {
                    for (int z = coordsHandler.unitAttackRangeStartZ; z < coordsHandler.unitAttackRangeEndZ + 1; z++)
                    {
                        float pathCost = tileMap.GenerateMvAtkPathTo(x, z, tile.Item1, tile.Item2, enemyUnit.unitGameObject);
                        //Debug.Log(enemyUnit.name + " From: " + tile.Item1 + ", " + tile.Item2 + " To: " + x + ", " + z + " Pathcost is: "  + pathCost);

                        if (pathCost - 1 <= enemyUnit.unitInventory.equippedWeapon.range)
                        {
                            if (coordsHandler.GetUnitAt(x, z) == target)
                            {
                                (int, int) potentialTile = (tile.Item1, tile.Item2);
                                //Debug.Log(enemyUnit.name + " " + potentialTile);
                                listOfOptimalTiles.Add(potentialTile);
                            }
                        }
                    }
                }
            }

            else
            {
                coordsHandler.SetUnitAtkMvmtGrid(tile.Item1, tile.Item2, enemyUnit.unitInventory.equippedMagic.range);

                for (int x = coordsHandler.unitAttackRangeStartX; x < coordsHandler.unitAttackRangeEndX + 1; x++)
                {
                    for (int z = coordsHandler.unitAttackRangeStartZ; z < coordsHandler.unitAttackRangeEndZ + 1; z++)
                    {
                        float pathCost = tileMap.GenerateMvAtkPathTo(x, z, tile.Item1, tile.Item2, enemyUnit.unitGameObject);
                        //Debug.Log(enemyUnit.name + " From: " + tile.Item1 + ", " + tile.Item2 + " To: " + x + ", " + z + " Pathcost is: "  + pathCost);

                        if (pathCost - 1 <= enemyUnit.unitInventory.equippedMagic.range)
                        {
                            if (coordsHandler.GetUnitAt(x, z) == target)
                            {
                                (int, int) potentialTile = (tile.Item1, tile.Item2);
                                //Debug.Log(enemyUnit.name + " " + potentialTile);
                                listOfOptimalTiles.Add(potentialTile);
                            }
                        }
                    }
                }
            }
        }

        foreach ((int, int) tile in listOfOptimalTiles)
        {
            int reciprocalAtksAt = coordsHandler.GetListOfFriendliesThatCanAtk(tile.Item1, tile.Item2).Count;
            Debug.Log("reciprocal attacks at tile: " + tile + " is: " + reciprocalAtksAt);
            foreach (Unit unit in coordsHandler.GetListOfFriendliesThatCanAtk(tile.Item1, tile.Item2))
            {
                //Debug.Log("tile: " + tile + " can be attacked by: " + unit);
            }

            if (reciprocalAtksAt < reciprocalAtks)
            {
                reciprocalAtks = reciprocalAtksAt;
                optimalTile = (tile.Item1, tile.Item2);
            }

            // below should make the ai choose the tile farthest away even if it is not in range of a unit. hint use pathcost. why am i giving myself a hint lmao
            /*
            if (reciprocalAtksAt == reciprocalAtks)
            {
                optimalTile = (tile.Item1, tile.Item2);
            }
            */
        }

        //IMPORTANT, IF NOT CLEARED WE GET AN INFINITE LOOP BUG
        listOfOptimalTiles.Clear();

        return optimalTile;
    }

    public void MoveEnemyUnitTo(int x, int z, GameObject enemyUnitGO)
    {
        enemyUnitGO.transform.position = new Vector3(x, 0.5f, z);

        /*
        //get path (node list)
        float pathCost = tileMap.GeneratePathTo(x, z, enemyUnitGO);
        List<Node> nodeList = enemyUnitGO.GetComponent<UnitData>().currentPath;

        //for each node in the list, move the unit to that node
        foreach (Node node in nodeList)
        {
            Node nextNode = node;

            if (nodeList.IndexOf(node) + 1 <= nodeList.Count - 1)
            {
                nextNode = nodeList[nodeList.IndexOf(node) + 1];
            }
            
            Vector3 newPos = new Vector3(nextNode.NodeX - node.NodeX, 0, nextNode.NodeZ - node.NodeZ);

            //enemyUnitGO.transform.position = new Vector3(x, 0, z);

            //movementHandler.MoveSelectedUnit(newPos, enemyUnitGO);

            if (nextNode.NodeX > node.NodeX)
            {
                coordsHandler.IncreaseTileX(enemyUnitGO);
            }

            if (nextNode.NodeX < node.NodeX)
            {
                coordsHandler.DecreaseTileX(enemyUnitGO);
            }

            if (nextNode.NodeZ > node.NodeZ)
            {
                coordsHandler.IncreaseTileZ(enemyUnitGO);
            }

            if (nextNode.NodeZ < node.NodeZ)
            {
                coordsHandler.DecreaseTileZ(enemyUnitGO);
            }
        }
        */
    }
}
