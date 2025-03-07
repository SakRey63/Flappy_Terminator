using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;

    private void OnEnable()
    {
        _player.Destroyed += OnDestroyPlayer;
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _player.Destroyed -= OnDestroyPlayer;
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
    }

    private void Awake()
    {
        StopGame();
        
        _endGameScreen.Close();
    }
    
    private void OnDestroyPlayer()
    {
        StopGame();
        
        _endGameScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _player.Reset();
        
        _endGameScreen.Close();
        
        StartGame();
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        
        StartGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        Time.timeScale = 1;
    }
}