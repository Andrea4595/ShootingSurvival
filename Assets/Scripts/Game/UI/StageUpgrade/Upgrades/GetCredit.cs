using Data;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class GetCredit : IUpgrade
    {
        GameData gameData => GameData.instance;

        int creditAmount => Mathf.RoundToInt(gameData.stageUpgrades.credit.power[0] * gameData.creditBonus);

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => null;

        public string GetName() => gameData.language.credit;

        public string GetContent()
        {
            return gameData.language.GetCreditText(creditAmount.ToString());
        }

        public void Upgrade()
        {
            StageSpawner.instance.creditReward += creditAmount;
        }
    }
}
