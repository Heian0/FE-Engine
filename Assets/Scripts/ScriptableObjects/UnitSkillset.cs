using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Skillset", menuName = "Unit Skillset")]
public class UnitSkillset : ScriptableObject
{
    public new string name;

    public Skill skillOne;

    public Skill skillTwo;

    public List<Skill> skillsList;
}
