using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private AnimationExplosion _explosion;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private ScannerDetector _scanner;
    
    private float _direction = 1;
    private bool _isForce = false;

    public event Action Destroyed;
    
    private void OnEnable()
    {
        _inputReader.Attack += Attack;
        _inputReader.Force += SetForce;
    }

    private void OnDisable()
    {
        _inputReader.Attack -= Attack;
        _inputReader.Force -= SetForce;
    }

    private void FixedUpdate()
    {
        _mover.Move(_isForce);

        _isForce = false;

        ChangeStatus();
    }

    public void Reset()
    {
        _explosion.ExplosionAnimation(false);
        
        _scanner.ChangeStatus();
    }

    private void Attack()
    {
        _bulletSpawner.СreateBullet(transform.rotation, _direction);
    }

    private void SetForce()
    {
        _isForce = true;
    }

    private void ChangeStatus()
    {
        if (_scanner.IsDestroyed)
        {
            Destroyed?.Invoke();
        }
    }
}