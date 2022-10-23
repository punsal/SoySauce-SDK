namespace SoySauceSDK.Services.Analytics.Adapter.Interface
{
    public interface IAnalyticsAdapter
    {
        void Init(bool consent);
        void TrackEvent(string eventName);
    }
}