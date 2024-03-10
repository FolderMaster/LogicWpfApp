namespace Model.Parsing
{
    public interface ILexeme
    {
        public string Pattern { get; }

        public object Parse(string value, object context);
    }
}
