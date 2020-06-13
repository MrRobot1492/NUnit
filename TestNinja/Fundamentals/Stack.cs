using System;
using System.Collections.Generic;

namespace TestNinja.Fundamentals
{
    public class Stack<T> where T : class, new()
    {
        private readonly List<T> _list = new List<T>();

        public int Count => _list.Count;

        public void Push(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("Input value cannot be NULL");

            _list.Add(obj);
        }

        public T Pop()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException("Cannot remove elements on an empty list");

            var result = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);

            return result;
        }


        public T Peek()
        {
            if (_list.Count == 0)
                throw new InvalidOperationException();

            return _list[_list.Count - 1];
        }
    }
}