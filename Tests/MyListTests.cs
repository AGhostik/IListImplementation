using System;
using System.Collections.Generic;
using IListImplementation;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class MyListTests
    {
        [TestCase(1,2,3, ExpectedResult = 3)]
        [TestCase(1,2,3,4,5, ExpectedResult = 5)]
        [TestCase(1,2,3,4,5,6,7,8,9,10, ExpectedResult = 10)]
        [TestCase(99,23,30,44,805,6,7,8,19,0, ExpectedResult = 10)]
        public int Add_Length(params int[] collection)
        {
            var list = new MyList<int>(collection);

            return list.Count;
        }

        [TestCase(0, 1,2,3,4,5, ExpectedResult = 1)]
        [TestCase(2, 1,2,3,4,5, ExpectedResult = 3)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = 8)]
        [TestCase(70, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = -1)]
        public int GetByIndex(int index, params int[] collection)
        {
            var list = new MyList<int>(collection);

            try
            {
                return list[index];
            }
            catch (IndexOutOfRangeException)
            {
                return -1;
            }
        }

        [TestCase(0, 1,2,3,4,5, ExpectedResult = -1)]
        [TestCase(2, 1,2,3,4,5, ExpectedResult = 1)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = 6)]
        [TestCase(99, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = -1)]
        public int IndexOf(int item, params int[] collection)
        {
            var list = new MyList<int>(collection);

            return list.IndexOf(item);
        }

        [TestCase(0, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(2, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = true)]
        [TestCase(70, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = false)]
        public bool Contains_Int(int item, params int[] collection)
        {
            var list = new MyList<int>(collection);

            return list.Contains(item);
        }

        [TestCase("hello world", "hello world", ExpectedResult = true)]
        [TestCase("hello world","hello", "world", ExpectedResult = false)]
        [TestCase("hello world", "", "test", " hello world", ExpectedResult = false)]
        public bool Contains_String(string item, params string[] collection)
        {
            var list = new MyList<string>(collection);

            return list.Contains(item);
        }

        [Test]
        public void InitializeEnumerable_Length()
        {
            IEnumerable<object> enumerable = new[] {new object(), new object(),};
            var list = new MyList<object>(enumerable);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void InitializeEnumerable_Contains()
        {
            var obj1 = new object();
            var obj2 = new object();
            IEnumerable<object> enumerable = new[] {obj1, obj2};

            var list = new MyList<object>(enumerable);

            Assert.IsTrue(list.Contains(obj1));
            Assert.IsTrue(list.Contains(obj2));
        }

        [Test]
        public void Clear_LengthIsZero()
        {
            var list = new MyList<int>();

            for (var i = 1; i <= 100; i++)
            {
                list.Add(i);
            }

            list.Clear();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void CopyTo_ListsEquals()
        {
            var list = new MyList<int>();

            for (var i = 1; i <= 10; i++)
            {
                list.Add(i);
            }

            var array = new int[10];

            list.CopyTo(array, 0);

            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual(i + 1, array[i]);
            }
        }

        [TestCase(0, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(2, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = true)]
        [TestCase(70, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = false)]
        public bool Remove(int removedItem, params int[] collection)
        {
            var list = new MyList<int>(collection);

            var removed = list.Remove(removedItem);

            foreach (var i in collection)
            {
                if (i == removedItem)
                    Assert.IsFalse(list.Contains(i));
                else
                    Assert.IsTrue(list.Contains(i));
            }

            return removed;
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(0, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(-1, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(3, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(7, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = true)]
        [TestCase(100, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = false)]
        public bool Insert(int index, params int[] collection)
        {
            var list = new MyList<int>(collection);
            var item = 666;
            try
            {
                list.Insert(index, item);
                return list.Contains(item);
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(0, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(-1, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(3, 1,2,3,4,5, ExpectedResult = true)]
        [TestCase(7, 1,2,3,4,5, ExpectedResult = false)]
        [TestCase(7, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = true)]
        [TestCase(100, 1,2,3,4,5,6,7,8,9,10, ExpectedResult = false)]
        public bool RemoveAt(int index, params int[] collection)
        {
            var list = new MyList<int>(collection);
            try
            {
                list.RemoveAt(index);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        [TestCase(1,2,3)]
        [TestCase(1,2,3,4,5)]
        [TestCase(1,2,3,4,5,6,7,8,9,10)]
        public void GetEnumerator(params int[] collection)
        {
            var index = 0;
            var count = 0;
            var list = new MyList<int>(collection);

            using (var enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Assert.AreEqual(collection[index], enumerator.Current);
                    index++;
                    count++;

                    // чтобы не зависнуть в while в случае ошибки
                    if (count > collection.Length)
                    {
                        Assert.Fail();
                        return;
                    }
                }
            }
        }
    }
}
