using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Transform _point;
    [SerializeField] private float _minPositionY = -4f;
    [SerializeField] private float _maxPositionY = 5f;
    [SerializeField] private Enemy[] _enemies;
    [SerializeField] private float _delay = 2f;
    [SerializeField] private DisplayCounter _display;

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
    
    protected override void SetAction(Enemy enemy)
    {
        enemy.Destroy += Destroy;
        
        enemy.transform.position = new Vector2(_point.position.x, Random.Range(_minPositionY, _maxPositionY));
        
        base.SetAction(enemy);
    }

    private void Destroy(Enemy enemy)
    {
        enemy.Destroy -= Destroy;

        if (enemy.IsDestroyed)
        {
            _display.ChangeValue();
        }
        
        enemy.Reset();
        
        Release(enemy);
    }
}