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

        public Object.CharacterInformation[] characters => _characterData.items;
        public Object.WeaponInformation[] weapons => _weaponData.items;
        public Object.StageInformation[] stages => _stageData.items;
        public UpgradeInformation.StageUpgrades stageUpgrades => _upgradeData.stageUpgrades;
        public UpgradeInformation.PermanentUpgrades permanentUpgrades => _upgradeData.permanentUpgrades;

        [System.Serializable]
        public class PermanentUpgradeLevel
        {
            public int increaseDamage;
            public int increaseInterval;
            public int increaseRange;
            public int increaseHp;
            public int increaseMoveSpeed;
            public int increaseCredit;
            public int increaseChoiceCount;
        }

        public PermanentUpgradeLevel permanentUpgradeLevel;

        [System.Serializable]
        public class StageUpgradeLevel
        {
            public int increaseHp;
            public int increaseMoveSpeed;
            public int increaseCredit;
            public Dictionary<string, int> weapons = new Dictionary<string, int>();

            public void Initialize()
            {
                increaseHp = 0;
                increaseMoveSpeed = 0;
                increaseCredit = 0;
            }

            public void InitializeWeapons(Object.WeaponInformation[] weaponInfos)
            {
                foreach (var weaponInfo in weaponInfos)
                    weapons.Add(weaponInfo.key, -1);
            }
        }

        public StageUpgradeLevel stageUpgradeLevel;

        void FirstInitialize()
        {
            Initialize(this);

            _characterData.Initialize("config/characters.json");
            _weaponData.Initialize("config/weapons.json");
            _stageData.Initialize("config/stages.json");
            _upgradeData.Initialize("config/upgrades.json");

            GameInitialize();
        }
        
        public void GameInitialize()
        {
            void SetBaseWeaponLevel()
            {
                var baseWeapons = GetCharacterData("player").weapons;
                foreach (var baseWeapon in baseWeapons)
                    stageUpgradeLevel.weapons[baseWeapon] = 0;
            }

            stageUpgradeLevel.Initialize();
            stageUpgradeLevel.InitializeWeapons(weapons);
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
    }
}