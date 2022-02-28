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
            var gameData = Data.GameData.instance;

            List<string> GetChoiceKeys(int count)
            {
                List<string> allChoices = new List<string>();

                if (gameData.stageUpgradeLevel.increaseHp < gameData.stageUpgrades.increaseHp.Length)
                    allChoices.Add("increaseHp");
                if (gameData.stageUpgradeLevel.increaseMoveSpeed < gameData.stageUpgrades.increaseMoveSpeed.Length)
                    allChoices.Add("increaseMoveSpeed");
                if (gameData.stageUpgradeLevel.increaseCredit < gameData.stageUpgrades.increaseCredit.Length)
                    allChoices.Add("increaseCredit");

                var weaponInfos = gameData.weapons;
                foreach (var weaponInfo in weaponInfos)
                {
                    if (weaponInfo.forPlayer == false)
                        continue;

                    int level = gameData.stageUpgradeLevel.weapons[weaponInfo.key];

                    if (level >= weaponInfo.upgrades.Length)
                        continue;
                    
                    allChoices.Add($"w_{weaponInfo.key}");
                }

                if (allChoices.Count <= 0)
                    allChoices.Add("credit");

                allChoices.Add("heal");

                List<string> choices = new List<string>();

                for (var i = Mathf.Min(count, allChoices.Count); i > 0; i--)
                {
                    var choice = allChoices.ToArray()[Random.Range(0, allChoices.Count)];
                    allChoices.Remove(choice);
                    choices.Add(choice);
                }

                return choices;
            }

            var keys = GetChoiceKeys(count);

            foreach (var key in keys)
            {
                var choice = Instantiate(_choicePrefab);
                choice.transform.SetParent(_choiceContainer);
                choice.Initialize(key);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_choiceContainer);
        }
    }
}