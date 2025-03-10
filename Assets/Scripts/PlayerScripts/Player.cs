using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private Detector _detector;
    [SerializeField] private Exploder _exploder;
    
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
        
        if (_detector.IsDestroyed || _detector.IsFinished)
        {
           _detector.SetStatus();
           
           _detector.gameObject.SetActive(false);
            
           _exploder.Explode();
        }

        if (_exploder.IsExplosion)
        {
            _exploder.SetStatus();
            
            Dead();
        }
    }

    public void Reset(Vector2 position)
    {
        transform.position = new Vector2(position.x, position.y);
        
        _detector.gameObject.SetActive(true);
    }

    private void Attacked()
    {
        _bulletSpawner.SetParametersShot(transform.rotation, _direction);
    }

    private void SetForced()
    {
        _isForce = true;
    }

    private void Dead()
    {
        Killed?.Invoke();
    }
}