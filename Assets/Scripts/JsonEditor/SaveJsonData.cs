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
        public const string stagessPath = "config/stages.json";
        public const string upgradesPath = "config/upgrades.json";
        public const string backup = ".bac";

        public void SaveIfAuto(string path, object information)
        {
            if (EditorConfig.instance.autoSave == false)
                return;
            
            Save(path, information);
        }

        public void Save(string path, object information)
        {
            File.WriteAllText(path + backup, File.ReadAllText(path));

            try
            {
                var json = JsonUtility.ToJson(information);
                File.WriteAllText(path, json);
            }
            catch
            {
                Message.Show("저장 안 됨", "콘텐츠 내용에 문제가 있어 저장할 수 없습니다.");
                File.WriteAllText(path, File.ReadAllText(path + backup));
            }
        }

        public void UpdateReferencedCharacterKey(string from, string to)
        {
            //TODO : Stage reference update
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

            //TODO : Weapon list update
        }
    }
}