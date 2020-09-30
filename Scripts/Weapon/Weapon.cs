using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private int _ammunition;
    [SerializeField] private float _shootDelay;

    [SerializeField] protected Bullet Bullet;

    private int _currentAmmunition;

    public event UnityAction<int> AmmunitionChanged;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public int Ammunition => _ammunition;
    public int CurrentAmmunition => _currentAmmunition;
    public float ShootDelay => _shootDelay;

    public abstract void Shoot(Transform shootPoint);

    protected void ChangeAmmunition()
    {
        _currentAmmunition--;
        AmmunitionChanged?.Invoke(_currentAmmunition);
    }

    public void ReloadWeapon()
    {
        _currentAmmunition = _ammunition;
        AmmunitionChanged?.Invoke(_currentAmmunition);
    }

    public void Buy()
    {
        _isBuyed = true;
    }
}
