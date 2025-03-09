using System.Collections;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private ExplodeAnimator _explosion;
    [SerializeField] private float _delay = 0.7f;

    public bool IsDestroyed { get; private set; }
    public float Direction { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            StartCoroutine(Explode());
        }
        else if(other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.Direction != Direction)
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

        IsDestroyed = true;
    }

    public void ChangeStatus()
    {
        IsDestroyed = false;
    }

    public void SetDirection(float direction)
    {
        Direction = direction;
    }
}