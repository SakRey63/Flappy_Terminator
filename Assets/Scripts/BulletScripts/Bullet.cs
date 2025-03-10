using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private BulletMover _mover;
    
    public float Direction { get; private set; }
    
    public event Action<Bullet> AchievedTarget;

    private void Update()
    {
        if (Direction < 0)
        {
            _mover.MoveEnemy();
        }
        else
        {
            _mover.MovePlayer();
        }

        if (Time.timeScale == 0)
        {
            ReturnToPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            AchievedTarget?.Invoke(this);
        }
    }

    public void SetParameters(float direction)
    {
        Direction = direction;
        
        _flipper.CreateDirection(Direction);
    }
    
    public void ReturnToPool()
    {
        AchievedTarget?.Invoke(this);
    }
}