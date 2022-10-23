using System;
using SoySauceSDK.Runtime.Services.GDPR.Interface;

namespace SoySauceSDK.Runtime.Services.GDPR
{
    public class GdprService : IConsentService
    {
        public bool IsConsentGiven { get; private set; }

        public void Init(bool consent, Action<bool> onComplete)
        {
            IsConsentGiven = consent;
            onComplete?.Invoke(true); // send true because init succeed
        }
    }
}