using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    //list of GameObjects, selectable tiles, for visuals
    public List<GameObject> stList = new List<GameObject>();

    //List of custom class AccTile which contains coordinates of selectable tiles, for internal calculations.
    public List<AccTile> accessibleTiles = new List<AccTile>();

    //list of GameObjects, attackable tiles, for visuals
    public List<GameObject> atList = new List<GameObject>();

    //List of custom class AccTile which contains coordinates of attackable tiles that are in range, for internal calculations.
    public List<AccTile> attackableTiles = new List<AccTile>();

    //List of custom class AccTile which contains coordinates of attackable tiles that are in range and have an enemy on them, for internal calculations.
    public List<AccTile> attackableTilesWithEnemy = new List<AccTile>();

    public void AddAccessibleTile(int x, int z)
    {
        accessibleTiles.Add(new AccTile { AccTileX = x, AccTileZ = z });
    }

    public void DisplayAndAddSelectedTile(int x, int z, GameObject tileGO)
    {
        GameObject go = GameObject.Instantiate(tileGO, new Vector3(x, -0.45f, z), Quaternion.identity);

        stList.Add(go);
    }

    public void DestroySTs()
    {
        foreach (GameObject st in stList)
        {
            GameObject.Destroy(st);
        }
    }

    public void AddAttackableTile(int x, int z)
    {
        attackableTiles.Add(new AccTile { AccTileX = x, AccTileZ = z });
    }

    public void DisplayAndAddAttackableTile(int x, int z, GameObject tileGO)
    {
        GameObject go = GameObject.Instantiate(tileGO, new Vector3(x, -0.45f, z), Quaternion.identity);
        attackableTiles.Add(new AccTile { AccTileX = x, AccTileZ = z });

        atList.Add(go);
    }

    public void DisplayAttackableTile(int x, int z, GameObject tileGO)
    {
        GameObject go = GameObject.Instantiate(tileGO, new Vector3(x, -0.45f, z), Quaternion.identity);

        atList.Add(go);
    }

    public void DestroyATs()
    {
        foreach (GameObject at in atList)
        {
            GameObject.Destroy(at);
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

    public bool GetAtkTile(int x, int z)
    {
        foreach (AccTile AT in attackableTiles)
        {
            if (AT.AccTileX == x && AT.AccTileZ == z)
            {
                return true;
            }
        }

        return false;
    }

    public void ClearAccessibleTiles()
    {
        accessibleTiles.Clear();
    }

    public void ClearAttackableTiles()
    {
        attackableTiles.Clear();
    }

    public void ClearAttackableTilesWithEnemy()
    {
        attackableTilesWithEnemy.Clear();
    }
}
