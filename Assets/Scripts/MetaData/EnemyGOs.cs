using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGOs : MonoBehaviour
{
    public GameObject enemyGO;

    public static List<GameObject> enemyGOList = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in enemyGO.transform)
        {
            enemyGOList.Add(child.gameObject);
        }
    }

    void Update()
    {

    }
}