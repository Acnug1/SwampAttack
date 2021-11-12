using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadDelay;
    [SerializeField] private float _deathTime;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;
    private Animator _animator;
    private float _elapsedTime;
    private bool _onReload;

    public int CurrentHealth => _currentHealth;
    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction Died; // используем данное событие для GameOverScreen

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();

        ReloadWeaponPlayer(_currentWeapon); // перезаряжаем оружие
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonUp(0)) // работает и для тачскрина на телефоне (0 - левая часть экрана)
        {
            ShootWeapon();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ReloadWeaponPlayer(_currentWeapon);
        }

        if (_onReload == true && _elapsedTime >= _reloadDelay)
        {
            _onReload = false;
        }
    }

    private void ShootWeapon()
    {
        if (_currentWeapon.CurrentAmmunition > 0 && _onReload == false && _currentHealth > 0)
        {
            if (_elapsedTime >= _currentWeapon.ShootDelay)
            {
                _animator.SetTrigger("Shoot");
                _currentWeapon.Shoot(_shootPoint); // Стреляем

                _elapsedTime = 0; // обнуляем прошедшее время для задержки перед следующим выстрелом
            }
        }
    }

    public void OnReloadWeapon()
    {
        ReloadWeaponPlayer(_currentWeapon);
    }

    private void ReloadWeaponPlayer(Weapon currentWeapon)
    {
        if (_currentHealth > 0)
        {
            _onReload = true;

            _animator.SetTrigger("Reload");
            currentWeapon.ReloadWeapon(); // Перезаряжаем оружие

            _elapsedTime = 0; // обнуляем прошедшее время, для начала перезарядки
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        _animator.SetTrigger("Hit");

        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("Death");
            StartCoroutine(DeathPlayer());
        }
    }

    private IEnumerator DeathPlayer()
    {
        var waitForSeconds = new WaitForSeconds(_deathTime);
        yield return waitForSeconds;

        _animator.StopPlayback();
        Destroy(gameObject);
        Died?.Invoke(); // оповещаем GameOverScreen о смерти игрока
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money); // добавляем баланс денег игрока в событие MoneyChanged и наш магазин
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price; // списываем деньги за оружие у игрока
        MoneyChanged?.Invoke(Money); // добавляем баланс денег игрока в событие MoneyChanged и наш магазин
        _weapons.Add(weapon); // добавляем новое оружие игроку в инвентарь
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1) // если текущий номер оружия равен последнему оружию в List
            _currentWeaponNumber = 0; // то текущем оружием выбираем нулевое
        else
            _currentWeaponNumber++; // иначе увеличиваем номер оружия на единицу

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeaponPlayer(_weapons[_currentWeaponNumber]); // перезаряжаем новое оружие
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0) // если текущий номер оружия равен 0
            _currentWeaponNumber = _weapons.Count - 1; // то текущем оружием выбираем последнее, т.е. _weapons.Count - 1
        else
            _currentWeaponNumber--; // иначе уменьшаем номер оружия на единицу

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeaponPlayer(_weapons[_currentWeaponNumber]); // перезаряжаем новое оружие
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
