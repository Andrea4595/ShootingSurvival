using Data;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class IncreaseCredit : IUpgrade
    {
        GameData gameData => GameData.instance;

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => gameData.stageUpgrades.increaseCredit;

        float baseValue => 1 + gameData.permanentUpgrades.increaseCreditBonus.current.power;
        float currentUpgrade => upgrade.current;
        float nextUpgrade => upgrade.next;
        float nextValue => baseValue * (1 + nextUpgrade);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.creditBonus)} {GetLevel()}";
        string GetLevel()
        {
            if (upgrade.level + 2 >= upgrade.power.Length)
                return "MAX";

            return (upgrade.level + 1).ToString();
        }

        public string GetContent()
        {
            var nowCredit = baseValue * (1 + currentUpgrade);

            return $"{gameData.language.creditBonus} : {nowCredit * 100}% ¡æ {nextValue * 100}%\n";
        }

        public void Upgrade()
        {
            upgrade.level++;
        }
    }
}
