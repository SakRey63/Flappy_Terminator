using System.Collections;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private ExplodeAnimator _explosion;
    [SerializeField] private float _delay = 0.7f;
    
    public bool IsExplosion { get; private set; }
  
    private IEnumerator  SetExplosion()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
       
        _explosion.ExplosionAnimation(true);
           
        yield return delay;
        
        _explosion.ExplosionAnimation(false);

        IsExplosion = true;
    } 
    
    public void Explode()
    {
        StartCoroutine(SetExplosion());
    }
    
    public void SetStatus()
    {
        IsExplosion = false;
    }
}