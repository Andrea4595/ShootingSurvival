using Data;

namespace Game.UI.StageUpgrade
{
    public class IncreaseMoveSpeed : IUpgrade
    {
        GameData gameData => GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => gameData.stageUpgrades.increaseMoveSpeed;

        float baseValue => gameData.GetCharacterInformation("player").moveSpeed + gameData.permanentUpgrades.increaseMoveSpeed.current.power;
        float currentUpgrade => upgrade.current;
        float nextUpgrade => upgrade.next;
        float nextValue => baseValue * (1 + nextUpgrade);

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.moveSpeed)} {GetLevel()}";
        string GetLevel()
        {
            if (upgrade.level + 2 >= upgrade.power.Length)
                return "MAX";

            return (upgrade.level + 1).ToString();
        }

        public string GetContent()
        {
            var nowMoveSpeed = baseValue * (1 + currentUpgrade);

            return $"{gameData.language.moveSpeed} : {nowMoveSpeed} ¡æ {nextValue}\n";
        }

        public void Upgrade()
        {
            player.movement.moveSpeed = nextValue;
            upgrade.level++;
        }
    }
}
