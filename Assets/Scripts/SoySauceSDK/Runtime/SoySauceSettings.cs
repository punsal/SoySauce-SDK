using SoySauceSDK.Runtime.Services.Ads;
using UnityEngine;

namespace SoySauceSDK.Runtime
{
    /// <summary>
    ///     This is an example implementation for global settings. There are lots of open areas for misuse.
    /// </summary>
    [CreateAssetMenu(fileName = SoySauceGlobals.SoySauceSettingsAssetName, menuName = "Soy/Settings", order = 0)]
    public class SoySauceSettings : ScriptableObject
    {
        [Header("Ads")] [SerializeField] private AdsServiceSettings adsServiceSettings;

        public AdsServiceSettings AdsServiceSettings => adsServiceSettings;

        public void ResetSettings()
        {
            adsServiceSettings = new AdsServiceSettings();
        }
    }
}