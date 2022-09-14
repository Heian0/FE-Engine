using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackHandler : MonoBehaviour
{
    //AbilityHandler Class
    public AbilityHandler abilityHandler;

    //SkillHandler Class
    public SkillHandler skillHandler;

    //UnitHandler Class
    private UnitHandler unitHandler;

    void Start()
    {
        //AbilityHandler Class
        GameObject SelectAttackCanvases = GameObject.Find("SelectAttackCanvases");
        abilityHandler = SelectAttackCanvases.GetComponent<AbilityHandler>();

        //SkillHandler Class
        skillHandler = SelectAttackCanvases.GetComponent<SkillHandler>();

        //UnitHandler Class
        GameObject units = GameObject.Find("Units");
        unitHandler = units.GetComponent<UnitHandler>();
    }

    public void AttackW(Unit selectedUnit, Unit selectedEnemyUnit, Weapon unitWeapon, Weapon enemyWeapon, HealthBarHandler healthBarHandler, bool enemyCanAttack)
    {
        int outgoingDamage;
        int incomingDamage;
        bool unitMovesFirst = UnitMovesFirst(selectedUnit, selectedEnemyUnit, unitWeapon.priority, enemyWeapon.priority);

        switch (unitWeapon.weaponIndex)
        {
            case 0: //Damascus

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitWeapon.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackW(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, unitMovesFirst);

                break;

            case 1: //Horizon

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitWeapon.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackW(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, unitMovesFirst);

                break;

            case 2: //Silver Bow

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitWeapon.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackW(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, unitMovesFirst);

                break;

            case 3: //Silver Sword

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitWeapon.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackW(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, unitMovesFirst);

                break;

            case 4: //Hurricane

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitWeapon.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackW(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, unitMovesFirst);

                break;
        }
    }

    public void AttackS(Unit selectedUnit, Unit selectedEnemyUnit, Skill unitSkill, Weapon enemyWeapon, HealthBarHandler healthBarHandler, bool enemyCanAttack)
    {
        int outgoingDamage;
        int incomingDamage;
        bool unitMovesFirst = UnitMovesFirst(selectedUnit, selectedEnemyUnit, unitSkill.priority, enemyWeapon.priority);


        switch (unitSkill.skillIndex)
        {
            case 0: //ExtremeSpeed // (not related but ability running start, when the unit moves 1st, gain a 1.25x speed boost and 1.25 attack boost for next turn, could be used in tandem with ExtremeSpeed.)

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitSkill.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackS(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, enemyWeapon, unitSkill, unitMovesFirst);

                break;

            case 1: //Inner Focus

                incomingDamage = 0;
                outgoingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                selectedUnit.attack = 2 * selectedUnit.attack;

                Debug.Log(selectedUnit.name + "'s attack sharply rose!");

                InitializeAttackS(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, enemyWeapon, unitSkill, unitMovesFirst);

                break;

            case 2: //QuickDraw

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitSkill.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackS(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, enemyWeapon, unitSkill, unitMovesFirst);

                break;

            case 3: //Skydive

                outgoingDamage = ((((2 * selectedUnit.lvl) / 5 + 2) * unitSkill.mt * (selectedUnit.attack / selectedEnemyUnit.defense)) / 50) + 2;
                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;
                }

                InitializeAttackS(selectedUnit, selectedEnemyUnit, incomingDamage, outgoingDamage, healthBarHandler, enemyWeapon, unitSkill, unitMovesFirst);

                break;
        }
    }

    public void AttackA(Unit selectedUnit, Unit selectedEnemyUnit, Ability unitAbility, Weapon enemyWeapon, HealthBarHandler healthBarHandler, bool enemyCanAttack)
    {
        int outgoingDamage;
        int incomingDamage;
        bool unitMovesFirst = UnitMovesFirst(selectedUnit, selectedEnemyUnit, unitAbility.priority, enemyWeapon.priority);

        switch (unitAbility.abilityIndex)
        {
            case 0: //Dauntless Aura

                if (unitAbility.abilityLevel == 0)
                {
                    selectedUnit.attack = Mathf.RoundToInt(selectedUnit.attack * 1.25f + 0.01f);
                    selectedUnit.defense = Mathf.RoundToInt(selectedUnit.defense * 1.25f + 0.01f);
                    selectedUnit.magic = Mathf.RoundToInt(selectedUnit.magic * 1.25f + 0.01f);
                    selectedUnit.resistance = Mathf.RoundToInt(selectedUnit.resistance * 1.25f + 0.01f);
                    selectedUnit.speed = Mathf.RoundToInt(selectedUnit.speed * 1.25f + 0.01f);
                }

                if (unitAbility.abilityLevel == 1)
                {
                    selectedUnit.attack = Mathf.RoundToInt(selectedUnit.attack * 1.5f + 0.01f);
                    selectedUnit.defense = Mathf.RoundToInt(selectedUnit.defense * 1.5f + 0.01f);
                    selectedUnit.magic = Mathf.RoundToInt(selectedUnit.magic * 1.5f + 0.01f);
                    selectedUnit.resistance = Mathf.RoundToInt(selectedUnit.resistance * 1.5f + 0.01f);
                    selectedUnit.speed = Mathf.RoundToInt(selectedUnit.speed * 1.5f + 0.01f);
                }

                Debug.Log("Dauntless Aura active.");

                break;

            case 1: //Zero Dawn

                incomingDamage = 0;

                if (enemyCanAttack)
                {
                    incomingDamage = ((((2 * selectedEnemyUnit.lvl) / 5 + 2) * enemyWeapon.mt * (selectedEnemyUnit.attack / selectedUnit.defense)) / 50) + 2;

                    InitializeAttackA(selectedUnit, selectedEnemyUnit, incomingDamage, healthBarHandler, unitAbility, unitMovesFirst);

                }

                else
                {
                    abilityHandler.ZeroDawn(selectedUnit, selectedEnemyUnit);
                }

                break;
        }
    }

    private void InitializeAttackW(Unit selectedUnit, Unit selectedEnemyUnit, int incomingDamage, int outgoingDamage, HealthBarHandler healthBarHandler, bool unitMovesFirst)
    {
        if (unitMovesFirst)
        {
            selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
            healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
            if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
            selectedUnit.hp = selectedUnit.hp - incomingDamage;
            healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
            if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
        }

        else
        {
            selectedUnit.hp = selectedUnit.hp - incomingDamage;
            healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
            if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
            selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
            healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
            if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
        }
    }
    private void InitializeAttackS(Unit selectedUnit, Unit selectedEnemyUnit, int incomingDamage, int outgoingDamage, HealthBarHandler healthBarHandler, Weapon enemyWeapon, Skill skill, bool unitMovesFirst)
    {
        switch (skill.skillIndex)
        {
            case 0: case 1: case 2:

                if(unitMovesFirst)
                {
                    selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
                    healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
                    if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
                    selectedUnit.hp = selectedUnit.hp - incomingDamage;
                    healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
                    if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
                }

                else
                {
                    selectedUnit.hp = selectedUnit.hp - incomingDamage;
                    healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
                    if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
                    selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
                    healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
                    if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
                }

                break;
               
            case 3: //Skydive
                
                if (unitMovesFirst)
                {
                    selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
                    healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
                    if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
                    skillHandler.SkyDive();
                }

                else
                {
                    selectedUnit.hp = selectedUnit.hp - incomingDamage;
                    healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
                    if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
                    selectedEnemyUnit.hp = selectedEnemyUnit.hp - outgoingDamage;
                    healthBarHandler.SetEnemyCurrHealth(selectedEnemyUnit);
                    if (selectedEnemyUnit.hp <= 0) unitHandler.UnitFallen(selectedEnemyUnit);
                    skillHandler.SkyDive();
                }

                break;
        }
    }

    private void InitializeAttackA(Unit selectedUnit, Unit selectedEnemyUnit, int incomingDamage, HealthBarHandler healthBarHandler, Ability ability, bool unitMovesFirst)
    {
        switch(ability.abilityIndex)
        {
            case 1: //Zero Dawn

                if (unitMovesFirst)
                {
                    abilityHandler.ZeroDawn(selectedUnit, selectedEnemyUnit);
                    selectedUnit.hp = selectedUnit.hp - incomingDamage;
                    healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
                    if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
                }

                else
                {
                    selectedUnit.hp = selectedUnit.hp - incomingDamage;
                    healthBarHandler.SetFriendlyCurrHealth(selectedUnit);
                    if (selectedUnit.hp <= 0) unitHandler.UnitFallen(selectedUnit);
                    abilityHandler.ZeroDawn(selectedUnit, selectedEnemyUnit);
                }

                break;

        }
    }

    public bool IsUnitAttackable(Unit unit, Unit enemy)
    {
        if (enemy.name == "None")
        {
            return false;
        }

        if (enemy.airUnit)
        {
            if (unit.canAtkAir)
            {
                return true;
            }

            foreach (Weapon weapon in unit.unitInventory.weaponsList)
            {
                if (weapon.canAtkAir)
                {
                    return true;
                }
            }

            foreach (Skill skill in unit.unitSkillset.skillsList)
            {
                if (skill.canAtkAir)
                {
                    return true;
                }
            }

            return false;
        }

        return true;
    }

    public bool UnitMovesFirst(Unit unit, Unit enemy, int unitPriority, int enemyPriority)
    {
        if (unitPriority < enemyPriority) return true;
        if (unitPriority > enemyPriority) return false;
        
        else
        {
            if (unit.speed > enemy.speed) return true;
            if (unit.speed < enemy.speed) return false;
            else if (unit.speed == enemy.speed)
            {
                int randInt = Random.Range(0, 2);
                if (randInt == 0) return true;
                else return false;
            }
        }

        Debug.Log("ERROR");
        return true;
    }
}
