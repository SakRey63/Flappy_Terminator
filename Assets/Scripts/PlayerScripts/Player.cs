using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ExplodeAnimator _explosion;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Detector _detector;
    [SerializeField] private Game _game;
    
    private float _direction = 1;
    private bool _isForce = false;

    public event Action Killed;
    
    private void OnEnable()
    {
        _inputReader.Attacked += Attacked;
        _inputReader.Forced += SetForced;
    }

    private void OnDisable()
    {
        _inputReader.Attacked -= Attacked;
        _inputReader.Forced -= SetForced;
    }

    private void FixedUpdate()
    {
        _mover.Move(_isForce);

        if (_isForce)
        {
            _isForce = false;
        } 
        
        if (_detector.IsDestroyed)
        {
            Dead();
            
            _detector.ChangeStatus();
        }
    }

    public void Reset(Vector2 position)
    {
        transform.position = new Vector2(position.x, position.y);
        
        _explosion.ExplosionAnimation(false);
    }

    private void Attacked()
    {
        _bulletSpawner.СreateBullet(transform.rotation, _direction, _game);
    }

    private void SetForced()
    {
        _isForce = true;
    }

    private void Dead   ()
    {
        Killed?.Invoke();
    }
}