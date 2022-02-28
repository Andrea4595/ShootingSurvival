namespace Game.UI.StageUpgrade
{
    public class IncreaseCredit : IUpgradeInformation
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float permanentIncreasedCredit => gameData.permanentUpgrades.increaseCredit.levels[gameData.permanentUpgradeLevel.increaseCredit].power;
        float stageIncreasedCredit => gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit];
        float nextStageIncreasedCredit => gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit + 1];
        float nextCredit => permanentIncreasedCredit + nextStageIncreasedCredit;

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.creditBonus)} {gameData.stageUpgradeLevel.increaseCredit + 1}";

        public string GetContent()
        {
            var nowCredit = permanentIncreasedCredit + stageIncreasedCredit;

            return $"{gameData.language.creditBonus} : {nowCredit * 100}% ¡æ {nextCredit * 100}%\n";
        }

        public void Upgrade()
        {
            //TODO : Credit Bonus
            gameData.stageUpgradeLevel.increaseCredit++;
        }
    }
}
