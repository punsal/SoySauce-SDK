using SoySauceSDK.Services.Interface;

namespace SoySauceSDK.Services.Analytics.Interface
{
    public interface IAnalyticsService : IService
    {
        void TrackEvent(string eventName);
    }
}