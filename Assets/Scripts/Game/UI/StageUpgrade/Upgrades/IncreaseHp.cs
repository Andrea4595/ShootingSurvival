namespace Game.UI.StageUpgrade
{
    public class IncreaseHp : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float baseHp => gameData.GetCharacterInformation("player").maxHp;
        float permanentIncreasedHp => gameData.permanentUpgrades.increaseHp.current.power;
        float stageIncreasedHp => gameData.stageUpgrades.increaseHp.current + permanentIncreasedHp * gameData.stageUpgrades.increaseHp.level;
        float nextStageIncreasedHp => gameData.stageUpgrades.increaseHp.next + permanentIncreasedHp * (gameData.stageUpgrades.increaseHp.level + 1);
        float nextHp => baseHp + nextStageIncreasedHp;

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.hp)} {gameData.stageUpgrades.increaseHp.level + 1}";

        public string GetContent()
        {
            var nowHp = baseHp + stageIncreasedHp;

            return $"{gameData.language.hp} : {nowHp} ¡æ {nextHp}\n";
        }

        public void Upgrade()
        {
            player.health.SetMaxHp(nextHp);
            gameData.stageUpgrades.increaseHp.level++;
        }
    }
}