using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmunitionDisplay : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private TMP_Text _ammunitionDisplay;

    private void OnEnable()
    {
        _weapon.AmmunitionChanged += OnAmmunitionChanged;
    }

    private void OnDisable()
    {
        _weapon.AmmunitionChanged -= OnAmmunitionChanged;
    }

    private void OnAmmunitionChanged(int currentAmmunition)
    {
        _ammunitionDisplay.text = currentAmmunition.ToString();
    }
}
