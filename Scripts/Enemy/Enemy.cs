using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;

    private Player _target;
    private Animator _animator;

    public int Health => _health;
    public int Reward => _reward;
    public Player Target => _target;

    public event UnityAction<Enemy> Dying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void InitTarget(Player target)
    {
        _target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");

        if (_health <= 0)
        {
            Dying?.Invoke(this);
        }
    }
}
