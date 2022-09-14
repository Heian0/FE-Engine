using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public new string name;

    public GameObject unitGameObject;

    public int gameObjectIndex;

    public Unit target;

    public (int, int) optimalTile;

    public UnitInventory unitInventory;

    public UnitSkillset unitSkillset;

    public UnitAbilityset unitAbilityset;

    public int recAtkType;

    public string side;

    public int AIType; //0 == passive, 1 == agressive, 2 == supportive, 3 == escape

    public string unitClass;

    public string unitType;

    public bool airUnit;

    public bool canAtkAir;

    public int attackType; //0 == physical, 1 == special

    public int lvl;

    public int maxHp;

    public int hp;

    public int baseAttack;

    public int attack;

    public int baseDefense;

    public int defense;

    public int baseMagic;

    public int magic;

    public int baseResistance;

    public int resistance;

    public int baseSpeed;

    public int speed;

    public int baseMovementRange;

    public int movementRange;

    public int attackRange;
}
