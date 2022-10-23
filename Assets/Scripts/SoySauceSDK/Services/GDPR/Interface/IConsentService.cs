using SoySauceSDK.Services.Interface;

namespace SoySauceSDK.Services.GDPR.Interface
{
    public interface IConsentService : IService
    {
        bool IsConsentGiven { get; }
    }
}