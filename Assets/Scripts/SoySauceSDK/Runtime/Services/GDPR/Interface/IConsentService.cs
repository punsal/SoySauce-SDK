using SoySauceSDK.Runtime.Services.Interface;

namespace SoySauceSDK.Runtime.Services.GDPR.Interface
{
    public interface IConsentService : IService
    {
        bool IsConsentGiven { get; }
    }
}