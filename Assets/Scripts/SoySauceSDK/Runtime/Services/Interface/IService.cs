using System;

namespace SoySauceSDK.Runtime.Services.Interface
{
    public interface IService
    {
        void Init(bool consent, Action<bool> onComplete);
    }
}