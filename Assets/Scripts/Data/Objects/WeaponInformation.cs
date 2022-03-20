using System;
using System.Collections;

namespace Data.Object
{
    [System.Serializable]
    public class WeaponInformation
    {
        [System.Serializable]
        public class Projectile
        {
            public string sprite = "Bullet";
            public float maxHp = 0;
            public float damage = 1;
            public float range = 0;
            public float speed = 10;
            public float scale = 1;
            public float homming = 0;
            public float lifetime = 5;
            public bool hitProjectile = true;

            public Projectile Clone()
            {
                var clone = new Projectile();
                clone.sprite = (string)sprite.Clone();
                clone.maxHp = maxHp;
                clone.damage = damage;
                clone.range = range;
                clone.speed = speed;
                clone.scale = scale;
                clone.homming = homming;
                clone.lifetime = lifetime;
                clone.hitProjectile = hitProjectile;
                return clone;
            }
        }

        [System.Serializable]
        public class Upgrade
        {
            [System.Serializable]
            public class Fix
            {
                public string key = "projectile/damage";
                public float fixTo;

                public static string[] keyList = { "projectile/maxHp", "projectile/damage", "projectile/range", "projectile/speed", "projectile/scale", "projectile/homming", "projectile/lifetime", "fireCount", "continuousCount", "interval", "angleRange" };

                public Fix Clone()
                {
                    var clone = new Fix();
                    clone.key = (string)key.Clone();
                    clone.fixTo = fixTo;

                    return clone;
                }
            }

            public Fix[] fixes = new Fix[0];

            public Upgrade Clone()
            {
                var clone = new Upgrade();
                clone.fixes = new Fix[fixes.Length];
                for (int i = 0; i < fixes.Length; i++)
                    clone.fixes[i] = fixes[i].Clone();

                return clone;
            }
        }

        public string key = "newWeapon";
        public string name => GetWeaponText().name;
        public string information => GetWeaponText().information;
        public bool forPlayer = false;
        public float weight = 1;
        public Projectile projectile = new Projectile();
        public enum Type { FrontEvenSpread, FrontArrow, Random }
        public string fireType = "FrontEvenSpread";
        public int fireCount = 1;
        public int continuousCount = 1;
        public float interval = 1;
        public float angleRange = 0;

        public Type GetFireType()
        {
            switch (fireType)
            {
                case "FrontEvenSpread":
                    return Type.FrontEvenSpread;
                case "FrontArrow":
                    return Type.FrontArrow;
                case "Random":
                    return Type.Random;
            }

            UnityEngine.Debug.LogError($"no firetype named {fireType}");
            return Type.FrontEvenSpread;
        }

        public void SetFireType(Type type)
        {
            switch (type)
            {
                case Type.FrontEvenSpread:
                    fireType = "FrontEvenSpread";
                    break;
                case Type.FrontArrow:
                    fireType = "FrontArrow";
                    break;
                case Type.Random:
                    fireType = "Random";
                    break;
            }
        }

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

        public Upgrade[] upgrades = new Upgrade[0];

        public IEnumerator FireTypeCoroutine(Game.Character.Character owner)
        {
            switch (GetFireType())
            {
                case Type.FrontEvenSpread:
                    return Game.Weapon.FireType.FrontEvenSpread.CRun(owner, this);
                case Type.FrontArrow:
                    return Game.Weapon.FireType.FrontArrow.CRun(owner, this);
                case Type.Random:
                    return Game.Weapon.FireType.Random.CRun(owner, this);
            }

            return null;
        }

        public WeaponInformation Clone()
        {
            var clone = new WeaponInformation();

            clone.key = (string)key.Clone();
            clone.forPlayer = forPlayer;
            clone.projectile = projectile.Clone();
            clone.fireType = (string)fireType.Clone();
            clone.fireCount = fireCount;
            clone.continuousCount = continuousCount;
            clone.interval = interval;
            clone.angleRange = angleRange;
            clone.upgrades = new Upgrade[upgrades.Length];
            for (var i = 0; i < upgrades.Length; i++)
                clone.upgrades[i] = upgrades[i].Clone();

            return clone;
        }
    }
}