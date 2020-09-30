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
    public event UnityAction Died;

    private void Start()
    {
        ChangeWeapon(_weapons[_currentWeaponNumber]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();

        ReloadWeaponPlayer(_currentWeapon);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonUp(0))
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
                _currentWeapon.Shoot(_shootPoint);

                _elapsedTime = 0;
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
            currentWeapon.ReloadWeapon();

            _elapsedTime = 0;
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
        Died?.Invoke();
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChanged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeaponPlayer(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0)
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
        ReloadWeaponPlayer(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }
}
