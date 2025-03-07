using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private BulletSpawner _spawner;
    [SerializeField] private float _delay;
    
    private float _direction = -1;

    public event Action<Enemy> Collision;
    
    private void OnEnable()
    {
        StartCoroutine(RepeatAttack());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            Collision?.Invoke(this);
        }
    }

    private IEnumerator RepeatAttack()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return delay;

            _spawner.СreateBullet(transform.rotation, _direction);
        }
    }

    public void Exploder()
    {
        Collision?.Invoke(this);
    }
}