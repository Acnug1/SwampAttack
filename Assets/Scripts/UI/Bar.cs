using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public void OnValueChanged(int value, int maxValue) 
    {
        Slider.value = (float)value / maxValue; // находим соотношение текущего и максимального зачения слайдера и приводим его значение к нормализованому виду (от 0 до 1)
    }
}
