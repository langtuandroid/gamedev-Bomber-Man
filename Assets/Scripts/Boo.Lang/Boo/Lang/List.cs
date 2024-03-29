using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	[Serializable]
	public class List<T> : ICollection<T>, IList<T>, IEnumerable<T>, IEquatable<List<T>>, ICollection, IEnumerable, IList
	{
		private static readonly T[] EmptyArray = new T[0];

		protected T[] _items;

		protected int _count;

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = Coerce(value);
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		public int Count
		{
			get
			{
				return _count;
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		public object SyncRoot
		{
			get
			{
				return _items;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public T this[int index]
		{
			get
			{
				return _items[CheckIndex(NormalizeIndex(index))];
			}
			set
			{
				_items[CheckIndex(NormalizeIndex(index))] = value;
			}
		}

		public List()
		{
			_items = EmptyArray;
		}

		void ICollection<T>.Add(T item)
		{
			Push(item);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		void IList<T>.Insert(int index, T item)
		{
			Insert(index, item);
		}

		void IList<T>.RemoveAt(int index)
		{
			InnerRemoveAt(CheckIndex(NormalizeIndex(index)));
		}

		bool ICollection<T>.Remove(T item)
		{
			return InnerRemove(item);
		}

		int IList.Add(object value)
		{
			Add((T)value);
			return Count - 1;
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, Coerce(value));
		}

		void IList.Remove(object value)
		{
			Remove(Coerce(value));
		}

		int IList.IndexOf(object value)
		{
			return IndexOf(Coerce(value));
		}

		bool IList.Contains(object value)
		{
			return Contains(Coerce(value));
		}

		void IList.RemoveAt(int index)
		{
			RemoveAt(index);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(_items, 0, array, index, _count);
		}

		public IEnumerator<T> GetEnumerator()
		{
			int originalCount = _count;
			T[] originalItems = _items;
			for (int i = 0; i < _count; i++)
			{
				if (originalCount != _count || originalItems != _items)
				{
					throw new InvalidOperationException("The list was modified.");
				}
				yield return _items[i];
			}
		}

		public void CopyTo(T[] target, int index)
		{
			Array.Copy(_items, 0, target, index, _count);
		}

		public List<T> Push(T item)
		{
			return Add(item);
		}

		public virtual List<T> Add(T item)
		{
			EnsureCapacity(_count + 1);
			_items[_count] = item;
			_count++;
			return this;
		}

		public override string ToString()
		{
			return "[" + Join(", ") + "]";
		}

		public string Join(string separator)
		{
			return Builtins.join(this, separator);
		}

		public override int GetHashCode()
		{
			int num = _count;
			for (int i = 0; i < _count; i++)
			{
				T val = _items[i];
				if (val != null)
				{
					num ^= val.GetHashCode();
				}
			}
			return num;
		}

		public override bool Equals(object other)
		{
			return this == other || Equals(other as List<T>);
		}

		public bool Equals(List<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			if (_count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < _count; i++)
			{
				if (!RuntimeServices.EqualityOperator(_items[i], other[i]))
				{
					return false;
				}
			}
			return true;
		}

		public void Clear()
		{
			for (int i = 0; i < _count; i++)
			{
				_items[i] = default(T);
			}
			_count = 0;
		}

		public bool Contains(T item)
		{
			return -1 != IndexOf(item);
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < _count; i++)
			{
				if (RuntimeServices.EqualityOperator(_items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		public List<T> Insert(int index, T item)
		{
			int num = NormalizeIndex(index);
			EnsureCapacity(Math.Max(_count, num) + 1);
			if (num < _count)
			{
				Array.Copy(_items, num, _items, num + 1, _count - num);
			}
			_items[num] = item;
			_count++;
			return this;
		}

		public List<T> Remove(T item)
		{
			InnerRemove(item);
			return this;
		}

		public List<T> RemoveAt(int index)
		{
			InnerRemoveAt(CheckIndex(NormalizeIndex(index)));
			return this;
		}

		private void EnsureCapacity(int minCapacity)
		{
			if (minCapacity > _items.Length)
			{
				T[] array = NewArray(minCapacity);
				Array.Copy(_items, 0, array, 0, _count);
				_items = array;
			}
		}

		private T[] NewArray(int minCapacity)
		{
			int val = Math.Max(1, _items.Length) * 2;
			return new T[Math.Max(val, minCapacity)];
		}

		private void InnerRemoveAt(int index)
		{
			_count--;
			_items[index] = default(T);
			if (index != _count)
			{
				Array.Copy(_items, index + 1, _items, index, _count - index);
			}
		}

		private bool InnerRemove(T item)
		{
			int num = IndexOf(item);
			if (num != -1)
			{
				InnerRemoveAt(num);
				return true;
			}
			return false;
		}

		private int CheckIndex(int index)
		{
			if (index >= _count)
			{
				throw new IndexOutOfRangeException();
			}
			return index;
		}

		private int NormalizeIndex(int index)
		{
			return (index >= 0) ? index : (index + _count);
		}

		private static T Coerce(object value)
		{
			if (value is T)
			{
				return (T)value;
			}
			return (T)RuntimeServices.Coerce(value, typeof(T));
		}
	}
}
