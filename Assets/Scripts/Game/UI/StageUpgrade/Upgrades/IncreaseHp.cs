namespace Game.UI.StageUpgrade
{
    public class IncreaseHp : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float baseHp => gameData.GetCharacterInformation("player").maxHp + gameData.permanentUpgrades.increaseHp.current.power;
        float stageIncreasedHp => gameData.stageUpgrades.increaseHp.current;
        float nextStageIncreasedHp => gameData.stageUpgrades.increaseHp.next;
        float nextHp => baseHp * (1 + nextStageIncreasedHp);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.hp)} {gameData.stageUpgrades.increaseHp.level + 1}";

        public string GetContent()
        {
            var nowHp = baseHp * (1 + stageIncreasedHp);

            return $"{gameData.language.hp} : {nowHp} ¡æ {nextHp}\n";
        }

        public void Upgrade()
        {
            player.health.SetMaxHp(nextHp);
            gameData.stageUpgrades.increaseHp.level++;
        }
    }
}