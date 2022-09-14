using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public new string name;

    public int abilityIndex;

    public int abilityLevel;

    public int range;

    public int mt;

    public bool canAtkAir;

    public int priority;
}
