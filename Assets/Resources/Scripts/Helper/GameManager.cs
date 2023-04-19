using System;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    [SerializeField] private GameObject GameOverPanel;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance is null) ExceptionHandler.Throw("Game Manager is NULL!");
            return _instance;
        }
    }
    public static GameState state;
    public enum GameState
    {
        Start,
        Game,
        End
    }

    private void Awake()
    {
        _instance = this;
        Time.timeScale = 1;
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
        GameOverPanel.SetActive(true);
    }
    public void OnGameOver()
    {
        ChangeGameState(GameState.End);
        AudioManager.Instance.PlaySoundWithName(Tags.GAME_OVER_SFX);
    }
}
