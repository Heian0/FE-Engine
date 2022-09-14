using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonHandler : MonoBehaviour
{
    public GameObject equippedWeaponButton;
    public GameObject secondaryWeaponButton;

    public TMPro.TMP_Text equippedWeaponButtonText;
    public TMPro.TMP_Text secondaryWeaponButtonText;

    public GameObject skillOneButton;
    public GameObject skillTwoButton;

    public TMPro.TMP_Text skillOneButtonText;
    public TMPro.TMP_Text skillTwoButtonText;

    public GameObject abilityOneButton;
    public GameObject abilityTwoButton;
    public GameObject abilityThreeButton;

    public GameObject ViewAbilityOneButton;
    public GameObject ViewAbilityTwoButton;
    public GameObject ViewAbilityThreeButton;

    public TMPro.TMP_Text abilityOneButtonText;
    public TMPro.TMP_Text abilityTwoButtonText;
    public TMPro.TMP_Text abilityThreeButtonText;

    public TMPro.TMP_Text ViewAbilityOneButtonText;
    public TMPro.TMP_Text ViewAbilityTwoButtonText;
    public TMPro.TMP_Text ViewAbilityThreeButtonText;

    public void SetWeaponButtons(UnitInventory selectedUnitInventory)
    {
        ClearAttackButtons();

        if (selectedUnitInventory.weaponsList.Count == 2)
        {
            equippedWeaponButton.SetActive(true);
            secondaryWeaponButton.SetActive(true);

            equippedWeaponButtonText.text = selectedUnitInventory.equippedWeapon.name + " (1)";
            secondaryWeaponButtonText.text = selectedUnitInventory.secondaryWeapon.name + " (2)";
        }

        else
        {
            equippedWeaponButton.SetActive(true);
            secondaryWeaponButton.SetActive(false);

            equippedWeaponButtonText.text = selectedUnitInventory.equippedWeapon.name + " (1)";
        }
    }

    public void SetSkillButtons(UnitSkillset selectedUnitSkillset)
    {
        ClearAttackButtons();

        if (selectedUnitSkillset.skillsList.Count == 2)
        { 
            skillOneButton.SetActive(true);
            skillTwoButton.SetActive(true);

            skillOneButtonText.text = selectedUnitSkillset.skillOne.name + " (1)";
            skillTwoButtonText.text = selectedUnitSkillset.skillTwo.name + " (2)";
        }

        else
        {
            skillOneButton.SetActive(true);
            skillTwoButton.SetActive(false);

            skillOneButtonText.text = selectedUnitSkillset.skillOne.name + " (1)";
        }
    }

    public void SetAbilityButtons(UnitAbilityset selectedUnitAbilityset)
    {
        ClearAttackButtons();

        if (selectedUnitAbilityset.abilityList.Count == 3)
        {
            abilityOneButton.SetActive(true);
            abilityTwoButton.SetActive(true);
            abilityThreeButton.SetActive(true);

            abilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
            abilityTwoButtonText.text = selectedUnitAbilityset.abilityTwo.name + " (2)";
            abilityThreeButtonText.text = selectedUnitAbilityset.abilityThree.name + " (3)";
        }

        if (selectedUnitAbilityset.abilityList.Count == 2)
        {
            abilityOneButton.SetActive(true);
            abilityTwoButton.SetActive(true);
            abilityThreeButton.SetActive(false);

            abilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
            abilityTwoButtonText.text = selectedUnitAbilityset.abilityTwo.name + " (2)";
        }

        else
        {
            abilityOneButton.SetActive(true);
            abilityTwoButton.SetActive(false);
            abilityThreeButton.SetActive(false);

            abilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
        }
    }

    public void SetViewAbilityButtons(UnitAbilityset selectedUnitAbilityset)
    {
        ClearViewAbilityButtons();

        if (selectedUnitAbilityset.abilityList.Count == 3)
        {
            ViewAbilityOneButton.SetActive(true);
            ViewAbilityTwoButton.SetActive(true);
            ViewAbilityThreeButton.SetActive(true);

            ViewAbilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
            ViewAbilityTwoButtonText.text = selectedUnitAbilityset.abilityTwo.name + " (2)";
            ViewAbilityThreeButtonText.text = selectedUnitAbilityset.abilityThree.name + " (3)";
        }

        if (selectedUnitAbilityset.abilityList.Count == 2)
        {
            ViewAbilityOneButton.SetActive(true);
            ViewAbilityTwoButton.SetActive(true);

            ViewAbilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
            ViewAbilityTwoButtonText.text = selectedUnitAbilityset.abilityTwo.name + " (2)";
        }

        else
        {
            ViewAbilityOneButton.SetActive(true);;

            ViewAbilityOneButtonText.text = selectedUnitAbilityset.abilityOne.name + " (1)";
        }
    }

    public void ClearAttackButtons()
    {
        equippedWeaponButton.SetActive(false);
        secondaryWeaponButton.SetActive(false);

        skillOneButton.SetActive(false);
        skillTwoButton.SetActive(false);

        abilityOneButton.SetActive(false);
        abilityTwoButton.SetActive(false);
        abilityThreeButton.SetActive(false);

        ViewAbilityOneButton.SetActive(false);
        ViewAbilityTwoButton.SetActive(false);
        ViewAbilityThreeButton.SetActive(false);
    }

    public void ClearViewAbilityButtons()
    {
        ViewAbilityOneButton.SetActive(false);
        ViewAbilityTwoButton.SetActive(false);
        ViewAbilityThreeButton.SetActive(false);
    }
}
