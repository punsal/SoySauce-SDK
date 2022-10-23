using System;

namespace SoySauceSDK.Services.Ads.Adapter.Interface
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