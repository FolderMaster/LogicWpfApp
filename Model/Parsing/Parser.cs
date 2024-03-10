using Model.Logic.Expressions;

namespace Model.Parsing
{
    public class Parser : IParser
    {
        public object Parse(IEnumerable<Token> tokens)
        {
            var result = new Expression<bool>();
            var values = new List<IValue<bool>>();
            foreach (var token in tokens)
            {
                var value = (IValue<bool>)token.Lexeme.Parse(token.Value, values);
                result.Add(value);
                values.Add(value);
            }
            return result;
        }
    }
}
