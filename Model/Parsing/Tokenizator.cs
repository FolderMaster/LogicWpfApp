using System.Text.RegularExpressions;

namespace Model.Parsing
{
    public class Tokenizator : ITokenizator
    {
        private readonly IEnumerable<ILexeme> _lexemes;

        public Tokenizator(IEnumerable<ILexeme> lexemes) => _lexemes = lexemes;

        private List<Token> ResolveConflicts(List<Token> tokens)
        {
            var result = new List<Token>();
            var sortedList = tokens.OrderByDescending((t) => t.Length).
                OrderBy((t) => t.Index).ToList();
            var selectedToken = sortedList.FirstOrDefault();
            while (!selectedToken.Equals(default(Token)))
            {
                result.Add(selectedToken);
                sortedList.RemoveAll((token) => token.Index <
                    selectedToken.Index + selectedToken.Length);
                selectedToken = sortedList.FirstOrDefault();
            }
            return result;
        }

        private List<ParsingErrorPart> CheckErrors(IEnumerable<Token> tokens, int index,
            int length, string word)
        {
            var result = new List<ParsingErrorPart>();
            var currentIndex = index;
            foreach (var token in tokens)
            {
                if (currentIndex < token.Index)
                {
                    var currentLength = token.Index - currentIndex;
                    result.Add(new ParsingErrorPart(word.Substring(currentIndex - index,
                        currentLength), currentIndex, currentLength,
                        "Token not finded!", "Tokenizator"));
                }
                currentIndex = token.Index + token.Length;
            }
            var lastIndex = index + length;
            if (currentIndex < lastIndex)
            {
                result.Add(new ParsingErrorPart(word.Substring(currentIndex - index), currentIndex,
                    lastIndex - currentIndex, "Token not finded!", "Tokenizator"));
            }
            return result;
        }

        public IEnumerable<Token> Parse(string expression)
        {
            var result = new List<Token>();
            var errors = new List<ParsingErrorPart>();
            var wordMatch = Regex.Match(expression, @"\S+");
            while (wordMatch.Success)
            {
                var tokens = new List<Token>();
                foreach (var lexeme in _lexemes)
                {
                    var lexemeMatch = Regex.Match(wordMatch.Value, lexeme.Pattern,
                        RegexOptions.IgnoreCase);
                    while (lexemeMatch.Success)
                    {
                        tokens.Add(new Token(lexemeMatch.Value, lexeme,
                            lexemeMatch.Index + wordMatch.Index, lexemeMatch.Length));
                        lexemeMatch = lexemeMatch.NextMatch();
                    }
                }
                tokens = ResolveConflicts(tokens);
                errors.AddRange(CheckErrors(tokens, wordMatch.Index,
                    wordMatch.Length, wordMatch.Value));
                result.AddRange(tokens);
                wordMatch = wordMatch.NextMatch();
            }
            if (errors.Count > 0)
            {
                throw new ParsingException(errors);
            }
            return result;
        }
    }
}
