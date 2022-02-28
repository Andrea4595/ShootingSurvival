namespace Data
{
    [System.Serializable]
    public class UpgradeInformation
    {
        [System.Serializable]
        public class StageUpgrades
        {
            public int choiceCount;
            public StageUpgrade increaseHp;
            public StageUpgrade increaseMoveSpeed;
            public StageUpgrade increaseCredit;
            public StageUpgrade heal;
            public StageUpgrade credit;
            public float weaponUpgradesWeight;
        }

        [System.Serializable]
        public class StageUpgrade
        {
            public float weight;
            public float[] power;
        }

        [System.Serializable]
        public class PermanentUpgrades
        {
            public PermanentUpgrade[] increaseDamage;
            public PermanentUpgrade[] increaseInterval;
            public PermanentUpgrade[] increaseRange;
            public PermanentUpgrade[] increaseHp;
            public PermanentUpgrade[] increaseMoveSpeed;
            public PermanentUpgrade[] increaseCredit;
            public PermanentUpgrade[] increaseChoiceCount;
        }

        [System.Serializable]
        public class PermanentUpgrade
        {
            public int cost;
            public float power;
        }

        public StageUpgrades stageUpgrades;
        public PermanentUpgrades permanentUpgrades;

        public void Initialize(string path) => JsonParser.GetObject(path, this);
    }
}