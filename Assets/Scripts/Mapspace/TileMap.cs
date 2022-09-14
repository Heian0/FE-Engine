using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    //selectedUnit is currently hardcoded in as the only unit we have, however later once more units are added code will need to be edited so that the GameObject selectedUnit changes when a different unit is selected.
    public GameObject selectedUnit;

    List<Node> currentPath = null;

    public GameObject blueTile;

    public GameObject yellowTile;

    public GameObject grasslandTile;

    public TileType[] tileTypes;

    //tiles that include unit occupancy, used in most calculations
    public static int[,] tiles;

    //tiles that only include terrain types, used to change tiles[x,z] = 3 of a units previous location to the proper terrain it should be.
    public static int[,] terrainTiles;

    Node[,] graph;

    public static int mapSizeX = 10;
    public static int mapSizeZ = 10;

    void Start()
    {
        //setup selected unit's variables, may need to be moved down to update() later.
        //selectedUnit.GetComponent<UnitData>().tileX = (int)selectedUnit.transform.position.x;
        //selectedUnit.GetComponent<UnitData>().tileZ = (int)selectedUnit.transform.position.z;
        //selectedUnit.GetComponent<UnitData>().map = this;
        //selectedUnit.GetComponent<UnitMovement>().map = this;

        //spawns map data
        GenerateMapData();

        //spawns graph of map data
        GeneratePathfindingGraph();

        //spawning visual prefabs
        GenerateMapVisuals();
    }

    void Update()
    {
        //selectedUnit.GetComponent<UnitData>().tileX = (int)selectedUnit.transform.position.x;
        //selectedUnit.GetComponent<UnitData>().tileZ = (int)selectedUnit.transform.position.z;
    }

    void GenerateMapData()
    {
        //allocates our map tiles, initializes
        tiles = new int[mapSizeX, mapSizeZ];
        terrainTiles = new int[mapSizeX, mapSizeZ];

        //initializes all our map tiles to be grassland
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                tiles[x, z] = 0;
            }
        }

        //initializes all our terrain tiles to be grassland at start
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                terrainTiles[x, z] = 0;
            }
        }

        //creates a set of unpassable ruin walls
        tiles[1, 3] = 2;
        tiles[2, 3] = 2;
        tiles[3, 2] = 2;
        tiles[3, 3] = 2;
        tiles[4, 2] = 2;
        tiles[5, 2] = 2;

        //creates forest tiles
        tiles[1, 8] = 1;
        tiles[1, 7] = 1;
        tiles[2, 4] = 1;
        tiles[3, 5] = 1;
        tiles[3, 6] = 1;
        tiles[4, 7] = 1;
        tiles[7, 6] = 1;
        tiles[7, 5] = 1;
        tiles[6, 4] = 1;
        tiles[8, 8] = 1;
        tiles[8, 5] = 1;

        //creates a list of the map tiles that does not include occupied tiles, only terrain tile types.
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                terrainTiles[x, z] = tiles[x, z];
            }
        }

        //where our units start
        tiles[0, 0] = 3;
        tiles[1, 0] = 3;
        tiles[2, 0] = 3;

        //where enemy starts
        tiles[0, 3] = 3;
        tiles[3, 5] = 3;
        tiles[8, 4] = 3;
    }

    void GeneratePathfindingGraph()
    {
        //initialize the array.
        graph = new Node[mapSizeX, mapSizeZ];

        //initialize a Node for each spot in the array.
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                graph[x, z] = new Node();

                graph[x, z].NodeX = x;
                graph[x, z].NodeZ = z;
                graph[x, z].NodeY = 0;
            }
        }

        //now that all the Nodes exist, calculate their naighbours.
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                //we have a 4-way connected square grid map

                if (x > 0)
                {
                    graph[x, z].neighbours.Add(graph[x - 1, z]);
                }

                //mapSizeX - 1 = 9  
                if (x < mapSizeX - 1)
                {
                    graph[x, z].neighbours.Add(graph[x + 1, z]);
                }

                if (z > 0)
                {
                    graph[x, z].neighbours.Add(graph[x, z - 1]);
                }

                //mapSizeZ - 1 = 9  
                if (z < mapSizeZ - 1)
                {
                    graph[x, z].neighbours.Add(graph[x, z + 1]);
                }
            }
        }
    }

    float CostToEnterTile(int x, int z, Unit unit)
    {
        TileType tt = tileTypes[tiles[x, z]];

        if (unit.airUnit)
        {
            return tt.aerialMvmtCost;
        }

        return tt.movementCost;
    }

    public TileType GetTileType(int x, int z)
    {
        TileType tt = tileTypes[TileMap.tiles[x, z]];

        return tt;
    }

    public TileType GetTileTerrainType(int x, int z)
    {
        TileType tt = tileTypes[TileMap.terrainTiles[x, z]];

        return tt;
    }

    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                    TileType tt = tileTypes[terrainTiles[x, z]];

                    GameObject tileGO = GameObject.Instantiate(tt.tileVisualPrefab, new Vector3(x, -0.5f, z), Quaternion.identity);

                    SelectableTile st = tileGO.GetComponent<SelectableTile>();

                    st.tileX = x;
                    st.tileZ = z;
                    st.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

    public float GeneratePathTo(int x, int z, GameObject selectedUnitGO)
    {
        //clears out the selected unit's old path.
        selectedUnitGO.GetComponent<UnitData>().currentPath = null;

        float pathLength = 0;

        if (x == selectedUnitGO.GetComponent<UnitData>().tileX
            && z == selectedUnitGO.GetComponent<UnitData>().tileZ)
        {
            return pathLength;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //setups the list of unvisited Nodes, Nodes we have not yet checked.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnitGO.GetComponent<UnitData>().tileX,
                            selectedUnitGO.GetComponent<UnitData>().tileZ
                            ];

        Node target = graph[x, z];

        dist[source] = 0;
        prev[source] = null;

        //initializes every Node to have infinite distance cause we don't know any distances from the source Node right now. 
        //It's also possible that some Nodes can never be reached from the source so it makes sense that the Nodes distance are set to infinity.
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            //u is going to be the unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //pathLength = pathLength + CostToEnterTile(v.NodeX, v.NodeZ);
                float alt = dist[u] + CostToEnterTile(v.NodeX, v.NodeZ, selectedUnitGO.GetComponent<UnitData>().unit);

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;  
                }
            }
        }

        //if we get here, either we have found the shortest route to the target or there is no route at all to our target.

        if (prev[target] == null)
        {
            //if there is no possible route:
            return 999;
        }

        List<Node> currentPath = new List<Node>();

        Node current = target;

        //steps through the "prev" chain and adds it to our path.
        while (current != null)
        {
            pathLength = pathLength + CostToEnterTile(current.NodeX, current.NodeZ, selectedUnitGO.GetComponent<UnitData>().unit);
            currentPath.Add(current);
            current = prev[current];
        }

        //right now currentPath describes a route from our target to our source, so we need to invert it.
        currentPath.Reverse();
        selectedUnitGO.GetComponent<UnitData>().currentPath = currentPath;

        //it says if the 1st tile in the path is occuppied (tiles[x,z] == 3) than - 98 from pathLength (bec 99 gives 1 more movement space than it should), as if the 1st in the path is occupied tiles[x,z] == 3
        //then it is not another unit, its the unit we're trying to move.
        if (tiles[currentPath[0].NodeX, currentPath[0].NodeZ] == 3)
        {
            pathLength = pathLength - 98;
        }

        //Debug.Log(pathLength);
        return pathLength;
    }

    public float GenerateAttackPathTo(int x, int z, GameObject selectedUnit)
    {
        //clears out the selected unit's old path.
        selectedUnit.GetComponent<UnitData>().currentPath = null;

        float pathLength = 0;

        if (x == selectedUnit.GetComponent<UnitData>().tileX
            && z == selectedUnit.GetComponent<UnitData>().tileZ)
        {
            return pathLength;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //setups the list of unvisited Nodes, Nodes we have not yet checked.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<UnitData>().tileX,
                            selectedUnit.GetComponent<UnitData>().tileZ
                            ];

        Node target = graph[x, z];

        dist[source] = 0;
        prev[source] = null;

        //initializes every Node to have infinite distance cause we don't know any distances from the source Node right now. 
        //It's also possible that some Nodes can never be reached from the source so it makes sense that the Nodes distance are set to infinity.
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            //u is going to be the unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //pathLength = pathLength + CostToEnterTile(v.NodeX, v.NodeZ);
                float alt = dist[u] + 1;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        //if we get here, either we have found the shortest route to the target or there is no route at all to our target.

        if (prev[target] == null)
        {
            //if there is no possible route:
            return 999;
        }

        List<Node> currentPath = new List<Node>();

        Node current = target;

        //steps through the "prev" chain and adds it to our path.
        while (current != null)
        {
            pathLength = pathLength + 1;
            currentPath.Add(current);
            current = prev[current];
        }

        //right now currentPath describes a route from our target to our source, so we need to invert it.
        currentPath.Reverse();
        selectedUnit.GetComponent<UnitData>().currentPath = currentPath;

        //Debug.Log(pathLength);
        return pathLength;
    }

    public float GenerateMvAtkPathTo(int x, int z, int stX, int stZ, GameObject selectedUnit)
    {
        //clears out the selected unit's old path.
        selectedUnit.GetComponent<UnitData>().currentPath = null;

        float pathLength = 0;

        if (x == stX
            && z == stZ)
        {
            return pathLength;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //setups the list of unvisited Nodes, Nodes we have not yet checked.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            stX,
                            stZ
                            ];

        Node target = graph[x, z];

        dist[source] = 0;
        prev[source] = null;

        //initializes every Node to have infinite distance cause we don't know any distances from the source Node right now. 
        //It's also possible that some Nodes can never be reached from the source so it makes sense that the Nodes distance are set to infinity.
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            //u is going to be the unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //pathLength = pathLength + CostToEnterTile(v.NodeX, v.NodeZ);
                float alt = dist[u] + 1;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        //if we get here, either we have found the shortest route to the target or there is no route at all to our target.

        if (prev[target] == null)
        {
            //if there is no possible route:
            return 999;
        }

        List<Node> currentPath = new List<Node>();

        Node current = target;

        //steps through the "prev" chain and adds it to our path.
        while (current != null)
        {
            pathLength = pathLength + 1;
            currentPath.Add(current);
            current = prev[current];
        }

        //right now currentPath describes a route from our target to our source, so we need to invert it.
        currentPath.Reverse();
        selectedUnit.GetComponent<UnitData>().currentPath = currentPath;

        //Debug.Log(pathLength);
        return pathLength;
    }
}
