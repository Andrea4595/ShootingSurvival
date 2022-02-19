namespace Data.Object
{
    [System.Serializable]
    public class Weapon
    {
        [System.Serializable]
        public class Projectile
        {
            public float damage;
            public float range;
            public float speed;
            public float scale;
            public float homming;
            public float lifetime;
            public int count;
            public float angleRange;
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
        public Projectile projectile;
        public int fireCount;
        public float interval;
        public float angle;

        public Upgrade[] upgrades;
    }
}