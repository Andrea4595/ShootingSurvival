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

        public Dictionary<string, Object.CharacterInformation> characters = new Dictionary<string, Object.CharacterInformation>();
        public Dictionary<string, Object.WeaponInformation> weapons = new Dictionary<string, Object.WeaponInformation>();
        public Object.StageInformation[] stages => _stageData.items;
        public UpgradeInformation.StageUpgrades stageUpgrades => _upgradeData.stageUpgrades;
        public UpgradeInformation.PermanentUpgrades permanentUpgrades => _upgradeData.permanentUpgrades;
        public PlayerData playerData = new PlayerData();

        public int credit;
        public float creditBonus => 1 + stageUpgrades.increaseCredit.current + permanentUpgrades.increaseCreditBonus.current.power * stageUpgrades.increaseCredit.level;

        void FirstInitialize()
        {
            _characterData.Initialize("config/characters.json");
            _weaponData.Initialize("config/weapons.json");
            _stageData.Initialize("config/stages.json");
            _upgradeData.Initialize("config/upgrades.json");
            _languageData.Initialize("config/languages.json");

            CharacterDataInitialize();
            WeaponDataInitialize();

            playerData.Load();

            GameInitialize();
        }

        public void CharacterDataInitialize()
        {
            characters.Clear();

            foreach (var character in _characterData.items)
            {
                try
                {
                    characters.Add(character.key, character);
                }
                catch(System.ArgumentException e)
                {
                    var index = 1;
                    while (characters.ContainsKey($"{character.key}{index}"))
                        index++;

                    character.key = $"{character.key}{index}";
                    characters.Add(character.key, character);
                }
            }
        }

        public void WeaponDataInitialize()
        {
            weapons.Clear();

            foreach (var weapon in _weaponData.items)
            {
                try
                {
                    weapons.Add(weapon.key, weapon);
                }
                catch (System.ArgumentException e)
                {
                    var index = 1;
                    while (weapons.ContainsKey($"{weapon.key}{index}"))
                        index++;

                    weapon.key = $"{weapon.key}{index}";
                    weapons.Add(weapon.key, weapon);
                }
            }
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

        public Object.CharacterInformation GetCharacterData(string key) => characters[key];
        public Object.WeaponInformation GetWeaponData(string key) => weapons[key];

        public string languageKey { get; set; }
        public LanguageInformation.Language language => _languageData.GetLanguage(languageKey);

        public Table<Object.CharacterInformation> GetCharactersData() => _characterData;
        public Table<Object.WeaponInformation> GetWeaponsData() => _weaponData;
        public void SetCharacterData(Object.CharacterInformation information) => characters[information.key] = information;
        public void SetWeaponData(Object.WeaponInformation information) => weapons[information.key] = information;
    }
}