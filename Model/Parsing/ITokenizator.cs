namespace Model.Parsing
{
    public interface ITokenizator
    {
        public IEnumerable<Token> Parse(string expression);
    }
}
