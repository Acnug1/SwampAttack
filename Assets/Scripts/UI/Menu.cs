using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true); // активируем панель меню
        Time.timeScale = 0; // останавливаем время (ставим игру на паузу)
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false); // закрываем панель меню
        Time.timeScale = 1; // запускаем время в обычном режиме (возобновляем игру)
    }

    public void Restart()
    {
        Time.timeScale = 1; // запускаем время в обычном режиме (возобновляем игру)
        SceneManager.LoadScene(0); // Перезагрузка нулевой сцены
    }

    public void Exit()
    {
        Application.Quit();
    }
}
