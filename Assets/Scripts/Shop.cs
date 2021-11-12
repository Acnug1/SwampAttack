using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template; // шаблон с просмотром оружия в магазине
    [SerializeField] private GameObject _itemContainer; // контейнер, куда мы создаем наши товары

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick; // подписываемся под событие SellButtonClick при создании оружия в магазине
        view.Render(weapon);
    }

    private void OnSellButtonClick(Weapon weapon, WeaponView view) // Если кто-то нажал на кнопку происходит обработка покупки SellButtonClick
    {
        TrySellWeapon(weapon, view);
    }

    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money) // если у игрока достаточно денег
        {
            _player.BuyWeapon(weapon); // продаем ему оружие
            weapon.Buy(); // устанавливает, что данное оружие продано
            view.SellButtonClick -= OnSellButtonClick; // отписываемся от события SellButtonClick при продаже оружия
        }
    }
}
