using System;
using UnityEngine;

namespace SoySauceSDK
{
    /// <summary>
    /// PlayerPref based database for simple implementation. Singleton
    /// </summary>
    public class SoySauceDatabase
    {
        private static readonly Lazy<SoySauceDatabase> LazyInstance = new Lazy<SoySauceDatabase>(CreateInstance);

        public static SoySauceDatabase Instance => LazyInstance.Value;

        private static SoySauceDatabase CreateInstance()
        {
            return new SoySauceDatabase();
        }

        public string Read(string key)
        {
            return PlayerPrefs.GetString(key, string.Empty);
        }

        public void Write(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}