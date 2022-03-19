using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JsonEditor
{
    public class SaveJsonData : Singleton<SaveJsonData>
    {
        public const string charactersPath = "config/characters.json";
        public const string weaponsPath = "config/weapons.json";
        public const string stagesPath = "config/stages.json";
        public const string upgradesPath = "config/upgrades.json";
        public const string backup = ".bac";

        [SerializeField]
        UnityEngine.UI.Toggle _autoSave;

        private void Awake()
        {
            void Initialize() => _autoSave.onValueChanged.AddListener(SetAutoSave);

            Initialize();
        }

        public void SaveCharacterIfAuto()
        {
            if (EditorConfig.instance.autoSave == false)
                return;

            SaveCharacterData();
        }

        public void SaveWeaponIfAuto()
        {
            if (EditorConfig.instance.autoSave == false)
                return;

            SaveWeaponData();
        }

        public void SaveStageIfAuto()
        {
            if (EditorConfig.instance.autoSave == false)
                return;

            SaveStageData();
        }

        public void SaveUpgradeIfAuto()
        {
            if (EditorConfig.instance.autoSave == false)
                return;

            SaveUpgradeData();
        }

        public bool Save(string path, object information)
        {
            if (Compare(path, information))
                return false;

            PushBackup(path);
            WriteBackup(path);

            try
            {
                var json = JsonUtility.ToJson(information, true);
                File.WriteAllText(path, json);

                return true;
            }
            catch
            {
                Message.Show("저장 안 됨", $"{path}에 저장 될 콘텐츠 내용에 문제가 있어 저장할 수 없습니다.");
                File.WriteAllText(path, File.ReadAllText(path + backup));

                return false;
            }
        }

        bool Compare(string path, object information)
        {
            var json = JsonUtility.ToJson(information, true);
            var file = File.ReadAllText(path);

            return json.CompareTo(file) == 0;
        }

        void PushBackup(string path)
        {
            var index = 0;
            var filePathList = new Stack<string>();
            
            while (true)
            {
                var fileName = $"{path}{backup}{index++}";

                if (File.Exists(fileName) == false)
                    break;
                
                filePathList.Push(fileName);
            }

            while (filePathList.Count > 0)
            {
                var filePath = filePathList.Pop();

                var fileName = $"{path}{backup}{--index}";
                var file = File.ReadAllText(filePath);
                File.WriteAllText(fileName, file);
            }
        }

        void WriteBackup(string path)
        {
            var fileName = $"{path}{backup}{0}";
            File.WriteAllText(fileName, File.ReadAllText(path));
        }

        public bool SaveCharacterData() => Save(charactersPath, Data.GameData.instance.GetCharactersData());
        public bool SaveWeaponData() => Save(weaponsPath, Data.GameData.instance.GetWeaponsData());
        public bool SaveStageData() => Save(stagesPath, Data.GameData.instance.GetStagesData());
        public bool SaveUpgradeData() => Save(upgradesPath, Data.GameData.instance.GetUpgradeData());

        public void SaveAll()
        {
            var log = "";
            if (SaveCharacterData())
                log += charactersPath;

            if (SaveWeaponData())
            {
                if (log.Length != 0)
                    log += ", ";
                log += weaponsPath;
            }

            if (SaveStageData())
            {
                if (log.Length != 0)
                    log += ", ";
                log += stagesPath;
            }

            if (SaveUpgradeData())
            {
                if (log.Length != 0)
                    log += ", ";
                log += upgradesPath;
            }

            if (log.Length <= 0)
                return;

            //Todo : 언어
            Message.Show("저장", $"{log} 항목이 저장되었습니다.");
        }

        public void SetAutoSave(bool autoSave) => EditorConfig.instance.autoSave = autoSave;

        public void UpdateReferencedCharacterKey(string from, string to)
        {
            var stageData = Data.GameData.instance.GetStagesData();
            foreach (var stage in stageData.items)
                foreach (var group in stage.groups)
                    foreach (var spawn in group.spawns)
                        if (spawn.key.CompareTo(from) == 0)
                            spawn.key = to;
        }

        public void UpdateReferencedWeaponKey(string from, string to)
        {
            var charactersData = Data.GameData.instance.GetCharactersData();

            foreach (var character in charactersData.items)
            {
                for (int i = 0; i < character.weapons.Length; i++)
                {
                    if (character.weapons[i].CompareTo(from) != 0)
                        continue;

                    character.weapons[i] = to;
                }
            }

            CharacterWeapon.InitializeAllDropDownMenu();
        }
    }
}