using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class GetCredit : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;

        int creditAmount => Mathf.RoundToInt(gameData.stageUpgrades.credit.power[0] * Data.GameData.instance.creditBonus);

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
