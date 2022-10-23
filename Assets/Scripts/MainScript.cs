using SoySauceSDK.Runtime;
using UnityEngine;

/// <summary>
///     This was already implemented. I used it.
/// </summary>
public class MainScript : MonoBehaviour
{
    public void AcceptGPDR()
    {
        HideGPDRPanel();
        SoySauce.SetConsent(true);
        SoySauce.StartGame();
    }

    public void DeniedGPDR()
    {
        HideGPDRPanel();
        SoySauce.SetConsent(false);
        SoySauce.StartGame();
    }

    public void ShowAd()
    {
        SoySauce.ShowAd();
    }

    private void HideGPDRPanel()
    {
        // I didn't like this but still keeping it.
        GameObject.Find(GameObjects.GPDRPanel).SetActive(false);
    }
}