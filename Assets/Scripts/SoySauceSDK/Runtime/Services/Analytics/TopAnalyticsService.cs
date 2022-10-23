using System;
using SoySauceSDK.Services.Analytics.Adapter;
using SoySauceSDK.Services.Analytics.Adapter.Interface;
using SoySauceSDK.Services.Analytics.Interface;
using SoySauceSDK.Services.GDPR.Interface;

namespace SoySauceSDK.Services.Analytics
{
    public class TopAnalyticsService : IAnalyticsService
    {
        private IConsentService _consentService;
        private readonly IAnalyticsAdapter _adapter;

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
            if (_consentService.IsConsentGiven)
            {
                _adapter.TrackEvent(eventName);
            }
        }
    }
}