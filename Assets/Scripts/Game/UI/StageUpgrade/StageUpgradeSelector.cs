using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class StageUpgradeSelector : Singleton<StageUpgradeSelector>
    {
        [SerializeField]
        GameObject _canvas;
        [SerializeField]
        RectTransform _choiceContainer;

        [SerializeField]
        StageUpgrade.StageUpgradeChoice _choicePrefab;

        private void Awake()
        {
            Initialize(this);
        }

        public void Show()
        {
            _canvas.SetActive(true);

            var choiceCount = GetChoiceCount();
            ShowRandomChoices(choiceCount);

            Time.instance.Fade(0, 0.5f);
        }

        public void Close()
        {
            Clear();

            _canvas.SetActive(false);

            Time.instance.Fade(1, 1f);
        }

        void Clear()
        {
            for (int i = _choiceContainer.childCount - 1; i >= 0; i--)
                Destroy(_choiceContainer.GetChild(i).gameObject);
        }

        int GetChoiceCount()
        {
            var gameData = Data.GameData.instance;
            var baseCount = gameData.stageUpgrades.choiceCount;
            var additionalCount = (int)gameData.permanentUpgrades.increaseChoiceCount.current.power;

            return baseCount + additionalCount;
        }

        void ShowRandomChoices(int count)
        {
            var keys = GetChoiceKeys(count);

            foreach (var key in keys)
            {
                var choice = Instantiate(_choicePrefab);
                choice.transform.SetParent(_choiceContainer);
                choice.Initialize(key);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_choiceContainer);
        }

        List<StageUpgrade.IUpgrade> GetChoiceKeys(int count)
        {
            var stageUpgrades = Data.GameData.instance.stageUpgrades;

            WeightedRandom<StageUpgrade.IUpgrade> allChoices = new WeightedRandom<StageUpgrade.IUpgrade>();

            if (stageUpgrades.increaseHp.level < stageUpgrades.increaseHp.power.Length - 1)
                allChoices.Add(new StageUpgrade.IncreaseHp(), stageUpgrades.increaseHp.weight);
            if (stageUpgrades.increaseMoveSpeed.level < stageUpgrades.increaseMoveSpeed.power.Length - 1)
                allChoices.Add(new StageUpgrade.IncreaseMoveSpeed(), stageUpgrades.increaseMoveSpeed.weight);
            if (stageUpgrades.increaseCredit.level < stageUpgrades.increaseCredit.power.Length - 1)
                allChoices.Add(new StageUpgrade.IncreaseCredit(), stageUpgrades.increaseCredit.weight);

            var weaponInfos = Data.GameData.instance.weapons;
            var weaponUpgradeWeight = stageUpgrades.weaponUpgradesWeight / weaponInfos.Length;
            foreach (var weaponInfo in weaponInfos)
            {
                if (weaponInfo.forPlayer == false)
                    continue;

                int level = stageUpgrades.weaponLevels[weaponInfo.key];

                if (level >= weaponInfo.upgrades.Length)
                    continue;

                allChoices.Add(new StageUpgrade.UpgradeWeapon(weaponInfo), weaponUpgradeWeight);
            }

            if (allChoices.Count <= 0)
                allChoices.Add(new StageUpgrade.GetCredit(), stageUpgrades.credit.weight);

            allChoices.Add(new StageUpgrade.GetHeal(), stageUpgrades.heal.weight);

            List<StageUpgrade.IUpgrade> choices = new List<StageUpgrade.IUpgrade>();

            for (var i = Mathf.Min(count, allChoices.Count); i > 0; i--)
                choices.Add(allChoices.TakeOne());

            return choices;
        }
    }
}