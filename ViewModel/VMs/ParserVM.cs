using CommunityToolkit.Mvvm.ComponentModel;

using Model.Logic.Expressions;
using Model.Parsing;
using Model.Logic.Operators.PairOperators;
using Model.Logic.Operators.SingleOperators;
using Model.Logic.Variables;
using Model;

namespace ViewModel.VMs
{
    public partial class ParserVM : ObservableObject
    {
        private static ITokenizator _tokenizator;

        private static IParser _parser = new Parser();

        private bool _isInput = false;

        private string _input = "";

        private IExpression<bool>? _expression;

        [ObservableProperty]
        private IEnumerable<ParsingErrorPart>? errors;

        public IExpression<bool>? Expression
        {
            get => _expression;
            set
            {
                if (SetProperty(ref _expression, value))
                {
                    if (!_isInput && Expression != null)
                    {
                        Input = Expression.ToString();
                    }
                }
            }
        }

        public string Input
        {
            get => _input;
            set
            {
                if (SetProperty(ref _input, value))
                {
                    _isInput = true;
                    ParseInput();
                    _isInput = false;
                }
            }
        }

        static ParserVM()
        {
            var lexemes = new List<ILexeme>()
            {
                new BaseLexeme("0|1|true|false", (value, context) => (value) switch
                {
                    "false" => new BoolLiteral(false),
                    "0" => new BoolLiteral(false),
                    "true" => new BoolLiteral(true),
                    "1" => new BoolLiteral(true),
                    _ => throw new NotImplementedException()
                }),
                new BaseLexeme(@"\w+", (value, context) => {
                    var variable = (INamedVariable<bool>)null;
                    foreach (var element in (IEnumerable<IValue<bool>>)context)
                    {
                        if(element is NamedBoolVariable namedVariable)
                        {
                            if (value == namedVariable.Name)
                            {
                                variable = namedVariable;
                            }
                        }
                        if (variable != null)
                        {
                            break;
                        }
                    }
                    if(variable == null)
                    {
                        return new NamedBoolVariable(value);
                    }
                    else
                    {
                        return variable;
                    }
                }),
                new BaseLexeme(@"\!|\^|\||&|=>|=", (value, context) => (value) switch
                {
                    "!" => new NotOperator(),
                    "^" => new XorOperator(),
                    "|" => new OrOperator(),
                    "&" => new AndOperator(),
                    "=>" => new ImplicateOperator(),
                    "=" => new EqualOperator(),
                    _ => throw new NotImplementedException()
                }),
            };
            _tokenizator = new Tokenizator(lexemes);
        }

        private void ParseInput()
        {
            try
            {
                var tokens = _tokenizator.Parse(Input);
                Expression = (IExpression<bool>)_parser.Parse(tokens);
                Errors = null;
            }
            catch(ParsingException ex)
            {
                Errors = ex.ErrorParts;
                Expression = null;
            }
        }
    }
}
