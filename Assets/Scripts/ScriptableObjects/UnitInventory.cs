using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Inventory", menuName = "Unit Inventory")]
public class UnitInventory : ScriptableObject
{
    public Weapon equippedWeapon;

    public Weapon secondaryWeapon;

    public List<Weapon > weaponsList;

    public Magic equippedMagic;

    public Magic secondaryMagic;

    public List<Magic> magicList;
}
