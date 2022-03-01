using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class GameData : Singleton<GameData>
    {
        [SerializeField]
        Table<Object.CharacterInformation> _characterData;
        [SerializeField]
        Table<Object.WeaponInformation> _weaponData;
        [SerializeField]
        Table<Object.StageInformation> _stageData;
        [SerializeField]
        UpgradeInformation _upgradeData;
        [SerializeField]
        LanguageInformation _languageData;

        public Object.CharacterInformation[] characters => _characterData.items;
        public Object.WeaponInformation[] weapons => _weaponData.items;
        public Object.StageInformation[] stages => _stageData.items;
        public UpgradeInformation.StageUpgrades stageUpgrades => _upgradeData.stageUpgrades;
        public UpgradeInformation.PermanentUpgrades permanentUpgrades => _upgradeData.permanentUpgrades;

        void FirstInitialize()
        {
            Initialize(this);

            _characterData.Initialize("config/characters.json");
            _weaponData.Initialize("config/weapons.json");
            _stageData.Initialize("config/stages.json");
            _upgradeData.Initialize("config/upgrades.json");
            _languageData.Initialize("config/languages.json");
            languageKey = "kr";

            GameInitialize();
        }
        
        public void GameInitialize()
        {
            void SetBaseWeaponLevel()
            {
                var baseWeapons = GetCharacterData("player").weapons;
                foreach (var baseWeapon in baseWeapons)
                    stageUpgrades.weaponLevels[baseWeapon] = 0;
            }

            stageUpgrades.Initialize();
            stageUpgrades.InitializeWeapons(weapons);
            SetBaseWeaponLevel();
        }

        private void Awake() => FirstInitialize();

        public Object.CharacterInformation GetCharacterData(string key)
        {
            foreach(var item in characters)
            {
                if (item.key == key)
                    return item;
            }

            Debug.LogError($"there is no character named '{key}'.");
            return null;
        }

        public Object.WeaponInformation GetWeaponData(string key)
        {
            foreach (var item in weapons)
            {
                if (item.key == key)
                    return item;
            }

            Debug.LogError($"there is no weapon named '{key}'.");
            return null;
        }

        public string languageKey { get; set; }
        public LanguageInformation.Language language => _languageData.GetLanguage(languageKey);
    }
}