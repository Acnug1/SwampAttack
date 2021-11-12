using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState; // то состояние, в которое мы перейдем при переходе

    protected Player Target { get; private set; }

    public State TargetState => _targetState; // определяет, какое будет наше следующее состояние

    public bool NeedTransit { get; protected set; } // определяет, что пора переходить в другое состояние
    // изменяем данный параметр через потомков

    public void Init(Player target) // инициализируем переход и передаем Player target 
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false; // по умолчанию, когда мы включаем transition, он пока не готов переходить в следующее состояние
    }
}
