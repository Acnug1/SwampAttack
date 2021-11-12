using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyBalance : MonoBehaviour
{
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Player _player;

    public void OnEnable() // При включении магазина
    {
        _money.text = _player.Money.ToString(); // отображается текущее количество денег игрока (при отключенном магазине)
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _money.text = money.ToString(); // отображается текущее количество денег игрока (при включенном магазине). Событие работает, даже если игра стоит на паузе
    }
}
