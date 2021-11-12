using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        _spawner.EndGame += OnGameEnded;
    }

    private void OnDisable()
    {
        _spawner.EndGame -= OnGameEnded;
    }

    private void OnGameEnded()
    {
        _text.gameObject.SetActive(true); // активируем надпись конца игры
        panel.SetActive(true); // активируем панель меню
    }
}
