namespace Model.Parsing.Tokenization
{
    public interface ILexeme
    {
        public string LexemePattern { get; }

        public string LexemeType { get; }
    }
}
