using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private AnimationExplosion _animation;
    [SerializeField] private float _delay = 0.7f;
    
    private float _direction;
    private float _stopSpeed = 0;
    
    public event Action<Bullet> Collision;
    public event Action HitTarget;

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            Collision?.Invoke(this);
        }
        else if (other.TryGetComponent(out Enemy enemy) && _direction > 0)
        {
            HitTarget?.Invoke();
            
            StartCoroutine(Exploder());
            
            enemy.Exploder();
        }
    }

    private IEnumerator Exploder()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        float tempSpeed = _speed;

        _speed = _stopSpeed;

        _animation.ExplosionAnimation(true);
        
        yield return delay;

        _speed = tempSpeed;
        
        Collision?.Invoke(this);
    }

    public void CollisionPlayer()
    {
        Collision?.Invoke(this);
    }

    public void GetDirection(float direction)
    {
        _direction = direction;
        
        _flipper.CreateDirection(_direction);
    }

    private void Move()
    {
        if (_direction < 0)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.Self);
        }
    }
}