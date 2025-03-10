using UnityEngine;

public class BulletSpawner : Spawner<Bullet>
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _transform;

    private float _direction;
    
    public void SetParametersShot(Quaternion rotation, float direction)
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

    protected override void SetAction(Bullet bullet)
    {
        bullet.AchievedTarget += ReturnToPool;
        bullet.SetParameters(_direction);
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        
        base.SetAction(bullet);
    }
    
    private void ReturnToPool(Bullet bullet)
    {
        bullet.AchievedTarget -= ReturnToPool;
        
        bullet.transform.rotation = Quaternion.Euler(0,0,0);
        
        Release(bullet);
    }
}