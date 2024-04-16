using System.Collections;

namespace Ji2Core.DataTypes.Array
{
 public class Array2D<T>: IArray<T>
 {
  private IArray<T> _array;

  public Array2D(IArray<T> array)
  {
   _array = array;
  }

  public IEnumerator GetEnumerator()
  {
   return _array.GetEnumerator();
  }

  public void CopyTo(System.Array array, int index)
  {
   _array.CopyTo(array, index);
  }

  public int Count => _array.Count;

  public bool IsSynchronized => _array.IsSynchronized;

  public object SyncRoot => _array.SyncRoot;

  public int Add(object value)
  {
   return _array.Add(value);
  }

  public void Clear()
  {
   _array.Clear();
  }

  public bool Contains(object value)
  {
   return _array.Contains(value);
  }

  public int IndexOf(object value)
  {
   return _array.IndexOf(value);
  }

  public void Insert(int index, object value)
  {
   _array.Insert(index, value);
  }

  public void Remove(object value)
  {
   _array.Remove(value);
  }

  public void RemoveAt(int index)
  {
   _array.RemoveAt(index);
  }

  public bool IsFixedSize => _array.IsFixedSize;

  public bool IsReadOnly => _array.IsReadOnly;

  public object this[int index]
  {
   get => _array[index];
   set => _array[index] = value;
  }

  public int CompareTo(object other, IComparer comparer)
  {
   return _array.CompareTo(other, comparer);
  }

  public bool Equals(object other, IEqualityComparer comparer)
  {
   return _array.Equals(other, comparer);
  }

  public int GetHashCode(IEqualityComparer comparer)
  {
   return _array.GetHashCode(comparer);
  }

  public object Clone()
  {
   return _array.Clone();
  }

  public T ElementAt(params int[] dimensions)
  {
   return _array.ElementAt(dimensions);
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
 }
}