using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class DeathState : State
{
    [SerializeField] private float _deathTime;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("Death");

        gameObject.GetComponent<BoxCollider2D>().isTrigger = false; // отключаем триггер у коллайдера, после смерти врага
        StartCoroutine(DeathEnemy());
    }

    private IEnumerator DeathEnemy()
    {
        var waitForSeconds = new WaitForSeconds(_deathTime);
        yield return waitForSeconds;

        _animator.StopPlayback();
        Destroy(gameObject);
    }
}
