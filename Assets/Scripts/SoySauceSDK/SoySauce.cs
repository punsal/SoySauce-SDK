using System;
using SoySauceSDK.Services.Ads;
using SoySauceSDK.Services.Ads.Interface;
using SoySauceSDK.Services.Analytics;
using SoySauceSDK.Services.Analytics.Interface;
using SoySauceSDK.Services.GDPR;
using SoySauceSDK.Services.GDPR.Interface;
using UnityEngine;

namespace SoySauceSDK
{
    public static class SoySauce{

        #region Adapters

        private static IConsentService _consentService;
        private static IAnalyticsService _analyticsService;
        private static IAdsService _adsService;

        #endregion

        #region Settings

        private static SoySauceSettings _settings;

        private static SoySauceSettings Settings
        {
            get
            {
                if (_settings != null) return _settings;
                _settings = Resources.Load<SoySauceSettings>(SoySauceGlobals.SoySauceSettingsAssetName);
                if (_settings != null) return _settings;
                Debug.Log("Create a default SoySauceSettings under Resources folder.");
                _settings = ScriptableObject.CreateInstance<SoySauceSettings>();
                _settings.ResetSettings();

                return _settings;
            }
        }

        #endregion
	
        // Before calling methods in TopAds and TopAnalytics you must call their init methods 
        // TopAds requires the TopAds prefab to be created in the scene
        // You also need to collect user GDPR consent and pass that boolean value to TopAds and TopAnalytics 
        // You can collect this consent by displaying a popup to the user at the start of the game and then storing that value for future use
        public static void Init()
        {
            // Removed bool param because I wanted to check consent like it's a feature of SDK.
            var consent = GetConsentStatus();
            switch (consent)
            {
                case ConsentStatus.Denied:
                case ConsentStatus.Approved:
                    InitializeServices(consent == ConsentStatus.Approved);
                    break;
                case ConsentStatus.NotDetermined:
                    var prefab = Resources.Load<GameObject>(SoySauceGlobals.SoySauceConsentPrefabName);
                    if (prefab != null)
                    {
                        // ReSharper disable once AccessToStaticMemberViaDerivedType
                        GameObject.Instantiate(prefab);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
        }

        public static void SetConsent(bool consent)
        {
            SoySauceDatabase.Instance.Write(SoySauceGlobals.SoySauceConsentKey,
                consent ? ConsentStatus.Approved.ToString() : ConsentStatus.Denied.ToString());
            InitializeServices(consent);
        }

        private static void InitializeServices(bool consent)
        {
            _consentService = new GdprService();
            _consentService?.Init(consent, success =>
            {
                if (!success) return;
                Debug.Log("GdprAdapter init is successful.");
         			
            });
            
            _analyticsService = new TopAnalyticsService(_consentService);
            _analyticsService.Init(consent, success =>
            {
                if (!success) return;
                Debug.Log("AnalyticsAdapter init is successful.");
				
            });

            _adsService = new AdsService(Settings.AdsServiceSettings);
            _adsService.Init(consent, success =>
            {
                if (success)
                {
                    Debug.Log("AdsAdapter init is successful.");
                    _adsService.OnAdShown += OnAdShown;
                }
            });
        }


        public static ConsentStatus GetConsentStatus()
        {
            var consentValue = SoySauceDatabase.Instance.Read(SoySauceGlobals.SoySauceConsentKey);
            return Enum.TryParse<ConsentStatus>(consentValue, out var status) ? status : ConsentStatus.NotDetermined;
        }
        
        public static void StartGame()
        {
            // Track in TopAnalytics that a game has started 
            _analyticsService.TrackEvent(Events.GameStarted);
            DisplayToast();
        }


        public static void EndGame()
        {
            // Track in TopAnalytics that a game has ended
            _adsService.IncrementGamesCounter();
            _analyticsService.TrackEvent(Events.GameEnded);
        }
	
        public static void ShowAd()
        {
            // TopAds methods must be called with a unique "string" ad unit id 
            // For your test app that id is "f4280fh0318rf0h2" 
            // However, when releasing the SDK to other studios, their ad unit id will be different 
            // Please find a flexible way to allow studios to provide their ad unit id to your SoySauce SDK 
            
            /*
             * TODO: PRESENT SOLUTION: Creating a SDKSettings and put info in it. An Editor-based solution would be great.
             */

            // Before an ad is available to display, you must call TopAds.RequestAd 
            // You must call RequestAd each time before an ad is ready to display 

            // RequestAd will make a "fake" request for an ad that will take 0 to 10 seconds to complete
            // Afterwards, either the OnAdLoadedEvent or OnAdFailedEvent will be invoked 
            // Please implement an autorequest system that ensures an ad is always ready to be displayed
            // Keep in mind that RequestAd can fail multiple times in a row 

            // If an ad is loaded correctly, clicking on the "Show Ad" button within Unity-SoySauceTestApp 
            // should display a fake ad popup that you can close. 


            // Track in TopAnalytics when an ad is displayed.  Hint: TopAds.OnAdShownEvent 
            _adsService.ShowAd();
        }

        private static void OnAdShown()
        {
            _analyticsService.TrackEvent(Events.AdShown);
        }

        public static void SetAdDisplayConditions(int secondsBetweenAds, int gamesBetweenAds)
        {
            // Sometimes studios call "ShowAd" too often and bombard players with ads 
            // Add a system that prevents the "ShowAd" method from showing an available ad 
            // Unless EITHER condition provided is true: 
            // 1) secondsBetweenAds: only show an ad if the previous ad was shown more than "secondBetweenAds" ago 
            // 2) gamesBetweenAds: only show an ad if "gamesBetweenAds" amount of games was played since the previous ad 
		
            //AdManager.SetAdDisplayConditions(secondsBetweenAds, gamesBetweenAds);
            Settings.AdsServiceSettings.SecondsBetweenAds = secondsBetweenAds;
            Settings.AdsServiceSettings.GamesBetweenAds = gamesBetweenAds;
        }
        
        private static void DisplayToast()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaClass unityActivity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
            object[] toastParams = new object[3];
            toastParams[0] = unityActivity.GetStatic<AndroidJavaObject> ("currentActivity");
            toastParams[1] = "Hello";
            toastParams[2] = toastClass.GetStatic<int> ("LENGTH_SHORT");
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", toastParams);
            toastObject.Call ("show");
#endif
        }
    }
}