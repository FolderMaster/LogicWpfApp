namespace Model.Parsing
{
    public struct ParsingErrorPart
    {
        public string Value { get; private set; }

        public int Index { get; private set; }

        public int Length { get; private set; }

        public string Message { get; private set; }

        public string Type { get; private set; }

        public ParsingErrorPart(string value, int index,
            int length, string message, string type)
        {
            Value = value;
            Index = index;
            Length = length;
            Message = message;
            Type = type;
        }
    }
}
