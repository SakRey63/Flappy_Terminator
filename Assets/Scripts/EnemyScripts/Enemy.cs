using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private Detector _detector;
    [SerializeField] private ExplodeAnimator _explosion;
    [SerializeField] private float _delay;
    
    private float _direction = -1;
    private Game _game;

    public event Action<Enemy> ReturnToPool;
    
    private void OnEnable()
    {     
        StartCoroutine(RepeatAttack());
    }

    private void Update()
    {
        if (_detector.IsDestroyed)
        {
            ReturnToPool?.Invoke(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            ReturnToPool?.Invoke(this);
        }
    }

    private IEnumerator RepeatAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return delay;

            _spawner.СreateBullet(transform.rotation, _direction, _game);
        }
    }
    
    public void Reset()
    {
        _explosion.ExplosionAnimation(false);
        
        _detector.ChangeStatus();
    }

    public void SetGame(Game game)
    {
        _game = game;
        
        _game.FinishedGame += Finished;
        
        _detector.SetDirection(_direction);
    }
    
    private void Finished()
    {
        _game.FinishedGame -= Finished;
        
        ReturnToPool?.Invoke(this);
    }
}