using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class WeaponList : ItemList<WeaponSelect>
    {
        [SerializeField]
        Weapon _weaponInformation;

        Data.Table<Data.Object.WeaponInformation> weaponData => Data.GameData.instance.GetWeaponsData();

        private void Awake()
        {
            UpdateInterface();
        }

        public void UpdateInterface()
        {
            ClearWithoutUpdate();

            foreach (var character in weaponData.items)
                AddItemWithoutUpdate().Initialize(this, character.key);
        }

        public void ShowWeaponInterface(int index)
        {
            TestPlayer.instance.UpdateWeaponInformation(weaponData.items[index].Clone());
            _weaponInformation.UpdateInterface(weaponData.items[index]);
        }

        protected override void AddFromButton(WeaponSelect item)
        {
            var weapons = new List<Data.Object.WeaponInformation>(weaponData.items);
            var newWeapon = new Data.Object.WeaponInformation();

            weapons.Add(newWeapon);
            weaponData.items = weapons.ToArray();
            Data.GameData.instance.WeaponDataInitialize();

            item.Initialize(this, newWeapon.key);
            ShowWeaponInterface(item.index);
        }

        protected override void Remove(WeaponSelect item)
        {
            var weapons = new List<Data.Object.WeaponInformation>(weaponData.items);
            var weapon = weapons[item.index];

            weapons.Remove(weapon);
            weaponData.items = weapons.ToArray();
            Data.GameData.instance.WeaponDataInitialize();

            if (_weaponInformation.CheckKey(weapon.key) == false)
                return;

            _weaponInformation.HideInterface();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = weaponData.items[indexA];
            weaponData.items[indexA] = weaponData.items[indexB];
            weaponData.items[indexB] = temp;
        }

        protected override void UpdateItems()
        {
            SaveJsonData.instance.SaveWeaponIfAuto();
        }
    }
}