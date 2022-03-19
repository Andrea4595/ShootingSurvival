using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class UpgradeList : ItemList<Upgrade>
    {
        Weapon _weapon;
        WeaponInformation.Upgrade[] _informations;

        public void Initialize(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void UpdateInterface(WeaponInformation.Upgrade[] informations)
        {
            _informations = informations;

            ClearWithoutUpdate();

            foreach (var information in informations)
                AddItemWithoutUpdate().Initialize(this, information);

            SetLevels();
        }

        void SetLevels()
        {
            for (var i = 0; i < _items.Count; i++)
                _items[i].SetLevel(i + 1);
        }

        internal void UpdateUpgrade(int index, WeaponInformation.Upgrade information)
        {
            _informations[index] = information;
            UpdateItems();
        }

        protected override void AddFromButton(Upgrade item)
        {
            var newInformation = new WeaponInformation.Upgrade();

            item.Initialize(this, newInformation);

            var list = new List<WeaponInformation.Upgrade>(_informations);
            list.Add(newInformation);
            _informations = list.ToArray();

            SetLevels();
        }

        protected override void Remove(Upgrade item)
        {
            var list = new List<WeaponInformation.Upgrade>(_informations);
            list.RemoveAt(item.index);
            _informations = list.ToArray();

            for (var i = item.index + 1; i < _items.Count; i++)
                _items[i].SetLevel(i);
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = _informations[indexA];
            _informations[indexA] = _informations[indexB];
            _informations[indexB] = temp;
            SetLevels();
        }

        protected override void UpdateItems()
        {
            _weapon.UpdateUpgrade(_informations);
        }

        internal void SelectUpgradeLevel(int level)
        {
            _weapon.SelectUpgradeLevel(level);
        }
    }
}