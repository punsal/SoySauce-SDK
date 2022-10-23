using System;
using _3rdParty;
using SoySauceSDK.Services.Analytics.Adapter.Interface;
using UnityEngine;

namespace SoySauceSDK.Services.Analytics.Adapter
{
    public class TopAnalyticsAdapter : IAnalyticsAdapter
    {
        public void Init(bool consent)
        {
            TopAnalytics.InitWithConsent(consent);
        }

        public void TrackEvent(string eventName)
        {
            try
            {
                TopAnalytics.TrackEvent(eventName);
            }
            catch (Exception e)
            {
                Debug.Log($"While tracking event:\n{e.Message}");
                throw;
            }
        }
    }
}