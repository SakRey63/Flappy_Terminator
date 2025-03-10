using UnityEngine;

public class Detector : MonoBehaviour
{
    public bool IsFinished { get; private set; }
    public bool IsDestroyed { get; private set; }
    public float Direction { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Finish>(out _))
        {
            IsFinished = true;
        }
        else if(other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.Direction != Direction)
            {
                bullet.EndTime();
                
                IsDestroyed = true;
            }
        }
    }

    public void SetStatus()
    {
        IsFinished = false;
        IsDestroyed = false;
    }

    public void SetDirection(float direction)
    {
        Direction = direction;
    }
}