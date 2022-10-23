using System;
using SoySauceSDK.Services.GDPR.Interface;

namespace SoySauceSDK.Services.GDPR
{
    public class GdprService : IConsentService
    {
        public bool IsConsentGiven { get; private set; } = false;

        public void Init(bool consent, Action<bool> onComplete)
        {
            IsConsentGiven = consent;
            onComplete?.Invoke(true); // send true because init succeed
        }
    }
}