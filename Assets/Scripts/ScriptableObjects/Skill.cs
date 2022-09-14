using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
    public new string name;

    public int mt;

    public int priority;

    public int skillIndex;

    public int range;

    public bool canAtkAir;
}
