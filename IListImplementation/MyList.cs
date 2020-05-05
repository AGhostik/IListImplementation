using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IListImplementation
{
    public class MyList<T> : IList<T>
    {
        private T[] _array;

        public MyList()
        {
            // почитай снова статью про дырявые интерфейсы у Сергея Теплякова - там была информация про внутреннюю работу List<T>
            _array = new T[16];
        }
        public MyList(IEnumerable<T> enumerable)
        {
            _array = enumerable.ToArray();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            var newArray = new T[_array.Length + 1];
            _array.CopyTo(newArray, 0);
            newArray[^1] = item;
        }

        public void Clear()
        {
            _array = new T[0];
        }

        public bool Contains(T item)
        {
            // проще было бы использовать готовый одноименный extension 

            for (var i = 0; i < _array.Length; i++)
            {
                if (_array[i].Equals(item))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {

            _array.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count => _array.Length;

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }
    }
}
