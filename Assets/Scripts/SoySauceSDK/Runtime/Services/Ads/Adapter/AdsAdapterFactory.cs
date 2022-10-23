using SoySauceSDK.Runtime.Services.Ads.Adapter.Interface;

namespace SoySauceSDK.Runtime.Services.Ads.Adapter
{
    public static class AdsAdapterFactory
    {
        public static IAdsAdapter Create()
        {
            IAdsAdapter adapter = new TopAdsAdapter();
            var didCreated = adapter.Create();
            if (didCreated) return adapter;

            adapter = new NullObjectAdsAdapter();
            adapter.Create();
            return adapter;
        }
    }
}