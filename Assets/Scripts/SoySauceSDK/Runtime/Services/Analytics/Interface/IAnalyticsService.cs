using SoySauceSDK.Runtime.Services.Interface;

namespace SoySauceSDK.Runtime.Services.Analytics.Interface
{
    public interface IAnalyticsService : IService
    {
        void TrackEvent(string eventName);
    }
}