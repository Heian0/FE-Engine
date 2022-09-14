using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyGOs : MonoBehaviour
{
    public GameObject friendlyGO;

    public static List<GameObject> friendlyGOList = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in friendlyGO.transform)
        {
            friendlyGOList.Add(child.gameObject);
        }
    }

    void Update()
    {
        
    }
}
