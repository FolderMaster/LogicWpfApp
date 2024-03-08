namespace Model.Parsing
{
    public struct Token
    {
        public string Value { get; private set; }

        public int Index { get; private set; }

        public int Length { get; private set; }

        public string Type { get; private set; }

        public Token(string value, string type, int index, int length)
        {
            Value = value;
            Type = type;
            Index = index;
            Length = length;
        }
    }
}
