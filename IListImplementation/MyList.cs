using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IListImplementation
{
    public class MyList<T> : IList<T>
    {
        private T[] _array;

        private int _length;

        private int _pow;

        public MyList()
        {
            _pow = 4;
            _array = new T[1 << _pow];
        }

        public MyList(IEnumerable<T> enumerable)
        {
            _array = enumerable.ToArray();
            _length = _array.Length;
            while ((1 << _pow) < _length)
            {
                _pow++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _length; i++)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (_array.Length <= _length)
            {
                IncreaceArray();
            }

            _array[_length] = item;
            _length++;
        }

        private void IncreaceArray()
        {
            _pow++;
            var newArray = new T[1 << _pow];
            _array.CopyTo(newArray, 0);
            _array = newArray;
        }

        public void Clear()
        {
            _array = new T[0];
            _length = 0;
            _pow = 0;
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < _length; i++)
            {
                if (_array[i].Equals(item))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, array, _length);
        }

        public bool Remove(T item)
        {
            var indexOfItem = IndexOf(item);
            if (indexOfItem == -1)
                return false;

            var newArray = new T[_array.Length];
            Array.Copy(_array, newArray, indexOfItem);
            Array.Copy(_array, indexOfItem + 1, newArray, indexOfItem, _array.Length - (indexOfItem + 1));
            _array = newArray;
            return true;
        }

        public int Count => _length;

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            for (var i = 0; i < _length; i++)
            {
                if (_array[i].Equals(item))
                    return i;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _length)
                throw new ArgumentOutOfRangeException();

            if (_array.Length <= _length)
            {
                IncreaceArray();
            }

            var newArray = new T[_array.Length];
            Array.Copy(_array, newArray, index);
            newArray[index] = item;
            Array.Copy(_array, index, newArray, index + 1, _array.Length - (index + 1));
            _array = newArray;

            _length++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > _length || _array.Length == 0)
                throw new ArgumentOutOfRangeException();

            var item = _array[index];
            var result = Remove(item);
            if (!result)
                throw new NotSupportedException();
        }

        public T this[int index]
        {
            get => _array[index];
            set => _array[index] = value;
        }
    }
}
