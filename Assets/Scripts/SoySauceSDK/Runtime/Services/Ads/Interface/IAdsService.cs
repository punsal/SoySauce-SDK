using System;
using SoySauceSDK.Services.Interface;

namespace SoySauceSDK.Services.Ads.Interface
{
    public interface IAdsService : IService
    {
        Action OnAdShown { get; set; }
        void ShowAd();
        void IncrementGamesCounter();
    }
}