using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseChoiceCount : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.choiceCount;
        string valueName => language.IncreaseStatusText(valueContentName);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseChoiceCount;

        public string GetContent()
        {
            var baseChoice = GameData.instance.stageUpgrades.choiceCount;

            if (information.level < information.levels.Length - 1)
                return $"{valueContentName} : {baseChoice + information.current.power} ¡æ {baseChoice + information.next.power}";
            else
                return $"{valueContentName} : {baseChoice + information.current.power}";
        }

        public string GetName()
        {
            return valueName;
        }
    }
}