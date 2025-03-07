using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _transform;
    [SerializeField] private PlayerView _playerView;

    private Rigidbody2D _rigidbody2D;
    private Transform _rotation;
    private float _direction;
    
    public void СreateBullet(Quaternion rotation, float direction)
    {
        _transform.rotation = rotation;
        _direction = direction;
        
        GetGameObject();
    }

    protected override Bullet ChoosePrefab()
    {
        Prefab = _bullet;

        return Prefab;
    }

    protected override void GetAction(Bullet bullet)
    {
        bullet.Collision += ReturnInPool;
        bullet.HitTarget += ReachTarget;
        bullet.GetDirection(_direction);
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        
        base.GetAction(bullet);
    }
    
    private void ReturnInPool(Bullet bullet)
    {
        bullet.transform.rotation = Quaternion.Euler(0,0,0);
        bullet.Collision -= ReturnInPool;
        bullet.HitTarget -= ReachTarget;
        
        Release(bullet);
    }

    private void ReachTarget()
    {
        _playerView.ChangeValue();
    }
}