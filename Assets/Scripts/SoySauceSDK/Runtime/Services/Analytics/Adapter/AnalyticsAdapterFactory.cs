using SoySauceSDK.Services.Analytics.Adapter.Interface;

namespace SoySauceSDK.Services.Analytics.Adapter
{
    public static class AnalyticsAdapterFactory
    {
        public static IAnalyticsAdapter Create()
        {
            return new TopAnalyticsAdapter();
        }
    }
}