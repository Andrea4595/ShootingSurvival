namespace Game.UI.StageUpgrade
{
    public class GetHeal : IUpgrade
    {
        Data.GameData gameData => Data.GameData.instance;

        float healAmount => gameData.stageUpgrades.heal.power[0];

        public string GetName() => gameData.language.heal;
        public string GetContent() => gameData.language.HealInformationText((healAmount * 100).ToString());
        public void Upgrade() => PlayerSetter.instance.player.health.Heal(PlayerSetter.instance.player.health.maxHp * healAmount);
    }
}
