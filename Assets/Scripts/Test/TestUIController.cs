using System;
using SoySauceSDK;
using UnityEngine;
using UnityEngine.UI;


namespace Test
{
    public class TestUIController : MonoBehaviour
    {
        public Button showAdButton;

        public Button startGameButton;

        public Button endGameButton; 

        private void Awake()
        {
            showAdButton.onClick.AddListener(ShowAdClick);
            startGameButton.onClick.AddListener(StartGameClick);
            endGameButton.onClick.AddListener(EndGameClick);
            
            SoySauce.SetAdDisplayConditions(60, 3);
            // Init SDK initially.
            SoySauce.Init();
        }

        private void ShowAdClick()
        {
            SoySauce.ShowAd(); 
        }

        private void StartGameClick()
        {
            SoySauce.StartGame(); 
        }

        private void EndGameClick()
        {
            SoySauce.EndGame(); 
        }
    }
}