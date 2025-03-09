using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private BulletMover _mover;
    [SerializeField] private float _delay = 0.7f;
    
    private Game _game;

    public bool IsTarget{ get; private set; }
    public float Direction { get; private set; }
    
    public event Action<Bullet> AchievedTarget;

    private void Update()
    {
        _mover.Move(Direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            IsTarget = false;
            
            AchievedTarget?.Invoke(this);
        }
        else if (other.TryGetComponent(out Detector detector))
        {
            if (detector.Direction >= Direction)
            {
                IsTarget = false;
            }
            else
            {
                IsTarget = true;
            }
        }
    }

    public void Reset()
    {
        Direction = 0;

        IsTarget = false;
    }

    public void SetParameters(float direction, Game game)
    {
        Direction = direction;
        
        _game = game;

        _game.FinishedGame += ReturnToPool;
        
        _flipper.CreateDirection(Direction);
    }
    
    public void ReturnToPool()
    {
        _game.FinishedGame -= ReturnToPool;
        
        AchievedTarget?.Invoke(this);
    }
}