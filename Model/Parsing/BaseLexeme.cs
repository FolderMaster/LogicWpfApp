namespace Model.Parsing
{
    public class BaseLexeme : ILexeme
    {
        private readonly Func<string, object, object> _parsePredicate;

        public string Pattern { get; private set; }

        public BaseLexeme(string pattern, Func<string, object, object> parsePredicate)
        {
            Pattern = pattern;
            _parsePredicate = parsePredicate;
        }

        public object Parse(string value, object context) => _parsePredicate(value, context);
    }
}
