using System;
using UnityEngine;

namespace SoySauceSDK.Services.Ads
{
    [Serializable]
    public class AdsServiceSettings
    {
        [SerializeField] private string adId = "";
        [SerializeField] private int secondsBetweenAds = 0;
        [SerializeField] private int gamesBetweenAds = 0;

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