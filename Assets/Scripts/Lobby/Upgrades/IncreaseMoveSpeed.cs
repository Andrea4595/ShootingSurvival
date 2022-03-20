using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Lobby.Upgrades
{
    public class IncreaseMoveSpeed : IUpgrade
    {
        LanguageInformation.Language language => GameData.instance.language;
        string valueContentName => language.moveSpeed;
        string valueName => language.increaseBaseStatusText(language.moveSpeed);

        public UpgradeInformation.PermanentUpgrades.Upgrade information => GameData.instance.permanentUpgrades.increaseMoveSpeed;

        public string GetContent()
        {
            var current = GameData.instance.GetCharacterInformation("player").moveSpeed;

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