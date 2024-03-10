namespace Model.Parsing
{
    public interface IParser
    {
        public object Parse(IEnumerable<Token> tokens);
    }
}
