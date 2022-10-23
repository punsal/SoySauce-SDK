using System;
using System.Collections;
using SoySauceSDK.Services.Ads.Adapter.Interface;
using UnityEngine;

namespace SoySauceSDK.Services.Ads.Adapter
{
    public class AdsAdapterErrorHandler : MonoBehaviour
    {
        public static Action<IAdsAdapter> OnAdapterErrorSolved;

        public void Resolve()
        {
            var adsPrefab = Resources.Load<GameObject>(SoySauceGlobals.SoySauceAdsPrefabName);
            if (adsPrefab != null)
            {
                StartCoroutine(InternalResolve(adsPrefab));
            }
        }

        private IEnumerator InternalResolve(GameObject prefab)
        {
            yield return new WaitForEndOfFrame();

            Instantiate(prefab);

            yield return new WaitForEndOfFrame();
            
            OnAdapterErrorSolved?.Invoke(AdsAdapterFactory.Create());
            Destroy(gameObject);
        }
    }
}