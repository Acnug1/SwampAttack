using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Player _target;
    private State _currentState;

    public State Current => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target; // из компонента Enemy считываем свойство Player на чтение (т.е. наш Target) и записываем в нашу цель Player _target
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null) // 1. если текущее состояние отсутствует не крутим дальше апдейт
            return;

        var nextState = _currentState.GetNextState(); // определяет состояние, куда бы мы могли пойти
        if (nextState != null) // Если возвращается не null (если мы готовы к переходу)
            Transit(nextState); // Осуществляем переход к нашему состоянию      
    }

    private void Reset(State startState) // 0. сбрасываем состояние объекта на стартовое при создании (вызывается только на старте игры)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState) // 2. переход в новое состояние
    {
        if (_currentState != null) // Если текущее состояние != null, то нам нужно выйти из нашего состояния
            _currentState.Exit(); // закрываем текущее состояние (чтобы не выполнялся start и update прошлого состояния)

        _currentState = nextState; // текущее состояние будет равно следующему состоянию

        if (_currentState != null)
            _currentState.Enter(_target); // входим в это состояние
    }
}
