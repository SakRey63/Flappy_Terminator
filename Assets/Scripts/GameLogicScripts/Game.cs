using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private DisplayCounter _display;

    private Vector2 _startPlayerPosition;

    public event Action FinishedGame;
    
    private void OnEnable()
    {
        _player.Killed += OnDestroyPlayer;
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _player.Killed -= OnDestroyPlayer;
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _endGameScreen.RestartButtonClicked -= OnRestartButtonClick;
    }

    private void Awake()
    {
        _startPlayerPosition = _player.transform.position;
        
        StopGame();
        
        _endGameScreen.Close();
    }
    
    public void ChangeValue()
    {
        _display.ChangeValue();
    }
    
    private void OnDestroyPlayer()
    {
        FinishedGame?.Invoke();
        
        StopGame();
        
        _endGameScreen.Open();
    }

    private void OnRestartButtonClick()
    {
        _player.Reset(_startPlayerPosition);
        
        _display.Reset();
        
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