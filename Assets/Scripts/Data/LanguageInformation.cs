using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class LanguageInformation
    {
        [System.Serializable]
        public class Language
        {
            public string key;
            public string increaseStatus;
            public string IncreaseStatusText(string value) => increaseStatus.Replace("[0]", value);
            public string hp;
            public string moveSpeed;
            public string credit;
            public string creditBonus;
            public string getCredit;
            public string GetCreditText(string value) => getCredit.Replace("[0]", value);
            public string heal;
            public string healInformation;
            public string HealInformationText(string value) => healInformation.Replace("[0]", value);
            public string weapon_projectile_maxHp;
            public string weapon_projectile_damage;
            public string weapon_projectile_range;
            public string weapon_projectile_speed;
            public string weapon_projectile_scale;
            public string weapon_projectile_homming;
            public string weapon_projectile_lifetime;
            public string weapon_fireCount;
            public string weapon_angleRange;
            public string weapon_continuousCount;
            public string weapon_interval;

            [System.Serializable]
            public class Weapon
            {
                public string key;
                public string name;
                public string information;
            }

            public Weapon[] weapons;

            Dictionary<string, Weapon> _weapons = new Dictionary<string, Weapon>();

            public void Initialize()
            {
                foreach(var weapon in weapons)
                    _weapons.Add(weapon.key, weapon);
            }

            public bool GetWeapon(string key, out Weapon weapon) => _weapons.TryGetValue(key, out weapon);
        }

        public string[] languageList;
        public Language[] languages;

        Dictionary<string, Language> _languages = new Dictionary<string, Language>();

        public void Initialize(string path)
        {
            JsonParser.GetObject(path, this);

            foreach (var language in languages)
            {
                language.Initialize();
                _languages.Add(language.key, language);
            }
        }

        public Language GetLanguage(string key)
        {
            Language language;

            if (_languages.TryGetValue(key, out language) == false)
                Debug.LogError($"no language named {key}");

            return language;
        }
    }
}