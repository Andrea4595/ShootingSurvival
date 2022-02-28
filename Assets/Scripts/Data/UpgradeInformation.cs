using System.Collections.Generic;

namespace Data
{
    [System.Serializable]
    public class UpgradeInformation
    {
        [System.Serializable]
        public class StageUpgrades
        {
            [System.Serializable]
            public class StageUpgrade
            {
                public float weight;
                public float[] power;
            }

            public int choiceCount;
            public StageUpgrade increaseHp;
            public StageUpgrade increaseMoveSpeed;
            public StageUpgrade increaseCredit;
            public StageUpgrade heal;
            public StageUpgrade credit;
            public float weaponUpgradesWeight;

            [System.Serializable]
            public class Level
            {
                public int increaseHp;
                public int increaseMoveSpeed;
                public int increaseCredit;
                public Dictionary<string, int> weapons = new Dictionary<string, int>();

                public void Initialize()
                {
                    increaseHp = 0;
                    increaseMoveSpeed = 0;
                    increaseCredit = 0;
                }

                public void InitializeWeapons(Object.WeaponInformation[] weaponInfos)
                {
                    foreach (var weaponInfo in weaponInfos)
                        weapons.Add(weaponInfo.key, -1);
                }
            }
        }

        [System.Serializable]
        public class PermanentUpgrades
        {
            [System.Serializable]
            public class PermanentUpgrade
            {
                [System.Serializable]
                public class Level
                {
                    public int cost;
                    public float power;
                }

                public string name;
                public Level[] levels;
            }

            public PermanentUpgrade increaseDamage;
            public PermanentUpgrade increaseInterval;
            public PermanentUpgrade increaseRange;
            public PermanentUpgrade increaseHp;
            public PermanentUpgrade increaseMoveSpeed;
            public PermanentUpgrade increaseCredit;
            public PermanentUpgrade increaseChoiceCount;

            [System.Serializable]
            public class Level
            {
                public int increaseDamage;
                public int increaseInterval;
                public int increaseRange;
                public int increaseHp;
                public int increaseMoveSpeed;
                public int increaseCredit;
                public int increaseChoiceCount;
            }
        }

        public StageUpgrades stageUpgrades;
        public PermanentUpgrades permanentUpgrades;

        public void Initialize(string path) => JsonParser.GetObject(path, this);
    }
}