using System;
using SoySauceSDK.Runtime.Services.Analytics.Adapter;
using SoySauceSDK.Runtime.Services.Analytics.Adapter.Interface;
using SoySauceSDK.Runtime.Services.Analytics.Interface;
using SoySauceSDK.Runtime.Services.GDPR.Interface;

namespace SoySauceSDK.Runtime.Services.Analytics
{
    public class TopAnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsAdapter _adapter;
        private readonly IConsentService _consentService;

        public TopAnalyticsService(IConsentService consentService)
        {
            _consentService = consentService;
            _adapter = AnalyticsAdapterFactory.Create();
        }

        public void Init(bool consent, Action<bool> onComplete)
        {
            _adapter.Init(consent);
            onComplete?.Invoke(true);
        }

        public void TrackEvent(string eventName)
        {
            if (_consentService.IsConsentGiven) _adapter.TrackEvent(eventName);
        }
    }
}