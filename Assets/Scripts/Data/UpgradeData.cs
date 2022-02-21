namespace Data
{
    [System.Serializable]
    public class UpgradeData
    {
        [System.Serializable]
        public class StageUpgrades
        {
            public int choiceCount;
            public float[] increaseHp;
            public float[] increaseMoveSpeed;
            public float[] increaseCredit;
            public float heal;
            public int credit;
        }

        [System.Serializable]
        public class PermanentUpgrades
        {
            public Upgrade[] increaseDamage;
            public Upgrade[] increaseInterval;
            public Upgrade[] increaseRange;
            public Upgrade[] increaseHp;
            public Upgrade[] increaseMoveSpeed;
            public Upgrade[] increaseCredit;
            public Upgrade[] increaseChoiceCount;
        }

        [System.Serializable]
        public class Upgrade
        {
            public int cost;
            public float power;
        }

        public StageUpgrades stageUpgrades;
        public PermanentUpgrades permanentUpgrades;

        public void Initialize(string path) => JsonParser.GetObject(path, this);
    }
}