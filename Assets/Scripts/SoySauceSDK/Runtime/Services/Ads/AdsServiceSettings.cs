using System;
using UnityEngine;

namespace SoySauceSDK.Runtime.Services.Ads
{
    [Serializable]
    public class AdsServiceSettings
    {
        [SerializeField] private string adId = "";
        [SerializeField] private int secondsBetweenAds;
        [SerializeField] private int gamesBetweenAds;

        public string AdId => adId;

        public int SecondsBetweenAds
        {
            get => secondsBetweenAds;
            set => secondsBetweenAds = value;
        }

        public int GamesBetweenAds
        {
            get => gamesBetweenAds;
            set => gamesBetweenAds = value;
        }
    }
}