using System;
using SoySauceSDK.Runtime.Services.Ads.Adapter.Interface;
using UnityEngine;

namespace SoySauceSDK.Runtime.Services.Ads.Adapter
{
    public class NullObjectAdsAdapter : IAdsAdapter
    {
        public Action OnAdShown { get; set; }

        public bool Create()
        {
            Debug.Log("This is creation of NullObjectAdsAdapter for error handling.");
            var result = CreateErrorHandler();
            return result;
        }

        public bool Init(bool consent, string adId)
        {
            Debug.Log("This is initialization of NullObjectAdsAdapter for error handling.");
            return false;
        }

        public bool IsAdReady()
        {
            Debug.Log("NullObjectAdsAdapter won't get any ads.");
            return false;
        }

        public void ShowAd()
        {
            Debug.Log("NullObjectAdsAdapter cannot show any ad.");
        }

        private static bool CreateErrorHandler()
        {
            Debug.LogWarning("Creating AdsAdapterErrorHandler...");
            var temp = new GameObject("AdsAdapterErrorHandler");
            var errorHandler = temp.AddComponent<AdsAdapterErrorHandler>();
            if (errorHandler != null)
            {
                errorHandler.Resolve();
                return true;
            }

            Debug.LogError("Cannot add AdsAdapterErrorHandler!");
            return false;
        }
    }
}