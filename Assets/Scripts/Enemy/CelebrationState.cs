using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CelebrationState : State
{
    private Animator _animator;

    private void Awake() // получаем аниматор до OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() // включаем нашу анимацию
    {
        _animator.Play("Celebration");
    }

    private void OnDisable() // отключаем анимацию
    {
        _animator.StopPlayback();
    }
}
