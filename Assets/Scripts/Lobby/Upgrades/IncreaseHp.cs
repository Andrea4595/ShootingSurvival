using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseHp : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.hp;
        string valueName => language.increaseBaseStatusText(language.hp);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseHp;

        public string GetContent()
        {
            var current = GameData.instance.GetCharacterInformation("player").maxHp;

            if (information.level < information.levels.Length - 1)
                return $"{valueContentName} : {current + information.current.power} ¡æ {current + information.next.power}";
            else
                return $"{valueContentName} : {current + information.current.power}";
        }

        public string GetName()
        {
            return valueName;
        }
    }
}