using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageUpgradeLevelList : ItemList<StageUpgradeLevel>
    {
        Data.UpgradeInformation.StageUpgrades.Upgrade _upgrade;

        internal void UpdateInterface(Data.UpgradeInformation.StageUpgrades.Upgrade upgrade)
        {
            _upgrade = upgrade;

            gameObject.SetActive(true);
            ClearWithoutUpdate();

            for (int i = 1; i < upgrade.power.Length; i++)
                AddItemWithoutUpdate().UpdateInterface(this, upgrade.power[i]);
        }

        internal void Hide() => gameObject.SetActive(false);

        protected override void AddFromButton(StageUpgradeLevel item)
        {
            var power = 1f;
            item.UpdateInterface(this, power);

            var list = new List<float>(_upgrade.power);
            list.Add(power);
            _upgrade.power = list.ToArray();
        }

        protected override void Remove(StageUpgradeLevel item)
        {
            var list = new List<float>(_upgrade.power);
            list.RemoveAt(item.index + 1);
            _upgrade.power = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB) { }

        protected override void UpdateItems()
        {
        }

        internal void UpdatePower(int index, float power)
        {
            _upgrade.power[index + 1] = power;
            SaveJsonData.instance.SaveUpgradeIfAuto();
        }
    }
}