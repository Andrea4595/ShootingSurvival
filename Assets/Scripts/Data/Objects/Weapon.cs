namespace Data.Object
{
    [System.Serializable]
    public class Weapon
    {
        [System.Serializable]
        public class Projectile
        {
            public string sprite;
            public float damage;
            public float range;
            public float speed;
            public float scale;
            public float homming;
            public float lifetime;
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
        public int continuousCount;
        public float interval;
        public float angleRange;

        public Upgrade[] upgrades;
    }
}