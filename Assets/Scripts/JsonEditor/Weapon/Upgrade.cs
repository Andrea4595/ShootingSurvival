using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class Upgrade : Item<Upgrade>
    {
        [SerializeField]
        FixList _fixList;
        [SerializeField]
        TMPro.TextMeshProUGUI _level;

        UpgradeList _upgradeList;
        WeaponInformation.Upgrade _information;

        public void Initialize(UpgradeList upgradeList, WeaponInformation.Upgrade information)
        {
            _upgradeList = upgradeList;
            _information = information;
            _fixList.Initialize(this, information.fixes);
        }

        public void SetLevel(int level) => _level.text = $"Level {level}";

        public void Remove()
        {
            _upgradeList.RemoveItem(this);
        }

        public void MoveUp() => _upgradeList.Swap(index, index - 1);
        public void MoveDown() => _upgradeList.Swap(index, index + 1);

        internal void UpdateFixList(WeaponInformation.Upgrade.Fix[] fixes)
        {
            _information.fixes = fixes;
            _upgradeList.UpdateUpgrade(index, _information);
        }

        public void SelectUpgradeLevel()
        {
            _upgradeList.SelectUpgradeLevel(index + 1);
        }
    }
}