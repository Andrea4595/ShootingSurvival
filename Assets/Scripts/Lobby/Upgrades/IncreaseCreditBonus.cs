using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseCreditBonus : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.IncreaseStatusText(language.creditBonus);
        string valueName => language.increaseUpgradeStatusText(language.creditBonus);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseCreditBonus;

        public string GetContent()
        {
            if (information.level < information.levels.Length - 1)
                return $"{valueContentName} : +{information.current.power * 100}% �� +{information.next.power * 100}%";
            else
                return $"{valueContentName} : +{information.current.power * 100}%";
        }

        public string GetName()
        {
            return valueName;
        }
    }
}