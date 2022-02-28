namespace Game.UI.StageUpgrade
{
    public class IncreaseHp : IUpgradeInformation
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float baseHp => gameData.GetCharacterData("player").maxHp;
        float permanentIncreasedHp => gameData.permanentUpgrades.increaseHp.levels[gameData.permanentUpgradeLevel.increaseHp].power;
        float stageIncreasedHp => gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp];
        float nextStageIncreasedHp => gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp + 1];
        float nextHp => baseHp + permanentIncreasedHp + nextStageIncreasedHp;

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.hp)} {gameData.stageUpgradeLevel.increaseHp + 1}";

        public string GetContent()
        {
            var nowHp = baseHp + permanentIncreasedHp + stageIncreasedHp;

            return $"{gameData.language.hp} : {nowHp} ¡æ {nextHp}\n";
        }

        public void Upgrade()
        {
            player.health.SetMaxHp(nextHp);
            gameData.stageUpgradeLevel.increaseHp++;
        }
    }
}