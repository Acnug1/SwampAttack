using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsForGold : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameObject startWatchingButton;
    [SerializeField] private Player _player;
    [SerializeField] private int _reward;

    private string gameId = "3840853"; // id игры на андроид
    private string myPlacementId = "rewardedVideo"; // инициализируем видеорекламу с вознаграждением с Unity Dashboard
    private bool testMode = true; // запуск в тестовом режиме

    private void Start()
    {
        Advertisement.AddListener(this); // добавляем слушателем (обработчиком события) текущий класс для реализации интерфейса IUnityAdsListener
        Advertisement.Initialize(gameId, testMode); // инициализируем рекламный контекст

        startWatchingButton.GetComponent<Button>().onClick.AddListener(() => // при нажатии на кнопку обрабатываем событие, записанное через лямбда выражение
        {
            Advertisement.Show(myPlacementId); // показываем нашу видеорекламу в полноэкранном режиме
            startWatchingButton.SetActive(false); // скрываем отображение кнопки
        });
    }

    // реализуем все методы внутри интерфейса IUnityAdsListener в текущем классе
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) // Когда показ рекламы завершился
    {
        Time.timeScale = 1; // возобновляем игру

        if (showResult == ShowResult.Finished) // если игрок досмотрел рекламу до конца
        {
            Debug.Log($"Вам начислено {_reward} монеток"); // выводим сообщение о начислении монеток
            _player.AddMoney(_reward);
        }
        else if (showResult == ShowResult.Skipped) // если реклама пропущена игроком
        {
        }
        else if (showResult == ShowResult.Failed) // если рекламу не удалось досмотреть
        {
        }
    }

    public void OnUnityAdsReady(string placementId) // вызывается метод "когда реклама готова к показу". В параметрах передается наименование видео, готовое к показу когда оно подгрузилось
    {
        if (placementId == myPlacementId) // если наше видео myPlacementId = "rewardedVideo" готово к показу
        {
            startWatchingButton.SetActive(true); // активируем кнопку
        }
    }

    public void OnUnityAdsDidError(string message) // вызывается метод "когда показ рекламы завершился ошибкой"
    {
        Debug.Log(message); // Выдаем сообщение об ошибке
        Time.timeScale = 1; // возобновляем игру
    }

    public void OnUnityAdsDidStart(string placementId) // вызывается метод "когда реклама запущена"
    {
        Time.timeScale = 0; // приостанавливаем игру
    }
}
