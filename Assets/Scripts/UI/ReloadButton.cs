using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour
{
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _reloadButton.onClick.AddListener(OnReloadButtonClick);
    }

    private void OnDisable()
    {
        _reloadButton.onClick.RemoveListener(OnReloadButtonClick);
    }

    private void OnReloadButtonClick()
    {
        _player.OnReloadWeapon();
    }
}
