using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _transform;

    private Rigidbody2D _rigidbody2D;
    private Transform _rotation;
    private float _direction;
    private Game _game;
    
    public void СreateBullet(Quaternion rotation, float direction, Game game)
    {
        _game = game;
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
        bullet.AchievedTarget += ReturnToPool;
        bullet.Reset();
        bullet.SetParameters(_direction, _game);
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        
        base.GetAction(bullet);
    }
    
    private void ReturnToPool(Bullet bullet)
    {
        bullet.AchievedTarget -= ReturnToPool;
        
        bullet.transform.rotation = Quaternion.Euler(0,0,0);

        if (bullet.IsTarget)
        {
            ReachTarget();
        }
        
        Release(bullet);
    }

    private void ReachTarget()
    {
        _game.ChangeValue();
    }
}