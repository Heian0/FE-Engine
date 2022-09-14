using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    SelectTile,
    UnitSelected,
    MoveUnit,
    SelectAction,
    Attack
}

public class StateController : MonoBehaviour
{
    public static GameState CurrentState;

    void Start()
    {
        CurrentState = GameState.SelectTile;
    }

    void Update()
    {
        /*
        switch (CurrentState)
        {
            
            case GameState.MoveUnit:
                
                if (Input.GetKeyDown(KeyCode.S))
                {
                    CurrentState = GameState.SelectTile;
                    Debug.Log("Game State is Select Tile");

                    CursorMovement.unitSelected = "None";

                    return;
                }
                        
                break;
            

            case GameState.SelectAction:

                if (Input.GetKeyDown(KeyCode.S))
                {
                    CurrentState = GameState.MoveUnit;
                    Debug.Log("Game State is Move Unit");

                    return;
                }

                break;
        }
        */
    }
}
