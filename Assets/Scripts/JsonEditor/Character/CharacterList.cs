using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class CharacterList : ItemList<CharacterSelect>
    {
        [SerializeField]
        Character _characterInformation;

        Data.Table<Data.Object.CharacterInformation> characterData => Data.GameData.instance.GetCharactersData();

        private void Awake()
        {
            UpdateInterface();
        }

        public void UpdateInterface()
        {
            ClearWithoutUpdate();

            foreach(var character in characterData.items)
                AddItemWithoutUpdate().Initialize(this, character.key);

            UpdateItems();
        }

        public void ShowCharacterInterface(int index)
        {
            var information = characterData.items[index];
            TestPlayer.instance.UpdateCharacterInformation(information.Clone());
            _characterInformation.UpdateInterface(information);
        }

        protected override void AddFromButton(CharacterSelect item)
        {
            var characters = new List<Data.Object.CharacterInformation>(characterData.items);
            var newCharacter = new Data.Object.CharacterInformation();

            characters.Add(newCharacter);
            characterData.items = characters.ToArray();
            Data.GameData.instance.CharacterDataInitialize();

            item.Initialize(this, newCharacter.key);
            ShowCharacterInterface(item.index);
        }

        protected override void Remove(CharacterSelect item)
        {
            var characters = new List<Data.Object.CharacterInformation>(characterData.items);
            var character = characters[item.index];

            characters.Remove(character);
            characterData.items = characters.ToArray();
            Data.GameData.instance.CharacterDataInitialize();

            if (_characterInformation.CheckKey(character.key) == false)
                return;

            _characterInformation.HideInterface();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = characterData.items[indexA];
            characterData.items[indexA] = characterData.items[indexB];
            characterData.items[indexB] = temp;
        }

        protected override void UpdateItems()
        {
            SaveJsonData.instance.SaveIfAuto(SaveJsonData.charactersPath, characterData);
        }
    }
}