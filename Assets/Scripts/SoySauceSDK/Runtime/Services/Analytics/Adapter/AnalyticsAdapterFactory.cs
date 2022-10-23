using SoySauceSDK.Runtime.Services.Analytics.Adapter.Interface;

namespace SoySauceSDK.Runtime.Services.Analytics.Adapter
{
    public static class AnalyticsAdapterFactory
    {
        public static IAnalyticsAdapter Create()
        {
            return new TopAnalyticsAdapter();
        }
    }
}