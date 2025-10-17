using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    GamePlay,
    MatchEnd,
    Paused
    //Ready,
    //FireDart,
    //ShowingPoint,
}

public class GameManager : Singleton<GameManager>
{
    [Header("DEBUG")]
    [SerializeField] private GameState currentGameState;
    [SerializeField] private GameObject GameplayObject;

    void Start()
    {
        OnInit();
    }

    void OnInit()
    {
        currentGameState = GameState.MainMenu;
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (currentGameState)
        {
            case GameState.MainMenu:
                Debug.Log("Main Menu: Show main menu UI, maybe play a sound, etc.");
                // Show main menu UI
                break;

            case GameState.GamePlay:
                //Tắt UI
                UIManager.Instance.HideAll();

                DartManager.Instance.OnInit();
                CameraManager.Instance.SetCamera(CameraType.gameplay);
                break;

            case GameState.MatchEnd:
                Debug.Log("Game Over: Show game over screen, offer restart.");
                // Show game over UI, handle score, etc.
                break;

            case GameState.Paused:
                Debug.Log("Game Paused: Pause all game activities.");
                // Pause the game (e.g., time scale = 0, show pause menu).
                break;
        }
    }
}
