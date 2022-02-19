namespace Data.Object
{
    [System.Serializable]
    public class Stage
    {
        [System.Serializable]
        public class Spawn
        {
            public string key;
            public int count;
        }

        [System.Serializable]
        public class Group
        {
            public Spawn[] spawns;
        }

        public int credit;
        public Group[] groups;
    }
}
