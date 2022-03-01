namespace Game.UI.StageUpgrade
{
    public class GetCredit : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;

        float creditAmount => gameData.stageUpgrades.credit.power[0];

        public string GetName() => gameData.language.credit;

        public string GetContent()
        {
            return gameData.language.GetCreditText(creditAmount.ToString());
        }

        public void Upgrade()
        {
            //TODO : get credit
        }
    }
}
