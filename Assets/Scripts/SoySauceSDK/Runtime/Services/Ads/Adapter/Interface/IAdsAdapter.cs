using System;

namespace SoySauceSDK.Runtime.Services.Ads.Adapter.Interface
{
    public interface IAdsAdapter
    {
        Action OnAdShown { get; set; }
        bool Create();
        bool Init(bool consent, string adId);
        bool IsAdReady();
        void ShowAd();
    }
}