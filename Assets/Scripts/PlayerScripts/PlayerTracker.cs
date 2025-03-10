using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        if (_player != null)
        {
            Vector3 position = transform.position;
            position.x = _player.transform.position.x + _xOffset;
            transform.position = position;
        }
    }
}