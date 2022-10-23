using System;
using _3rdParty.Runtime;
using SoySauceSDK.Runtime.Services.Analytics.Adapter.Interface;
using UnityEngine;

namespace SoySauceSDK.Runtime.Services.Analytics.Adapter
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