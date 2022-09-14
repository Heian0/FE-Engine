using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameStateManager gameState);

    public abstract void UpdateState(GameStateManager gameState);
}
