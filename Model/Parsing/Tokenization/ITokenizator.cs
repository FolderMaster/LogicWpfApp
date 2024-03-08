namespace Model.Parsing.Tokenization
{
    public interface ITokenizator
    {
        public IEnumerable<Token> Parse(string expression);
    }
}
