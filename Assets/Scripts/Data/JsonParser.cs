using UnityEngine;

namespace Data
{
    public class JsonParser
    {
        public static void GetObject(string path, object to)
        {
            var jsonText = System.IO.File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(jsonText, to);
        }
    }
}