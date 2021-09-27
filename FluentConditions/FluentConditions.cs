using System;

namespace FluentConditions
{
    public static class FluentCondition
    {
        public static FluentCondition<T> Of<T>()
        {
            return new FluentCondition<T>();
        }

        public static FluentCondition<T> When<T>(bool condition, T value)
        {
            return Of<T>().When(condition, value);
        }

        public static FluentCondition<T> When<T>(bool condition, Action<FluentCondition<T>> action)
        {
            return Of<T>().When(condition, action);
        }
    }

    public class FluentCondition<T>
    {
        private FluentCondition<T> _next;
        private FluentCondition<T> _root;
        private Action<FluentCondition<T>> _action;

        private bool _condition;
        private T _value;

        private FluentCondition<T> GetRoot() => _root ?? this;

        public FluentCondition<T> When(bool condition, T value)
        {
            _next = new FluentCondition<T>
            {
                _condition = condition,
                _value = value,
                _root = GetRoot()
            };
            return _next;
        }

        public FluentCondition<T> When(bool condition, Action<FluentCondition<T>> func)
        {
            _next = new FluentCondition<T>
            {
                _condition = condition,
                _action = func,
                _root = GetRoot()
            };
            return _next;
        }

        public FluentCondition<T> Otherwise(T value)
        {
            _next = new FluentCondition<T>
            {
                _condition = true,
                _root = GetRoot(),
                _value = value
            };
            return _next;
        }

        private bool TryGetResult(out T result)
        {
            if (_condition)
            {
                if (_action != null)
                {
                    var fluentCondition = new FluentCondition<T>();
                    _action(fluentCondition);
                    return fluentCondition.TryGetResult(out result);
                }
                result = _value;
                return true;
            }
            if (_next == null)
            {
                result = default;
                return false;
            }
            return _next.TryGetResult(out result);
        }

        public T GetResult()
        {
            var startPoint = _root._next;
            startPoint.TryGetResult(out var result);
            return result;
        }
    }
}
