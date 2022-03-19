using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace JsonEditor
{
    public class PermanentUpgradeInterface : ItemList<PermanentUpgradeLevel>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _title;

        UpgradeInformation.PermanentUpgrades.Upgrade _information;

        internal void Show(UpgradeInformation.PermanentUpgrades.Upgrade information, string titleText)
        {
            _information = information;
            _title.text = titleText;

            gameObject.SetActive(true);

            Initialize();
        }

        void Initialize()
        {
            ClearWithoutUpdate();

            for (var i = 1; i < _information.levels.Length; i++)
                AddItemWithoutUpdate().Initialize(this, _information.levels[i]);
        }

        protected override void AddFromButton(PermanentUpgradeLevel item)
        {
            var newLevel = new UpgradeInformation.PermanentUpgrades.Upgrade.Level();
            item.Initialize(this, newLevel);

            var list = new List<UpgradeInformation.PermanentUpgrades.Upgrade.Level>(_information.levels);
            list.Add(newLevel);
            _information.levels = list.ToArray();
        }

        protected override void Remove(PermanentUpgradeLevel item)
        {
            var list = new List<UpgradeInformation.PermanentUpgrades.Upgrade.Level>(_information.levels);
            list.RemoveAt(item.index + 1);
            _information.levels = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB) { }
        protected override void UpdateItems() => SaveJsonData.instance.SaveUpgradeIfAuto();
    }
}