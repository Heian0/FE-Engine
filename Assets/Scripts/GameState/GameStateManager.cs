 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    GameBaseState currState;

    public TurnStart turnStart = new TurnStart();
    public SelectTile selectTile = new SelectTile();
    public UnitSelected unitSelected = new UnitSelected();
    public MoveUnit moveUnit = new MoveUnit();
    public SelectAction selectAction = new SelectAction();
    public Attack attack = new Attack();
    public ViewAttack viewAttack = new ViewAttack();
    public SelectAbility selectAbility = new SelectAbility();
    public ViewAbility viewAbility = new ViewAbility();
    public ViewSkill viewSkill = new ViewSkill();
    public SelectAttackW selectAttackW = new SelectAttackW();
    public SelectAttackS selectAttackS = new SelectAttackS();
    public SelectAttackA selectAttackA = new SelectAttackA();
    public EnemyTurn enemyTurn = new EnemyTurn();
    public EnemyMovement enemyMovement = new EnemyMovement();
    public EnemyAttack enemyAttack = new EnemyAttack();
    public EnemySelectAttack enemySelectAttack = new EnemySelectAttack();

    private bool beingHandled = false;

    void Start()
    {
        //starting state 
        currState = turnStart;
        //"this" is a refernece to the context (this exact MonoBehaviour script)
        currState.EnterState(this);
    }

    void Update()
    {
        currState.UpdateState(this);
    }

    public void SwitchState(GameBaseState gameState)
    {
        currState = gameState;
        gameState.EnterState(this);
    }

    public IEnumerator SwitchStateDelay(GameBaseState gameState, float time)
    {
        beingHandled = true;

        yield return new WaitForSeconds(time);

        currState = gameState;
        gameState.EnterState(this);

        beingHandled = false;
    }
}


