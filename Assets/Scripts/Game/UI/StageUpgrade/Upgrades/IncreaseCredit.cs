namespace Game.UI.StageUpgrade
{
    public class IncreaseCredit : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;

        float baseCreditBonus => 1 + gameData.permanentUpgrades.increaseCreditBonus.current.power;
        float stageIncreasedCredit => gameData.stageUpgrades.increaseCredit.current;
        float nextStageIncreasedCredit => gameData.stageUpgrades.increaseCredit.next;
        float nextCredit => baseCreditBonus * (1 + nextStageIncreasedCredit);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.creditBonus)} {gameData.stageUpgrades.increaseCredit.level + 1}";

        public string GetContent()
        {
            var nowCredit = baseCreditBonus * (1 + stageIncreasedCredit);

            return $"{gameData.language.creditBonus} : {nowCredit * 100}% ¡æ {nextCredit * 100}%\n";
        }

        public void Upgrade()
        {
            gameData.stageUpgrades.increaseCredit.level++;
        }
    }
}
