using System.Collections.Generic;

namespace testlol.Util
{
    public class StuffHolder<T>
    {
        private readonly Dictionary<string, T> _things = new Dictionary<string, T>();

        public T this[string key]
        {
            get { return _things[key]; }
            set
            {
                if (!_things.ContainsKey(key))
                    _things.Add(key, value);
                else
                    _things[key] = value;
            }
        }
    }
}
