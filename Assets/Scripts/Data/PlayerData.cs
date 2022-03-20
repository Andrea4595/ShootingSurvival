using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data
{
    public class PlayerData
    {
        [System.Serializable]
        public class SaveData
        {
            [System.Serializable]
            public class UpgradesLevel
            {
                public int increaseHp;
                public int increaseDamage;
                public int increaseMoveSpeed;
                public int increaseCreditBonus;
                public int increaseChoiceCount;
            }

            public int credit;
            public UpgradesLevel upgradesLevel = new UpgradesLevel();
            public string language = "kr";
        }

        SaveData _saveData = new SaveData();

        const string _path = "config/save.json";

        public void Load()
        {
            JsonParser.GetObject(_path, _saveData);
            Initialize();
        }

        public void Save()
        {
            UpdateSaveData();

            var jsonText = JsonUtility.ToJson(_saveData, true);
            File.WriteAllText(_path, jsonText);
        }

        void Initialize()
        {
            var gameData = GameData.instance;

            gameData.credit = _saveData.credit;
            gameData.languageKey = _saveData.language;
            gameData.permanentUpgrades.increaseHp.level = _saveData.upgradesLevel.increaseHp;
            gameData.permanentUpgrades.increaseDamage.level = _saveData.upgradesLevel.increaseDamage;
            gameData.permanentUpgrades.increaseMoveSpeed.level = _saveData.upgradesLevel.increaseMoveSpeed;
            gameData.permanentUpgrades.increaseCreditBonus.level = _saveData.upgradesLevel.increaseCreditBonus;
            gameData.permanentUpgrades.increaseChoiceCount.level = _saveData.upgradesLevel.increaseChoiceCount;
        }

        void UpdateSaveData()
        {
            var gameData = GameData.instance;

            _saveData.credit = gameData.credit;
            _saveData.language = gameData.languageKey;
            _saveData.upgradesLevel.increaseHp = gameData.permanentUpgrades.increaseHp.level;
            _saveData.upgradesLevel.increaseDamage = gameData.permanentUpgrades.increaseDamage.level;
            _saveData.upgradesLevel.increaseMoveSpeed = gameData.permanentUpgrades.increaseMoveSpeed.level;
            _saveData.upgradesLevel.increaseCreditBonus = gameData.permanentUpgrades.increaseCreditBonus.level;
            _saveData.upgradesLevel.increaseChoiceCount = gameData.permanentUpgrades.increaseChoiceCount.level;
        }

        public void ClearSave()
        {
            _saveData = new SaveData();
            var jsonText = JsonUtility.ToJson(_saveData);
            File.WriteAllText(_path, jsonText);

            Load();
        }
    }
}