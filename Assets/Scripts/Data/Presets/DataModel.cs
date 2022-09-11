namespace Conffi.Data
{
    public enum ElementType
    {
        Left,
        Right,
        Middle,
        Corner,
        Pouf
    }
    
    public enum ElementSideType
    {
        None,
        Large,
        Thin
    }

    [System.Serializable]
    public class DataModel
    {
        public ElementType ElementType { get; set; }
        public ElementSideType ElementSideType { get; set; }
        public bool hasPillows { get; set; }
        
        public int x { get; set; }
        public int y { get; set; }
        public float rotation { get; set; }
    }

    public enum CommandType
    {
        Add,
        Remove,
        Swap
    }
    
    [System.Serializable]
    public class Command
    {
        public CommandType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float rotation { get; set; }
    }
}