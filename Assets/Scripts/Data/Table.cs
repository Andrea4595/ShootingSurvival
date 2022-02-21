namespace Data
{
    [System.Serializable]
    public class Table<T>
    {
        public T[] items;
        public void Initialize(string path) => JsonParser.GetObject(path, this);
    }
}