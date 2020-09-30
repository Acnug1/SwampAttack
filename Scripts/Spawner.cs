using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _enemyCount;

    public event UnityAction AllEnemySpawned;
    public event UnityAction<int ,int> EnemyCountChanged;
    public event UnityAction EndGame;

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++;
            _enemyCount++;
            ChangeEnemyCount(_enemyCount);

            _timeAfterLastSpawn = 0;

            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
        }

        if (_currentWave.Count <= _spawned)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                AllEnemySpawned?.Invoke();

            _currentWave = null;
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.InitTarget(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void ChangeEnemyCount(int enemyCount)
    {
        if (_waves.Count == _currentWaveNumber + 1 && enemyCount == 0)
        {
            EndGame?.Invoke();
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
        EnemyCountChanged?.Invoke(_spawned, _currentWave.Count);
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _enemyCount--;
        ChangeEnemyCount(_enemyCount);

        enemy.Dying -= OnEnemyDying;

        _player.AddMoney(enemy.Reward);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
