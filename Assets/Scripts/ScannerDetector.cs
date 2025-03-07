using System.Collections;
using UnityEngine;

public class ScannerDetector : MonoBehaviour
{
    [SerializeField] private AnimationExplosion _explosion;
    [SerializeField] private float _delay = 0.7f;

    private bool _isDestroyed = false;

    public bool IsDestroyed => _isDestroyed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            StartCoroutine(Exploder());
        }
        else if(other.TryGetComponent(out Bullet bullet))
        {
            StartCoroutine(Exploder());
            
            bullet.CollisionPlayer();
        }
    }
    
    private IEnumerator Exploder()
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
}