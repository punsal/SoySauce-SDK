using System;
using SoySauceSDK.Services.Ads.Adapter;
using SoySauceSDK.Services.Ads.Adapter.Interface;
using SoySauceSDK.Services.Ads.Interface;
using UnityEngine;

namespace SoySauceSDK.Services.Ads
{
    public class AdsService : IAdsService
    {
        private readonly AdsServiceSettings _adsServiceSettings;

        private bool _currentConsent;
        private DateTime _lastShownAdDateTime = new DateTime(0);
        private int _gamesPlayed = 0;

        public Action OnAdShown { get; set; }

        private IAdsAdapter _adapter;
        
        public AdsService(AdsServiceSettings adsServiceSettings)
        {
            _adsServiceSettings = adsServiceSettings;
            _adapter = AdsAdapterFactory.Create();
            AdsAdapterErrorHandler.OnAdapterErrorSolved += OnAdapterErrorSolved;
        }

        public void Init(bool consent, Action<bool> onComplete)
        {
            var didSucceed = InitAdapter(consent);

            onComplete?.Invoke(didSucceed);
        }

        private bool InitAdapter(bool consent)
        {
            _currentConsent = consent;

            var didSucceed = _adapter.Init(consent, _adsServiceSettings.AdId);

            if (didSucceed)
            {
                _adapter.OnAdShown += () => OnAdShown?.Invoke();
            }

            return didSucceed;
        }

        private void OnAdapterErrorSolved(IAdsAdapter adsAdapter)
        {
            _adapter = adsAdapter;

            var didSucceed = InitAdapter(_currentConsent);
            if (didSucceed)
            {
                Debug.Log("AdsAdapter error is solved.");
            }
        }
        
        public void ShowAd()
        {
            if (!_adapter.IsAdReady())
            {
                Debug.Log("Ad is not ready to show");
                return;
            }
            
            if(_gamesPlayed < _adsServiceSettings.GamesBetweenAds)
            {
                Debug.Log($"Ad is not shown, number of games played is not matched. ({_gamesPlayed}/{_adsServiceSettings.GamesBetweenAds})");
                return;
            }

            var betweenLastSeenAd = (DateTime.Now - _lastShownAdDateTime).TotalSeconds;
            if (betweenLastSeenAd < _adsServiceSettings.SecondsBetweenAds)
            {
                Debug.Log($"Ad is not shown, player has already seen an ad {betweenLastSeenAd}secs ago.");
                return;
            }
            
            Debug.Log("Ad is shown.");
            _adapter.ShowAd();
            _lastShownAdDateTime = DateTime.Now;
            _gamesPlayed = 0;
        }

        public void IncrementGamesCounter() => _gamesPlayed++;
    }
    
    
}