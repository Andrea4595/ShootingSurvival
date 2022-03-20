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
        TMPro.TMP_InputField _choiceCount;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseHp;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseMoveSpeed;
        [SerializeField]
        TMPro.TextMeshProUGUI _rateIncreaseCredit;

        private void Awake()
        {
            void Initialize()
            {
                void UpdateChoiceCount(string text)
                {
                    var value = ExceptionFilter.TryIntParse(text);
                    Data.GameData.instance.stageUpgrades.choiceCount = value;
                }

                _choiceCount.onEndEdit.AddListener(UpdateChoiceCount);
            }

            Initialize();
            UpdateInterface();
        }

        public void UpdateInterface()
        {
            ClearWithoutUpdate();

            var stageUpgrades = Data.GameData.instance.stageUpgrades;
            float weightSum =
                stageUpgrades.increaseHp.weight +
                stageUpgrades.increaseMoveSpeed.weight +
                stageUpgrades.increaseCredit.weight;

            var weapons = Data.GameData.instance.weapons;

            foreach (var weapon in weapons)
            {
                if (weapon.Value.forPlayer == false)
                    continue;

                weightSum += weapon.Value.weight;
            }

            _choiceCount.SetTextWithoutNotify(Data.GameData.instance.stageUpgrades.choiceCount.ToString());

            string GetRateText(float weight) => $"{Mathf.Round(weight * 10000 / weightSum) / 100}%";

            _rateIncreaseHp.text = GetRateText(stageUpgrades.increaseHp.weight);
            _rateIncreaseMoveSpeed.text = GetRateText(stageUpgrades.increaseMoveSpeed.weight);
            _rateIncreaseCredit.text = GetRateText(stageUpgrades.increaseCredit.weight);

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