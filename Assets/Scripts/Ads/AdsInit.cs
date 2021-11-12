using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour
{
    private string gameId = "3840853"; // id игры на андроид
    private bool testMode = true; // запуск в тестовом режиме

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode); // инициализация рекламного баннера
        StartCoroutine(ShowBannerWhenReady()); // запуск корутины с воспроизведение рекламы на баннере
    }

    private IEnumerator ShowBannerWhenReady() // корутина "показать баннер, когда реклама будет готова"
    {
        var _waitForSeconds = new WaitForSeconds(0.5f);
        while (!Advertisement.IsReady("MainBottom")) // пока наша реклама не готова
        {
            yield return _waitForSeconds; // ждем полсекунды и потом опять проверяем, пока она не будет готова
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER); // настроиваем место для отображения рекламы в игре
        Advertisement.Banner.Show("MainBottom"); // отобразить наш баннер с рекламой после завершения корутины
    }
}
