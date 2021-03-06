using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseDamage : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.damage;
        string valueName => language.increaseBaseStatusText(language.damage);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseDamage;

        public string GetContent()
        {
            var current = 1;

            if (information.level < information.levels.Length - 1)
                return $"{valueContentName} : {(current + information.current.power) * 100}% → {(current + information.next.power) * 100}%";
            else
                return $"{valueContentName} : {(current + information.current.power) * 100}%";
        }

        public string GetName()
        {
            return valueName;
        }
    }
}