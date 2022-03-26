using Data;

namespace Game.UI.StageUpgrade
{
    public class IncreaseHp : IUpgrade
    {
        GameData gameData => GameData.instance;
        Character.Character player => PlayerSetter.instance.player;

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => gameData.stageUpgrades.increaseHp;

        float baseValue => gameData.GetCharacterInformation("player").maxHp + gameData.permanentUpgrades.increaseHp.current.power;
        float currentUpgrade => upgrade.current;
        float nextUpgrade => upgrade.next;
        float nextValue => baseValue + nextUpgrade;

        public string GetName() => $"{gameData.language.IncreaseStatusText(gameData.language.hp)} {GetLevel()}";
        string GetLevel()
        {
            if (upgrade.level + 2 >= upgrade.power.Length)
                return "MAX";

            return (upgrade.level + 1).ToString();
        }

        public string GetContent()
        {
            var nowHp = baseValue + currentUpgrade;

            return $"{gameData.language.hp} : {nowHp} ¡æ {nextValue}\n";
        }

        public void Upgrade()
        {
            player.health.SetMaxHp(nextValue);
            upgrade.level++;
        }
    }
}