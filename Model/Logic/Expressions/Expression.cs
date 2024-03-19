using Model.Logic.Variables;
using System.Collections;
using System.Linq;

namespace Model.Logic.Expressions
{
    public class Expression<T> : IExpression<T>
    {
        protected ExpressionValueWrapper<T>? _root = null;

        public int Count { get; private set; }

        public IValue<T> this[int index]
        {
            get => FindValue(index);
            set => throw new NotImplementedException();
        }

        protected ExpressionValueWrapper<T> Merge(ExpressionValueWrapper<T> left, ExpressionValueWrapper<T> right)
        {
            if (left == null)
            {
                return right;
            }
            else if (right == null)
            {
                return left;
            }
            else if (left.Priority >= right.Priority)
            {
                ExpressionHelper<T>.SetRightValue(left,
                    Merge(ExpressionHelper<T>.GetRightValue(left), right));
                return left;
            }
            else
            {
                ExpressionHelper<T>.SetLeftValue(right,
                    Merge(left, ExpressionHelper<T>.GetLeftValue(right)));
                return right;
            }
        }

        protected (ExpressionValueWrapper<T>, ExpressionValueWrapper<T>) Split(ExpressionValueWrapper<T> value, int index)
        {
            if (value == null)
            {
                return (null, null);
            }
            else if (value.Index < index)
            {
                var (left, right) = Split(ExpressionHelper<T>.GetRightValue(value), index);
                ExpressionHelper<T>.SetRightValue(value, left);
                return (value, right);
            }
            else
            {
                var (left, right) = Split(ExpressionHelper<T>.GetLeftValue(value), index);
                ExpressionHelper<T>.SetLeftValue(value, right);
                return (left, value);
            }
        }

        protected void ChangeIndexes(ExpressionValueWrapper<T>? value, int change)
        {
            if (value != null)
            {
                value.Index += change;
                ChangeIndexes(ExpressionHelper<T>.GetLeftValue(value), change);
                ChangeIndexes(ExpressionHelper<T>.GetRightValue(value), change);
            }
        }

        protected IValue<T> FindValue(int index)
        {
            var current = _root;
            while (current != null)
            {
                if (current.Index == index)
                {
                    return current.Value;
                }
                else
                {
                    current = (current.Index < index) switch
                    {
                        true => ExpressionHelper<T>.GetRightValue(current),
                        false => ExpressionHelper<T>.GetLeftValue(current)
                    };

                    if (current == null)
                    {
                        break;
                    }
                }
            }
            throw new ArgumentException();
        }

        public Expression() { }

        public void Add(IValue<T> value, int index = -1)
        {
            if (index == -1)
            {
                index = Count;
            }
            var wrapper = new ExpressionValueWrapper<T>(value);
            wrapper.Index = index;
            var (left, right) = Split(_root, index);
            ChangeIndexes(right, 1);
            _root = Merge(Merge(left, wrapper), right);
            ++Count;
        }

        public void Remove(int index)
        {
            var (left, rightLeft) = Split(_root, index);
            var (_, right) = Split(rightLeft, index + 1);
            ChangeIndexes(right, -1);
            _root = Merge(left, right);
            --Count;
        }

        public IEnumerator<ExpressionValueWrapper<T>> GetEnumerator() =>
            new ExpressionEnumerator<T>(_root);

        public void Clear() => _root = null;

        public T GetValue() => _root.GetValue();

        IEnumerator IEnumerable.GetEnumerator() => new ExpressionEnumerator<T>(_root);

        public override string ToString()
        {
            var result = "";
            foreach (var item in this)
            {
                result += item.Value.ToString();
            }
            return result;
        }

        public IEnumerable<INamedVariable<T>> GetVariables()
        {
            var result = new List<INamedVariable<T>>();
            foreach (var valueWrapper in this)
            {
                var value = valueWrapper.Value;
                if (value is INamedVariable<T> variable)
                {
                    if(!result.Contains(variable))
                    {
                        result.Add(variable);
                    }
                }
            }
            return result;
        }

        public object Clone()
        {
            var result = new Expression<T>();
            var variables = new List<INamedVariable<T>>();
            foreach (var valueWrapper in this)
            {
                var value = valueWrapper.Value;
                if (value is INamedVariable<T> variable)
                {
                    var selectedVariable = variables.Find((v) => v.Name == variable.Name);
                    if (selectedVariable == null)
                    {
                        selectedVariable = (INamedVariable<T>)variable.Clone();
                        variables.Add(selectedVariable);
                        result.Add(selectedVariable);
                    }
                    else
                    {
                        result.Add(selectedVariable);
                    } 
                }
                else
                {
                    result.Add((IValue<T>)value.Clone());
                }
            }
            return result;
        }
    }
}
