namespace Game.UI.StageUpgrade
{
    public class IncreaseMoveSpeed : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float baseMoveSpeed => gameData.GetCharacterInformation("player").moveSpeed + gameData.permanentUpgrades.increaseMoveSpeed.current.power;
        float stageIncreasedMoveSpeed => gameData.stageUpgrades.increaseMoveSpeed.current;
        float nextStageIncreasedMoveSpeed => gameData.stageUpgrades.increaseMoveSpeed.next;
        float nextMoveSpeed => baseMoveSpeed * (1 + nextStageIncreasedMoveSpeed);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.moveSpeed)} {gameData.stageUpgrades.increaseMoveSpeed.level + 1}";

        public string GetContent()
        {
            var nowMoveSpeed = baseMoveSpeed * (1 + stageIncreasedMoveSpeed);

            return $"{gameData.language.moveSpeed} : {nowMoveSpeed} ¡æ {nextMoveSpeed}\n";
        }

        public void Upgrade()
        {
            player.movement.moveSpeed = nextMoveSpeed;
            gameData.stageUpgrades.increaseMoveSpeed.level++;
        }
    }
}
