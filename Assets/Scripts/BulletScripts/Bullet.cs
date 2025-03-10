using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IReturnableToPool
{
    [SerializeField] private Flipper _flipper;
    [SerializeField] private BulletMover _mover;
    [SerializeField] private TimeService _timeService;
    
    public float Direction { get; private set; }
    
    public event Action<Bullet> AchievedTarget;

    private void OnEnable()
    {
        _timeService.ReturnableToPool += EndTime;
    }

    private void OnDisable()
    {
        _timeService.ReturnableToPool -= EndTime;
    }

    private void Update()
    {
        if (Direction < 0)
        {
            _mover.Move(Vector2.left);
        }
        else
        {
            _mover.Move(Vector2.right);
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
    
    public void EndTime()
    {
        AchievedTarget?.Invoke(this);
    }
}