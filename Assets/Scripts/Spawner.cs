using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves; // можем вывести список волн из не наследуемого класса Wave
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn; // время прошедшее с предыдущего спавна
    private int _spawned; // сколько уже врагов было создано
    private int _enemyCount; // подсчет количества врагов

    public event UnityAction AllEnemySpawned; // Событие "все враги заспавнились"
    public event UnityAction<int ,int> EnemyCountChanged; // Событие оповещает об изменении количества врагов
    public event UnityAction EndGame; // Событие, которое вызывается, когда все волны врагов закончились

    private void Start()
    {
        SetWave(_currentWaveNumber);
    }

    private void Update()
    {
        if (_currentWave == null) // если волны кончились или отсутствуют
            return; // выходим из update

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            InstantiateEnemy();
            _spawned++; // увеличиваем счетчик количества врагов на единицу
            _enemyCount++;
            ChangeEnemyCount(_enemyCount); // вызываем метод ChangeEnemyCount и передаем количество заспавненных врагов

            _timeAfterLastSpawn = 0; // сбрасываем наш таймер

            EnemyCountChanged?.Invoke(_spawned, _currentWave.Count); // передаем в обработчик события количество заспавнененных врагов и общее количество врагов в волне
        }

        if (_currentWave.Count <= _spawned) // если количество врагов меньше или равно тому, что мы заспаунили
        {
            if (_waves.Count > _currentWaveNumber + 1) // Если следующая волна существует (т.е. волн хватает)
                AllEnemySpawned?.Invoke(); // Вызываем событие "все враги заспавнились" после окончания предыдущей волны

            _currentWave = null; // обнуляем текущую волну
        }
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.InitTarget(_player);
        enemy.Dying += OnEnemyDying; // подписываемся под событие при создании экземпляра врага
    }

    private void ChangeEnemyCount(int enemyCount)
    {
        if (_waves.Count == _currentWaveNumber + 1 && enemyCount == 0) // когда текущая волна последняя и не осталось врагов в волне
        {
            EndGame?.Invoke(); // вызываем событие конец игры
        }
    }

    private void SetWave(int index) // установить волну (int index - номер волны)
    {
        _currentWave = _waves[index];
    }

    public void NextWave()
    {
        SetWave(++_currentWaveNumber);
        _spawned = 0;
        EnemyCountChanged?.Invoke(_spawned, _currentWave.Count); // при запуске новой волны обнуляем значение события EnemyCountChanged
    }

    private void OnEnemyDying(Enemy enemy) // принимаем в параметрах именно того врага, кто это событие инициировал
    {
        _enemyCount--;
        ChangeEnemyCount(_enemyCount);

        enemy.Dying -= OnEnemyDying; // отписываемся от события при уничтожении экземпляра врага

        _player.AddMoney(enemy.Reward); // и передаем игроку деньги в награду
    }
}

[System.Serializable] // Отображение не наследуемого класса в инспекторе
public class Wave
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
