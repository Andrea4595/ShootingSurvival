namespace Data.Object
{
    [System.Serializable]
    public class StageInformation
    {
        [System.Serializable]
        public class Group
        {
            [System.Serializable]
            public class Spawn
            {
                public string key = "player";
                public int count = 1;
            }

            public Spawn[] spawns = new Spawn[0];
        }

        public int credit;
        public Group[] groups = new Group[0];
    }
}
