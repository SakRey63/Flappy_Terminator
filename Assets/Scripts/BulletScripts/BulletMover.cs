using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public void Move(float direction)
    {
        if (direction < 0)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.World);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.Self);
        }
    }
}