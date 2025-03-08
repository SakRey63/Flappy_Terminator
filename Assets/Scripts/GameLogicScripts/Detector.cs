using System.Collections;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private ExplodeAnimator _explosion;
    [SerializeField] private float _delay = 0.7f;

    private bool _isDestroyed = false;
    private float _direction;

    public bool IsDestroyed => _isDestroyed;
    public float Direction => _direction;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            StartCoroutine(Explode());
        }
        else if(other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.Direction != _direction)
            {
                bullet.ReturnToPool();
                
                StartCoroutine(Explode());
            }
        }
    }
    
    private IEnumerator Explode()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
    
        _explosion.ExplosionAnimation(true);
        
        yield return delay;

        _isDestroyed = true;
    }

    public void ChangeStatus()
    {
        _isDestroyed = false;
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }
}