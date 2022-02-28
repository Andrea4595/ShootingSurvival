namespace Game.UI.StageUpgrade
{
    public class GetCredit : IUpgradeInformation
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        public string GetName() => gameData.language.credit;

        public string GetContent()
        {
            var creditAmount = gameData.stageUpgrades.credit.power[0];
            return gameData.language.GetCreditText(creditAmount.ToString());
        }

        public void Upgrade()
        {
            //TODO : get credit
        }
    }
}
