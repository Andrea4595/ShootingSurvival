using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class CharacterWeapon : Item<CharacterWeapon>
    {
        [SerializeField]
        TMPro.TMP_Dropdown _keyDropDown;

        CharacterWeaponList _list;
        string _key;

        public void Initialize(CharacterWeaponList list, string key)
        {
            _list = list;
            _key = key;

            _keyDropDown.onValueChanged.AddListener(UpdateWeaponIndex);

            InitializeDropDownMenu();
            _keyDropDown.SetValueWithoutNotify(GetWeaponIndex(key));
        }

        void InitializeDropDownMenu()
        {
            var weapons = Data.GameData.instance.GetWeaponsData();
            var weaponList = new List<string>();

            foreach (var weapon in weapons.items)
                weaponList.Add(weapon.key);

            _keyDropDown.ClearOptions();
            _keyDropDown.AddOptions(weaponList);
        }

        public void UpdateWeaponIndex(int weaponIndex)
        {
            _key = _keyDropDown.options[weaponIndex].text;
            _list.UpdateIndex(index, _key);
        }

        public void Remove() => _list.RemoveItem(this);

        int GetWeaponIndex(string key)
        {
            var weapons = Data.GameData.instance.GetWeaponsData();

            for (var i = 0; i < weapons.items.Length; i++)
            {
                if (weapons.items[i].key.CompareTo(key) != 0)
                    continue;

                return i;
            }

            return -1;
        }

        public static void InitializeAllDropDownMenu()
        {
            var characterWeapons = FindObjectsOfType<CharacterWeapon>();
            foreach (var characterWeapon in characterWeapons)
                characterWeapon.Initialize(characterWeapon._list, characterWeapon._key);
        }
    }
}