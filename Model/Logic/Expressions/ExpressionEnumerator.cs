using System.Collections;

namespace Model.Logic.Expressions
{
    public class ExpressionEnumerator<T> : IEnumerator<ExpressionValueWrapper<T>>
    {
        protected List<ExpressionValueWrapper<T>> _values = new List<ExpressionValueWrapper<T>>();

        protected int _position = -1;

        public ExpressionValueWrapper<T> Current => _values[_position];

        object IEnumerator.Current => Current;

        public ExpressionEnumerator(ExpressionValueWrapper<T> value)
        {
            var stack = new Stack<ExpressionValueWrapper<T>>();
            var current = value;

            while (current != null || stack.Count > 0)
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = ExpressionHelper<T>.GetLeftValue(current);
                }
                else if (stack.Count > 0)
                {
                    current = stack.Pop();
                    _values.Add(current);
                    current = ExpressionHelper<T>.GetRightValue(current);
                }
            }
        }

        public bool MoveNext()
        {
            if (_position < _values.Count)
            {
                ++_position;
            }
            return _position >= 0 && _position < _values.Count;
        }

        public void Reset() => _position = -1;

        public void Dispose() { }
    }
}
