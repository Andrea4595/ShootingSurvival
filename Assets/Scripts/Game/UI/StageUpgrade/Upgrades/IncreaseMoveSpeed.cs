namespace Game.UI.StageUpgrade
{
    public class IncreaseMoveSpeed : IUpgradeInformation
    {
        Data.GameData gameData => Data.GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        float baseMoveSpeed => gameData.GetCharacterData("player").moveSpeed;
        float permanentIncreasedMoveSpeed => gameData.permanentUpgrades.increaseMoveSpeed.levels[gameData.permanentUpgradeLevel.increaseMoveSpeed].power;
        float stageIncreasedMoveSpeed => gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed];
        float nextStageIncreasedMoveSpeed => gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed + 1];
        float nextMoveSpeed => baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + nextStageIncreasedMoveSpeed);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.moveSpeed)} {gameData.stageUpgradeLevel.increaseMoveSpeed + 1}";

        public string GetContent()
        {
            var nowMoveSpeed = baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + stageIncreasedMoveSpeed);

            return $"{gameData.language.moveSpeed} : {nowMoveSpeed} ¡æ {nextMoveSpeed}\n";
        }

        public void Upgrade()
        {
            player.movement.moveSpeed = nextMoveSpeed;
            gameData.stageUpgradeLevel.increaseMoveSpeed++;
        }
    }
}
