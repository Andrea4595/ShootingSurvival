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
        StageUpgradeChoice _choicePrefab;

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
            var additionalCount = (int)gameData.permanentUpgrades.increaseChoiceCount[gameData.permanentUpgradeLevel.increaseChoiceCount].power;

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

        List<string> GetChoiceKeys(int count)
        {
            var stageUpgrades = Data.GameData.instance.stageUpgrades;
            var stageUpgradeLevel = Data.GameData.instance.stageUpgradeLevel;

            WeightedRandom<string> allChoices = new WeightedRandom<string>();

            if (stageUpgradeLevel.increaseHp < stageUpgrades.increaseHp.power.Length - 1)
                allChoices.Add("increaseHp", stageUpgrades.increaseHp.weight);
            if (stageUpgradeLevel.increaseMoveSpeed < stageUpgrades.increaseMoveSpeed.power.Length - 1)
                allChoices.Add("increaseMoveSpeed", stageUpgrades.increaseMoveSpeed.weight);
            if (stageUpgradeLevel.increaseCredit < stageUpgrades.increaseCredit.power.Length - 1)
                allChoices.Add("increaseCredit", stageUpgrades.increaseCredit.weight);

            var weaponInfos = Data.GameData.instance.weapons;
            var weaponUpgradeWeight = stageUpgrades.weaponUpgradesWeight / weaponInfos.Length;
            foreach (var weaponInfo in weaponInfos)
            {
                if (weaponInfo.forPlayer == false)
                    continue;

                int level = stageUpgradeLevel.weapons[weaponInfo.key];

                if (level >= weaponInfo.upgrades.Length)
                    continue;

                allChoices.Add($"w_{weaponInfo.key}", weaponUpgradeWeight);
            }

            if (allChoices.Count <= 0)
                allChoices.Add("credit", stageUpgrades.credit.weight);

            allChoices.Add("heal", stageUpgrades.heal.weight);

            List<string> choices = new List<string>();

            for (var i = Mathf.Min(count, allChoices.Count); i > 0; i--)
                choices.Add(allChoices.TakeOne());

            return choices;
        }
    }
}