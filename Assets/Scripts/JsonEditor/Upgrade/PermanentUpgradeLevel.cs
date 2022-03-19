using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class PermanentUpgradeLevel : Item<PermanentUpgradeLevel>
    {
        [SerializeField]
        TMPro.TMP_InputField _power;

        [SerializeField]
        TMPro.TMP_InputField _cost;

        PermanentUpgradeInterface _upgradeInterface;
        Data.UpgradeInformation.PermanentUpgrades.Upgrade.Level _level;

        public void Initialize(PermanentUpgradeInterface upgradeInterface, Data.UpgradeInformation.PermanentUpgrades.Upgrade.Level level)
        {
            _upgradeInterface = upgradeInterface;
            _level = level;

            _power.SetTextWithoutNotify(level.power.ToString());
            _cost.SetTextWithoutNotify(level.cost.ToString());
        }

        public void UpdatePower(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _level.power = value;

            UpdateLevel();
        }

        public void UpdateCost(string text)
        {
            var value = ExceptionFilter.TryIntParse(text);
            _level.cost = value;

            UpdateLevel();
        }

        void UpdateLevel() => SaveJsonData.instance.SaveUpgradeIfAuto();

        public void Remove() => _upgradeInterface.RemoveItem(this);
    }
}