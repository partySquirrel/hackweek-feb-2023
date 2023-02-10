using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public GameObject button; 
    public bool testMode = true;
    
    public string GAME_ID_ANDROID = "5153301"; //replace with your gameID from dashboard. note: will be different for each platform.
    public string GAME_ID_IOS = "5153300"; //replace with your gameID from dashboard. note: will be different for each platform.

    private const string adUnitIdAndroid = "Interstitial_Android";
    private const string adUnitIdIOS = "Interstitial_iOS";
    private string myAdUnitId;

    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
        }
        
        #if UNITY_IOS
            Advertisement.Initialize(GAME_ID_IOS, testMode, this);
            myAdUnitId = adUnitIdIOS;
        
            DebugLog("Advertisement initialized for IOS");
        #elif UNITY_ANDROID
            Advertisement.Initialize(GAME_ID_ANDROID, testMode, this);
            myAdUnitId = adUnitIdAndroid;
        
            DebugLog("Advertisement initialized for ANDROID");
        #else
            // currently just using ios settings as default
            Advertisement.Initialize(GAME_ID_IOS, testMode, this);
            myAdUnitId = adUnitIdIOS;
            
            DebugLog("Advertisement initialized for IOS by default");
        #endif
    }

    public void LoadNonRewardedAd()
    {
        Advertisement.Load(myAdUnitId, this);
    }

    public void ShowNonRewardedAd()
    {
        Advertisement.Show(myAdUnitId, this);
        button.SetActive(false);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
        LoadNonRewardedAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
        
        AnalyticsService.Instance.CustomData("adStarted", new Dictionary<string, object>());
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
        LoadNonRewardedAd();
    }
    #endregion

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
