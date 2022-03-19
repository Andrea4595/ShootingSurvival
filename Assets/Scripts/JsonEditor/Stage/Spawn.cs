using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class Spawn : Item<Spawn>
    {
        [SerializeField]
        Image _sprite;
        [SerializeField]
        TMPro.TMP_Dropdown _key;
        [SerializeField]
        TMPro.TMP_InputField _count;

        SpawnList _spawnList;
        StageInformation.Group.Spawn _spawn;

        private void Awake() => Initialize();

        void Initialize()
        {
            _key.onValueChanged.AddListener(UpdateKey);
            _count.onEndEdit.AddListener(UpdateCount);
        }

        internal void UpdateInterface(SpawnList spawnList, StageInformation.Group.Spawn spawn)
        {
            _spawnList = spawnList;
            _spawn = spawn;

            var characterInformation = Data.GameData.instance.GetCharacterInformation(spawn.key);
            UpdateSprite(characterInformation);

            InitializeKeyDropdownOptions();
            _key.SetValueWithoutNotify(GetCharacterIndex(spawn.key));

            _count.SetTextWithoutNotify(spawn.count.ToString());
        }

        void InitializeKeyDropdownOptions()
        {
            _key.ClearOptions();

            var characters = Data.GameData.instance.characters;
            var list = new List<TMPro.TMP_Dropdown.OptionData>();

            foreach(var character in characters)
            {
                var characterInformation = Data.GameData.instance.GetCharacterInformation(character.Key);
                var option = new TMPro.TMP_Dropdown.OptionData(characterInformation.key, characterInformation.GetSprite());
                list.Add(option);
            }

            _key.AddOptions(list);
        }

        int GetCharacterIndex(string key)
        {
            var characters = Data.GameData.instance.GetCharactersData();

            for (var i = 0; i < characters.items.Length; i++)
                if (characters.items[i].key.CompareTo(key) == 0)
                    return i;

            return -1;
        }

        void UpdateSprite(CharacterInformation information)
        {
            _sprite.sprite = information.GetSprite();
            _sprite.color = information.GetColor();
        }

        void UpdateKey(int index)
        {
            var character = Data.GameData.instance.GetCharactersData().items[index];
            _spawn.key = character.key;

            UpdateSprite(character);
            UpdateInformation();
        }

        void UpdateCount(string text)
        {
            var value = ExceptionFilter.TryIntParse(text);
            _spawn.count = value;

            UpdateInformation();
        }

        public void Remove()
        {
            _spawnList.RemoveItem(this);
        }

        void UpdateInformation()
        {
            _spawnList.UpdateWave();
            SaveJsonData.instance.SaveStageIfAuto();
        }
    }
}