using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private BulletMover _mover;
    [SerializeField] private float _delay = 0.7f;
    
    private bool _isTarget;
    private float _direction;
    private Game _game;

    public bool IsTarget => _isTarget;
    public float Direction => _direction;
    
    public event Action<Bullet> AchievedTarget;

    private void Update()
    {
        _mover.Move(_direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            _isTarget = false;
            
            AchievedTarget?.Invoke(this);
        }
        else if (other.TryGetComponent(out Detector detector))
        {
            if (detector.Direction >= _direction)
            {
                _isTarget = false;
            }
            else
            {
                _isTarget = true;
            }
        }
    }

    public void Reset()
    {
        _direction = 0;

        _isTarget = false;
    }

    public void SetParameters(float direction, Game game)
    {
        _direction = direction;
        
        _game = game;

        _game.FinishedGame += ReturnToPool;
        
        _flipper.CreateDirection(_direction);
    }
    
    public void ReturnToPool()
    {
        _game.FinishedGame -= ReturnToPool;
        
        AchievedTarget?.Invoke(this);
    }
}