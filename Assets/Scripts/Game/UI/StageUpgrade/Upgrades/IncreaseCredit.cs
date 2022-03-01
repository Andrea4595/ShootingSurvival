namespace Game.UI.StageUpgrade
{
    public class IncreaseCredit : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float permanentIncreasedCredit => gameData.permanentUpgrades.increaseCreditBonus.current.power;
        float stageIncreasedCredit => gameData.stageUpgrades.increaseCredit.current + permanentIncreasedCredit * gameData.stageUpgrades.increaseCredit.level;
        float nextStageIncreasedCredit => gameData.stageUpgrades.increaseCredit.next + permanentIncreasedCredit * (gameData.stageUpgrades.increaseCredit.level + 1);
        float nextCredit => nextStageIncreasedCredit;

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.creditBonus)} {gameData.stageUpgrades.increaseCredit.level + 1}";

        public string GetContent()
        {
            var nowCredit = stageIncreasedCredit;

            return $"{gameData.language.creditBonus} : {nowCredit * 100}% ¡æ {nextCredit * 100}%\n";
        }

        public void Upgrade()
        {
            //TODO : Credit Bonus
            gameData.stageUpgrades.increaseCredit.level++;
        }
    }
}
