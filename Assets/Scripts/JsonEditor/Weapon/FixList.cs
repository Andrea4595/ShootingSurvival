using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class FixList : ItemList<Fix>
    {
        Upgrade _upgrade;
        WeaponInformation.Upgrade.Fix[] _fixes;

        public void Initialize(Upgrade upgrade, WeaponInformation.Upgrade.Fix[] fixes)
        {
            _upgrade = upgrade;
            _fixes = fixes;

            ClearWithoutUpdate();

            foreach(var fix in fixes)
                AddItemWithoutUpdate().Initialize(this, fix);
        }

        protected override void AddFromButton(Fix item)
        {
            var fix = new WeaponInformation.Upgrade.Fix();
            item.Initialize(this, fix);

            var list = new List<WeaponInformation.Upgrade.Fix>(_fixes);
            list.Add(fix);
            _fixes = list.ToArray();
        }

        protected override void Remove(Fix item)
        {
            var list = new List<WeaponInformation.Upgrade.Fix>(_fixes);
            list.RemoveAt(item.index);
            _fixes = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = _fixes[indexA];
            _fixes[indexA] = _fixes[indexB];
            _fixes[indexB] = temp;
        }

        protected override void UpdateItems()
        {
            _upgrade.UpdateFixList(_fixes);
        }

        internal void UpdateFix(int index, WeaponInformation.Upgrade.Fix fix)
        {
            _fixes[index] = fix;
            UpdateItems();
        }
    }
}