using UnityEngine;

namespace Data
{
    public class GameData : Singleton<GameData>
    {
        [SerializeField]
        Table<Object.Character> _characterData;
        [SerializeField]
        Table<Object.Weapon> _weaponData;
        [SerializeField]
        Table<Object.Stage> _stageData;
        [SerializeField]
        UpgradeData _upgradeData;

        public Object.Character[] characters => _characterData.items;
        public Object.Weapon[] weapons => _weaponData.items;
        public Object.Stage[] stages => _stageData.items;
        public UpgradeData.StageUpgrades stageUpgrades => _upgradeData.stageUpgrades;
        public UpgradeData.PermanentUpgrades permanentUpgrades => _upgradeData.permanentUpgrades;

        void Initialize()
        {
            Initialize(this);

            _characterData.Initialize("config/characters.json");
            _weaponData.Initialize("config/weapons.json");
            _stageData.Initialize("config/stages.json");
            _upgradeData.Initialize("config/upgrades.json");
        }

        private void Awake() => Initialize();

        public Object.Character GetCharacterData(string key)
        {
            foreach(var item in characters)
            {
                if (item.key == key)
                    return item;
            }

            Debug.LogError($"there is no character named '{key}'.");
            return null;
        }

        public Object.Weapon GetWeaponData(string key)
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