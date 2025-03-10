using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _tapeForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _transform;

    private Vector2 _startPosition;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;
    
    private void Start()
    {
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    public void Move(bool isForce)
    {
        if (isForce)
        {
            _rigidbody2D.velocity = new Vector2(_speed, _tapeForce);

            _transform.rotation = _maxRotation;
        }
        
        _transform.rotation = Quaternion.Lerp(_transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
}