using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseHp : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.IncreaseStatusText(language.hp);
        string valueName => language.increaseUpgradeStatusText(language.hp);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseHp;

        public string GetContent()
        {
            var currentIncrease = GameData.instance.stageUpgrades.increaseHp.current;

            if (information.level < information.levels.Length - 1)
                return $"{valueContentName} : +{currentIncrease + information.current.power} ¡æ +{currentIncrease + information.next.power}";
            else
                return $"{valueContentName} : +{currentIncrease + information.current.power}";
        }

        public string GetName()
        {
            return valueName;
        }
    }
}