using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    public GameObject friendlyHealthBar;
    public GameObject enemyHealthBar;

    public GameObject cassiusHealthBar;
    public GameObject koroHealthBar;
    public GameObject cynthiaHealthBar;

    public GameObject enemyThiefHealthBar;
    public GameObject enemyArcherHealthBar;
    public GameObject enemyMSvntHealthBar;

    public TMPro.TMP_Text friendlyHealthText;
    public TMPro.TMP_Text enemyHealthText;

    void Start()
    {
        SetAllHealthBars();
    }

    public void SetFriendlyMaxHealth(Unit selectedUnit)
    {
        friendlyHealthBar.GetComponent<Slider>().maxValue = selectedUnit.maxHp;
        friendlyHealthBar.GetComponent<Slider>().value = selectedUnit.hp;

        friendlyHealthText.text = selectedUnit.hp.ToString();
    }

    public void SetAllHealthBars()
    {
        cassiusHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Friendly/Cassius").maxHp;
        cassiusHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Friendly/Cassius").hp;

        koroHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Friendly/Koro").maxHp;
        koroHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Friendly/Koro").hp;

        cynthiaHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Friendly/Cynthia").maxHp;
        cynthiaHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Friendly/Cynthia").hp;

        enemyThiefHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Enemy/EnemyThief").maxHp;
        enemyThiefHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Enemy/EnemyThief").hp;

        enemyArcherHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Enemy/EnemyArcher").maxHp;
        enemyArcherHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Enemy/EnemyArcher").hp;

        enemyMSvntHealthBar.GetComponent<Slider>().maxValue = Resources.Load<Unit>("Units/Enemy/EnemyMSvnt").maxHp;
        enemyMSvntHealthBar.GetComponent<Slider>().value = Resources.Load<Unit>("Units/Enemy/EnemyMSvnt").hp;
    }

    public void SetEnemyMaxHealth(Unit selectedEnemy)
    {
        enemyHealthBar.GetComponent<Slider>().maxValue = selectedEnemy.maxHp;
        enemyHealthBar.GetComponent<Slider>().value = selectedEnemy.hp;

        enemyHealthText.text = selectedEnemy.hp.ToString();
    }

    public void SetFriendlyCurrHealth(Unit selectedUnit)
    {
        if (selectedUnit.hp < 0)
        {
            friendlyHealthBar.GetComponent<Slider>().value = 0;
            friendlyHealthText.text = "0";

            return;
        }

        friendlyHealthBar.GetComponent<Slider>().value = selectedUnit.hp;
        friendlyHealthText.text = selectedUnit.hp.ToString();

        switch(selectedUnit.name)
        {
            case "Cassius":

                cassiusHealthBar.GetComponent<Slider>().value = selectedUnit.hp;

                break;

            case "Koro":

                koroHealthBar.GetComponent<Slider>().value = selectedUnit.hp;

                break;

            case "Cynthia":

                cynthiaHealthBar.GetComponent<Slider>().value = selectedUnit.hp;

                break;
        }
    }

    public void SetEnemyCurrHealth(Unit selectedEnemy)
    {
        if (selectedEnemy.hp < 0)
        {
            enemyHealthBar.GetComponent<Slider>().value = 0;
            enemyHealthText.text = "0";

            return;
        }

        enemyHealthBar.GetComponent<Slider>().value = selectedEnemy.hp;
        enemyHealthText.text = selectedEnemy.hp.ToString();

        switch (selectedEnemy.name)
        {
            case "Enemy Thief":

                enemyThiefHealthBar.GetComponent<Slider>().value = selectedEnemy.hp;

                break;

            case "Enemy Archer":

                enemyArcherHealthBar.GetComponent<Slider>().value = selectedEnemy.hp;

                break; 

            case "Enemy Mortal Savant":

                enemyMSvntHealthBar.GetComponent<Slider>().value = selectedEnemy.hp;

                break;
        }
    }
}
