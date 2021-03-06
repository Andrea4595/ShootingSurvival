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
            public class Upgrade
            {
                public float weight;
                public float[] power;

                public int level { get; set; }
                public float current => power[level];
                public float next => power[level + 1];
            }

            public int choiceCount;
            public Upgrade increaseHp;
            public Upgrade increaseMoveSpeed;
            public Upgrade increaseCredit;
            public Upgrade heal;
            public Upgrade credit;

            public void Initialize()
            {
                increaseHp.level = 0;
                increaseMoveSpeed.level = 0;
                increaseCredit.level = 0;
            }

            public Dictionary<string, int> weaponLevels = new Dictionary<string, int>();

            public void InitializeWeapons(Dictionary<string, Object.WeaponInformation> weaponInfos)
            {
                foreach (var weaponInfo in weaponInfos)
                {
                    if (weaponLevels.ContainsKey(weaponInfo.Key))
                        weaponLevels[weaponInfo.Key] = -1;
                    else
                        weaponLevels.Add(weaponInfo.Key, -1);
                }
            }
        }

        [System.Serializable]
        public class PermanentUpgrades
        {
            [System.Serializable]
            public class Upgrade
            {
                [System.Serializable]
                public class Level
                {
                    public int cost = 0;
                    public float power = 1;
                }

                public Level[] levels;

                public int level { get; set; }
                public Level current => levels[level];
                public Level next => levels[level + 1];
            }

            public Upgrade increaseHp;
            public Upgrade increaseDamage;
            public Upgrade increaseMoveSpeed;
            public Upgrade increaseCreditBonus;
            public Upgrade increaseChoiceCount;
        }

        public StageUpgrades stageUpgrades;
        public PermanentUpgrades permanentUpgrades;

        public void Initialize(string path) => JsonParser.GetObject(path, this);
    }
}