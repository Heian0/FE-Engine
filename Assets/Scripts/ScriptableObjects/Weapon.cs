using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public new string name;

    public int mt;

    public int range;

    public int weaponIndex;

    public int priority;

    public bool doubleAtk;

    public bool canAtkAir;

    //0 is physical, 1 is magic
    public int weaponType;
}
