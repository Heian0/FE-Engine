using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Magic", menuName = "Magic")]
public class Magic : ScriptableObject
{
    public new string name;

    public int magicIndex;

    public int range;

    public int mt;

    public bool canAtkAir;

    public int priority;
}
