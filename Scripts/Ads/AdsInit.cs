using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInit : MonoBehaviour
{
    private string gameId = "3840853";
    private bool testMode = true;

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    private IEnumerator ShowBannerWhenReady()
    {
        var _waitForSeconds = new WaitForSeconds(0.5f);
        while (!Advertisement.IsReady("MainBottom"))
        {
            yield return _waitForSeconds;
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("MainBottom");
    }
}
