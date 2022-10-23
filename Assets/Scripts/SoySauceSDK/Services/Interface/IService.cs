using System;

namespace SoySauceSDK.Services.Interface
{
    public interface IService
    {
        void Init(bool consent, Action<bool> onComplete);
    }
}