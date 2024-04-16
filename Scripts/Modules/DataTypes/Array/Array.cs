using System;
using System.Collections;

namespace Ji2Core.DataTypes.Array
{
 public class Array<T>: IArray<T>
 {
  private readonly System.Array _array;

  public Array(System.Array array)
  {
   _array = array;
   if (GetElementType() != typeof(T))
   {
    throw new InvalidCastException();
   }
  }
  
  public Array(params int[] lengths) : this(System.Array.CreateInstance(typeof(T), lengths))
  { }
  
  public Type GetElementType()
  {
   return _array.GetType().GetElementType();
  }
  
  public T ElementAt(params int[] dimensions)
  {
   return (T)_array.GetValue(dimensions);
  }

  public void SetValue(T value, params int[] dimensions)
  {
   _array.SetValue(value, dimensions);
  }

  public int Rank => _array.Rank;
  public int GetLength(int dimension)
  {
   return _array.GetLength(dimension);
  }

  public IEnumerator GetEnumerator()
  {
   return _array.GetEnumerator();
  }

  public void CopyTo(System.Array array, int index)
  {
   _array.CopyTo(array, index);
  }

  public int Count => ((ICollection)_array).Count;

  public bool IsSynchronized => _array.IsSynchronized;

  public object SyncRoot => _array.SyncRoot;

  public int Add(object value)
  {
   return ((IList)_array).Add(value);
  }

  public void Clear()
  {
   ((IList)_array).Clear();
  }

  public bool Contains(object value)
  {
   return ((IList)_array).Contains(value);
  }

  public int IndexOf(object value)
  {
   return ((IList)_array).IndexOf(value);
  }

  public void Insert(int index, object value)
  {
   ((IList)_array).Insert(index, value);
  }

  public void Remove(object value)
  {
   ((IList)_array).Remove(value);
  }

  public void RemoveAt(int index)
  {
   ((IList)_array).RemoveAt(index);
  }

  public bool IsFixedSize => _array.IsFixedSize;

  public bool IsReadOnly => _array.IsReadOnly;

  public object this[int index]
  {
   get => ((IList)_array)[index];
   set => ((IList)_array)[index] = value;
  }
  
  public int CompareTo(object other, IComparer comparer)
  {
   return ((IStructuralComparable)_array).CompareTo(other, comparer);
  }

  public bool Equals(object other, IEqualityComparer comparer)
  {
   return ((IStructuralEquatable)_array).Equals(other, comparer);
  }

  public int GetHashCode(IEqualityComparer comparer)
  {
   return ((IStructuralEquatable)_array).GetHashCode(comparer);
  }

  public object Clone()
  {
   return _array.Clone();
  }
 }
}