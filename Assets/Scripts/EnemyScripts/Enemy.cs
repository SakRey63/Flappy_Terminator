using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IReturnableToPool
{
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private Detector _detector;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private float _delay;
    [SerializeField] private TimeService _timeService;
    
    private float _direction = -1;

    public bool IsDestroyed { get; private set; }

    public event Action<Enemy> Destroy;
    
    private void OnEnable()
    {     
        _timeService.ReturnableToPool += EndTime;
        
        StartCoroutine(RepeatAttack());
        
        SetDirection();
    }

    private void OnDisable()
    {
        _timeService.ReturnableToPool -= EndTime;
    }

    private void Update()
    {
        if (_detector.IsDestroyed)
        {
            _detector.SetStatus();

            IsDestroyed = true;
            
            StopCoroutine(RepeatAttack());
            
            _detector.gameObject.SetActive(false);
            
            _exploder.Explode();
        }

        if (_detector.IsFinished)
        {
            _detector.SetStatus();
            
            EndTime();
        }
        
        if (_exploder.IsExplosion)
        {
            _exploder.SetStatus();
            
            EndTime();
        }
    }

    private IEnumerator RepeatAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return delay;

            _spawner.SetParametersShot(transform.rotation, _direction);
        }
    }
    
    public void Reset()
    {
        _detector.gameObject.SetActive(true);
        
        IsDestroyed = false;
    }

    private void SetDirection()
    {
        _detector.SetDirection(_direction);
    }

    public void EndTime()
    {
        Destroy?.Invoke(this);
    }
}