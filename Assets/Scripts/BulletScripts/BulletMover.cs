using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public void Move(Vector2 direction)
    {
        transform.Translate(direction * (_speed * Time.deltaTime), Space.Self);
    }
}