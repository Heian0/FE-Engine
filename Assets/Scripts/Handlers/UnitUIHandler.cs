using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUIHandler : MonoBehaviour
{
    private UnitHandler unitHandler;

    private CoordsHandler coordsHandler;

    private TileMap tileMap;

    public Dictionary<GameObject, GameObject> personalUnitUIDict = new Dictionary<GameObject, GameObject>();

    void Start()
    {
        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();

        //CoordsHandler Class
        GameObject map = GameObject.Find("Map");
        coordsHandler = map.GetComponent<CoordsHandler>();

        //TileMap Class
        tileMap = map.GetComponent<TileMap>();
    }

    void Update()
    {

    }

    public void SetAllUnitMovableUI(GameObject tileGO)
    {
        int i = 0;

        foreach (GameObject unitGO in unitHandler.friendlyUnitGOList)
        {
            GameObject UIgo = GameObject.Instantiate(tileGO, new Vector3(coordsHandler.GetTileX(unitGO), -0.45f, coordsHandler.GetTileZ(unitGO)), Quaternion.identity);

            personalUnitUIDict.Add(unitGO, UIgo);

            i = i + 1;
        }
    }

    public void ResetAllUnitMovableUI(GameObject tileGO)
    {
        foreach (GameObject unitGO in unitHandler.friendlyUnitGOList)
        {
            GameObject UIgo = GameObject.Instantiate(tileGO, new Vector3(coordsHandler.GetTileX(unitGO), -0.45f, coordsHandler.GetTileZ(unitGO)), Quaternion.identity);

            personalUnitUIDict[unitGO] = UIgo;

        }
    }

    public void SetUnitMovableUI(GameObject tileGO, GameObject unitGO)
    {
        GameObject UIgo = GameObject.Instantiate(tileGO, new Vector3(coordsHandler.GetTileX(unitGO), -0.45f, coordsHandler.GetTileZ(unitGO)), Quaternion.identity);

        personalUnitUIDict[unitGO] = UIgo;
    }
    public void ClearAllUnitMovableUI()
    {
        foreach (GameObject unitGO in unitHandler.friendlyUnitGOList)
        {
            GameObject.Destroy(personalUnitUIDict[unitGO]);
        }

        personalUnitUIDict.Clear();
    }

    public void ClearUnitMovableUI(GameObject go)
    {
        GameObject.Destroy(personalUnitUIDict[go]);
    }
}
