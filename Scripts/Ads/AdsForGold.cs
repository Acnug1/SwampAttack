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

    private string gameId = "3840853";
    private string myPlacementId = "rewardedVideo";
    private bool testMode = true;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

        startWatchingButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Advertisement.Show(myPlacementId);
            startWatchingButton.SetActive(false);
        });
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Time.timeScale = 1;

        if (showResult == ShowResult.Finished)
        {
            Debug.Log($"Вам начислено {_reward} монеток");
            _player.AddMoney(_reward);
        }
        else if (showResult == ShowResult.Skipped)
        {
        }
        else if (showResult == ShowResult.Failed)
        {
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            startWatchingButton.SetActive(true);
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
        Time.timeScale = 1;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Time.timeScale = 0;
    }
}
