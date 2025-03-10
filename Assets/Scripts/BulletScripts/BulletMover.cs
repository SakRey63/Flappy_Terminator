using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    public void MoveEnemy()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime, Space.Self);
    }

    public void MovePlayer()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime, Space.Self);
    }
}