namespace CodeFusion
{
    public readonly struct Tuple
    {
        public readonly object this[int i]
        {
            get { return _objects[i]; }
            set { _objects[i] = value; }
        }

        private readonly List<object> _objects;

        public Tuple() => _objects = new List<object>();
        public Tuple(params object[] objects) => _objects = objects.ToList();
        public Tuple(List<object> objects) => _objects = objects;

        public readonly int Length => _objects.Count;

        public readonly int GetIndexOf(object @object) =>
            Array.FindIndex(_objects.ToArray(), @object.Equals);
        public readonly string Join(string separator) => string.Join(separator, _objects);
        public object[] ToArray() => _objects.ToArray();
        public List<object> ToList() => _objects;

        public object Pop()
        {
            var obj = _objects[Random.RandomInt(0, _objects.Count)];
            Pop(obj);
            return obj;
        }
        public object Pop(object @object)
        {
            _objects.Remove(@object);
            return @object;
        }
        public object Pop(int index)
        {
            var obj = _objects[index];
            _objects.RemoveAt(index);
            return obj;
        }

        public static Tuple operator *(Tuple tuple, int times)
        {
            if (times < 0) throw new ArgumentOutOfRangeException(times.ToString());

            Tuple temp = new();
            for (int i = 0; i < times; i++)
            {
                temp += tuple;
            }
            return temp;
        }
        public static Tuple operator /(Tuple t1, Tuple t2)
        {
            Tuple temp = t1;
            for (int i = 0; i < t1.Length; i++)
            {
                temp -= t2;
            }
            return temp;
        }
        public static Tuple operator +(Tuple t1, Tuple t2)
        {
            Tuple temp = new();
            for (int i = 0; i < t1.Length; i++)
                temp._objects.Add(t1[i]);
            for (int i = 0; i < t2.Length; i++)
                temp._objects.Add(t2[i]);
            return temp;
        }
        public static Tuple operator -(Tuple t1, Tuple t2)
        {
            Tuple temp = new();
            for (int i = 0; i < t1.Length; i++)
                temp._objects.Add(t1[i]);
            for (int i = 0; i < t2.Length; i++)
                temp.Pop(t2[i]);
            return temp;
        }

        public override string ToString() => $"({Join(", ")})";
    }
}