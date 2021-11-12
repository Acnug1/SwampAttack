using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange; // дистанция от игрока
    [SerializeField] private float _rangeSpread; // разброс дистанции

    private void Start()
    {
        _transitionRange += Random.Range(-_rangeSpread, _rangeSpread); // остановка enemy на случайном диапазоне от игрока от -_rangeSpread до _rangeSpread
    }

    private void Update()
    {
        if (Target != null)
        {
            if (Vector2.Distance(transform.position, Target.transform.position) < _transitionRange) // Если дистанция между врагом и игроком меньше, чем дистанция до игрока
            {
                NeedTransit = true; // нужна смена состояния
            }
        }
    }
}
