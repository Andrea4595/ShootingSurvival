using System.Collections;

namespace Data.Object
{
    [System.Serializable]
    public class WeaponInformation
    {
        [System.Serializable]
        public class Projectile
        {
            public string sprite;
            public float maxHp;
            public float damage;
            public float range;
            public float speed;
            public float scale;
            public float homming;
            public float lifetime;
            public bool hitProjectile;
        }

        [System.Serializable]
        public class Fix
        {
            public string key;
            public float fixTo;
        }

        [System.Serializable]
        public class Upgrade
        {
            public Fix[] fixes;
        }

        public string key;
        public string name => GetWeaponText().name;
        public string information => GetWeaponText().information;
        public bool forPlayer;
        public Projectile projectile;
        public string fireType;
        public int fireCount;
        public int continuousCount;
        public float interval;
        public float angleRange;

        LanguageInformation.Language.Weapon GetWeaponText()
        {
            LanguageInformation.Language.Weapon weaponText;
            
            if (GameData.instance.language.GetWeapon(key, out weaponText) == false)
            {
                weaponText = new LanguageInformation.Language.Weapon();
                weaponText.name = key;
                weaponText.information = $"No text for {key}. Please write text in config/language.json";
            }

            return weaponText;
        }

        public Upgrade[] upgrades;

        public IEnumerator FireTypeCoroutine(Game.Character.Character owner)
        {
            switch (fireType)
            {
                case "FrontEvenSpread":
                    return Game.Weapon.FireType.FrontEvenSpread.CRun(owner, this);
                case "FrontArrow":
                    return Game.Weapon.FireType.FrontArrow.CRun(owner, this);
                case "Random":
                    return Game.Weapon.FireType.Random.CRun(owner, this);
            }

            UnityEngine.Debug.LogError($"no firetype named {fireType}");
            return null;
        }
    }
}