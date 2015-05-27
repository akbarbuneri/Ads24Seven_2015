﻿namespace ArgaamPlus.CSQuery
{
    #region Imports

    using System;
    using System.Collections;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Adds reading semantics to a base  IEnumerator{T}  with the 
    /// option to un-read and insert new elements while consuming the source.
    /// </summary>
    public sealed class Reader<T> : IDisposable, IEnumerable<T>
    {
        private IEnumerator<T> _enumerator;
        private Stack<T> _buffer;

         
        public Reader(IEnumerable<T> e) :
            this(CheckNonNull(e).GetEnumerator()) { }

        private static IEnumerable<T> CheckNonNull(IEnumerable<T> e)
        {
            if (e == null) throw new ArgumentNullException("e");
            return e;
        }

         
        public Reader(IEnumerator<T> e)
        {
            if(e == null) throw new ArgumentNullException("e");
            _enumerator = e;
            _buffer = new Stack<T>();
            RealRead();
        }

         
        public bool HasMore
        {
            get
            {
                EnsureAlive();
                return _buffer.Count > 0;
            }
        }

         
        public void Unread(T value)
        {
            EnsureAlive();
            _buffer.Push(value);
        }

         
        public T Read()
        {
            if (!HasMore)
                throw new InvalidOperationException();

            var value = _buffer.Pop();

            if (_buffer.Count == 0)
                RealRead();

            return value;
        }

         
        public T Peek()
        {
            if (!HasMore)
                throw new InvalidOperationException();

            return _buffer.Peek();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

         
        public IEnumerator<T> GetEnumerator()
        {
            EnsureAlive();
            return GetEnumeratorImpl();
        }

        private IEnumerator<T> GetEnumeratorImpl()
        {
            while (HasMore)
                yield return Read();
        }

        private void RealRead()
        {
            EnsureAlive();

            if (_enumerator.MoveNext())
                Unread(_enumerator.Current);
        }

         
        public void Close()
        {
            Dispose();
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        void Dispose()
        {
            if(_enumerator == null) 
                return;
            _enumerator.Dispose();
            _enumerator = null;
            _buffer = null;
        }

        private void EnsureAlive()
        {
            if (_enumerator == null)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}