using System;
using SoySauceSDK.Runtime.Services.Interface;

namespace SoySauceSDK.Runtime.Services.Ads.Interface
{
    public interface IAdsService : IService
    {
        Action OnAdShown { get; set; }
        void ShowAd();
        void IncrementGamesCounter();
    }
}