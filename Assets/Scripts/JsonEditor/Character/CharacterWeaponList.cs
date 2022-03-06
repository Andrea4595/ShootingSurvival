using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class CharacterWeaponList : ItemList<CharacterWeapon>
    {
        Character _character;
        string[] _weapons;

        public void Initialize(Character character)
        {
            _character = character;
        }

        public void UpdateInterface(string[] weapons)
        {
            _weapons = weapons;

            ClearWithoutUpdate();

            for (var i = 0; i < weapons.Length; i++)
                AddItemWithoutUpdate().Initialize(this, weapons[i]);

            UpdateItems();
        }

        public void UpdateIndex(int index, string key)
        {
            _weapons[index] = key;
            UpdateItems();
        }

        protected override void AddFromButton(CharacterWeapon item)
        {
            string firstWeaponKey = "";

            if (Data.GameData.instance.weapons.Count > 0)
                firstWeaponKey = Data.GameData.instance.GetWeaponsData().items[0].key;

            item.Initialize(this, firstWeaponKey);

            var list = new List<string>(_weapons);
            list.Add(firstWeaponKey);
            _weapons = list.ToArray();
        }

        protected override void Remove(CharacterWeapon item)
        {
            var list = new List<string>(_weapons);
            list.RemoveAt(item.index);
            _weapons = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = _weapons[indexA];
            _weapons[indexA] = _weapons[indexB];
            _weapons[indexB] = temp;
        }

        protected override void UpdateItems()
        {
            _character.UpdateWeapon(_weapons);
        }
    }
}