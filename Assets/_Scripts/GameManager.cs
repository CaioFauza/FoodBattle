using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public int lifes;
    public int points;

    private static GameManager _instance;

    public enum GameState { MENU, GAME, PAUSE, END };

    public GameState gameState { get; private set; }

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    public static GameManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }
        return _instance;
    }

    private GameManager()
    {
        lifes = 5;
        points = 0;
        gameState = GameState.MENU;
    }

    public void ChangeState(GameState nextState)
    {
        if(nextState == GameState.GAME) Reset();
        gameState = nextState;
        changeStateDelegate();
    }

    private void Reset()
    {
        lifes = 5;
        points = 0;
    }

}


