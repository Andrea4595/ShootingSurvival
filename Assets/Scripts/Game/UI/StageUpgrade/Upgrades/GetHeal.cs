using Data;

namespace Game.UI.StageUpgrade
{
    public class GetHeal : IUpgrade
    {
        GameData gameData => GameData.instance;

        float healAmount => gameData.stageUpgrades.heal.power[0];

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => null;

        public string GetName() => gameData.language.heal;
        public string GetContent() => gameData.language.HealInformationText((healAmount).ToString());
        public void Upgrade() => PlayerSetter.instance.player.health.Heal(healAmount);
    }
}
