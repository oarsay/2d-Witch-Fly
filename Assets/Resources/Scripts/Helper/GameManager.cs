using System;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameState state;
    public enum GameState
    {
        Start,
        Game,
        End
    }

    public void ChangeGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.Start:
                break;
            case GameState.Game:
                HandleGame();
                break;
            case GameState.End:
                HandleEnd();
                break;
        }
    }
    private void HandleGame()
    {
        throw new NotImplementedException();
    }
    private void HandleEnd()
    {
        Time.timeScale = 0;
    }
    public void OnGameOver()
    {
        ChangeGameState(GameState.End);
    }
}
