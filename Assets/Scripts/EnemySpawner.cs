using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _minPositionY = -4f;
    [SerializeField] private float _maxPositionY = 5f;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _delay = 1.5f;

    private void Start()
    {
        StartCoroutine(RepeatEnemy());
    }
    
    private IEnumerator RepeatEnemy()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            GetGameObject();
            
            yield return delay;
        }
    }
    
    protected  override Enemy ChoosePrefab()
    { 
        return _enemies[Random.Range(0, _enemies.Length)];
    }
    
    protected override void GetAction(Enemy enemy)
    {
        enemy.Collision += ReturnInPool;
        
        enemy.transform.position = new Vector2(_point.position.x, Random.Range(_minPositionY, _maxPositionY));
        
        base.GetAction(enemy);
    }

    private void ReturnInPool(Enemy enemy)
    {
        enemy.Collision -= ReturnInPool;
        
        Release(enemy);
    }
}