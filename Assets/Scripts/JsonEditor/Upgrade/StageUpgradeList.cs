using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageUpgradeList : ItemList<StageWeaponUpgrade>
    {
        [SerializeField]
        StageUpgradeInterface _upgradeInterface;

        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseHp;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseMoveSpeed;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseCredit;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateHeal;

        private void Awake() => UpdateInterface();

        public void UpdateInterface()
        {
            _upgradeInterface.Hide();
            ClearWithoutUpdate();

            var stageUpgrades = Data.GameData.instance.stageUpgrades;
            float weightSum =
                stageUpgrades.increaseHp.weight +
                stageUpgrades.increaseMoveSpeed.weight +
                stageUpgrades.increaseCredit.weight +
                stageUpgrades.heal.weight;

            var weapons = Data.GameData.instance.weapons;

            foreach (var weapon in weapons)
            {
                if (weapon.Value.forPlayer == false)
                    continue;

                weightSum += weapon.Value.weight;
            }

            string GetRateText(float weight) => $"{Mathf.Round(weight * 10000 / weightSum) / 100}%";

            _rateIncreaseHp.text = GetRateText(stageUpgrades.increaseHp.weight);
            _rateIncreaseMoveSpeed.text = GetRateText(stageUpgrades.increaseMoveSpeed.weight);
            _rateIncreaseCredit.text = GetRateText(stageUpgrades.increaseCredit.weight);
            _rateHeal.text = GetRateText(stageUpgrades.heal.weight);

            foreach (var weapon in weapons)
            {
                if (weapon.Value.forPlayer == false)
                    continue;

                AddItemWithoutUpdate().Initialize(this, weapon.Value, GetRateText(weapon.Value.weight));
            }
        }

        protected override void AddFromButton(StageWeaponUpgrade item) { }
        protected override void Remove(StageWeaponUpgrade item) { }
        protected override void SwapItems(int indexA, int indexB) { }

        protected override void UpdateItems()
        {
            SaveJsonData.instance.SaveWeaponIfAuto();
            SaveJsonData.instance.SaveUpgradeIfAuto();
        }

        public void ShowIncreaseHp()
        {
            _upgradeInterface.UpdateInterface(Data.GameData.instance.stageUpgrades.increaseHp, "Increase Hp");
        }

        public void ShowIncreaseMoveSpeed()
        {
            _upgradeInterface.UpdateInterface(Data.GameData.instance.stageUpgrades.increaseMoveSpeed, "Increase MoveSpeed");
        }

        public void ShowIncreaseCredit()
        {
            _upgradeInterface.UpdateInterface(Data.GameData.instance.stageUpgrades.increaseCredit, "Increase Credit");
        }

        public void ShowHeal()
        {
            _upgradeInterface.UpdateInterface(Data.GameData.instance.stageUpgrades.heal, "Heal", true);
        }

        public void ShowCredit()
        {
            _upgradeInterface.UpdateInterface(Data.GameData.instance.stageUpgrades.credit, "Credit", true);
        }

        public void ShowWeaponWeight(Data.Object.WeaponInformation weapon)
        {
            _upgradeInterface.UpdateInterface(weapon, $"Weapon: {weapon.key}");
        }
    }
}