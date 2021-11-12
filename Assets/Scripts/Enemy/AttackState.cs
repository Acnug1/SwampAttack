using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private float _lastAttackTime; // время с последней атаки
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_lastAttackTime <= 0) // если время с прошлой атаки прошло
        {
            Attack(Target); // атакуем игрока
            _lastAttackTime = _delay; // сбрасываем таймер
        }
        _lastAttackTime -= Time.deltaTime; // постепенно вычитаем время с прошлой атаки
    }

    private void Attack(Player target)
    {
        if (target.CurrentHealth > 0) // если здоровье цели (игрока) больше нуля
        {
            _animator.Play("Attack"); // Воспроизведение анимации атаки (активация state в аниматоре)
            target.ApplyDamage(_damage);
        }
        else
            _animator.Play("Idle"); // Воспроизведение анимации ожидания смерти игрока
    }
}
