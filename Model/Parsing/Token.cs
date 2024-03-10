namespace Model.Parsing
{
    public struct Token
    {
        public string Value { get; private set; }

        public int Index { get; private set; }

        public int Length { get; private set; }

        public ILexeme Lexeme { get; private set; }

        public Token(string value, ILexeme lexeme, int index, int length)
        {
            Value = value;
            Lexeme = lexeme;
            Index = index;
            Length = length;
        }
    }
}
