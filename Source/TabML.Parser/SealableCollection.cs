using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabML.Parser
{
    class SealableCollection<T> : ICollection<T>, ICloneable
    {
        private readonly List<T> _storage;

        public bool IsSealed { get; private set; }

        public SealableCollection()
        {
            _storage = new List<T>();
        }

        public void Seal()
        {
            this.IsSealed = true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_storage).GetEnumerator();
        }

        public void Add(T item)
        {
            this.CheckSealed();
            _storage.Add(item);
        }

        private void CheckSealed()
        {
            if (this.IsSealed)
                throw new InvalidOperationException("this collection is sealed an uneditable");
        }

        public void Clear()
        {
            this.CheckSealed();
            _storage.Clear();
        }

        public bool Contains(T item)
        {
            return _storage.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _storage.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            this.CheckSealed();
            return _storage.Remove(item);
        }

        public int Count => _storage.Count;

        public bool IsReadOnly => this.IsSealed;

        public SealableCollection<T> Clone()
        {
            var collection = new SealableCollection<T>();

            if (typeof (ICloneable).IsAssignableFrom(typeof (T)))
            {
                foreach (var item in _storage)
                    collection.Add((T) ((ICloneable) item).Clone());
            }
            else
            {
                foreach (var item in _storage)
                    collection.Add(item);
            }

            return collection;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public void AppendClone(SealableCollection<T> other)
        {
            if (typeof(ICloneable).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in other)
                    this.Add((T)((ICloneable)item).Clone());
            }
            else
            {
                foreach (var item in other)
                    this.Add(item);
            }
        }
    }
}
