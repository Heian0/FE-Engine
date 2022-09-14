using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Abilityset", menuName = "Unit Abilityset")]
public class UnitAbilityset : ScriptableObject
{
    public new string name;

    public Ability abilityOne;

    public Ability abilityTwo;

    public Ability abilityThree;

    public List<Ability> abilityList;
}
