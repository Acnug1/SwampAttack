using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions; // создаем список переходов

    protected Player Target { get; private set; }

    public void Enter(Player target) // Вход в состояние
    {
        if (enabled == false) // если состояние объекта отключено (скрипт отключен)
        {
            Target = target; // присваиваем наш Target
            enabled = true; // включаем наш скрипт

            foreach (var transition in _transitions) // В коллекции _transitions перебираем все переходы
            {
                transition.enabled = true; // включаем наши переходы _transitions 
                transition.Init(Target); // инициируем переходы и передаем в них наш Target
            }
        }
    }

    public void Exit() // выход из состояния
    {
        if (enabled == true) // если состояние объекта включено (скрипт включен)
        {
            foreach (var transition in _transitions) // перебираем все наши переходы
                transition.enabled = false; // выключаем все переходы

            enabled = false; // выключаем сам объект (скрипт)
        }
    }

    public State GetNextState() // возвращает следующее состояние, в которое мы хотим попасть
    {
        foreach (var transition in _transitions) // перебираем все наши переходы
        {
            if (transition.NeedTransit) // Если нужен переход
                return transition.TargetState; // возвращает то состояние, которое указано в переходе
        }

        return null; // если переход не требуется возвращаем null
    }
}
