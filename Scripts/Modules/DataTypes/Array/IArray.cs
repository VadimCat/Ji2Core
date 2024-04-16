namespace Ji2Core.DataTypes.Array
{
 public interface IArray<T> : System.Collections.ICollection, System.Collections.IEnumerable, System.Collections.IList,
  System.Collections.IStructuralComparable, System.Collections.IStructuralEquatable, System.ICloneable
 {
  public T ElementAt(params int[] dimensions);
  public void SetValue(T value, params int[] dimensions);
  public int Rank { get; }
  public int GetLength(int dimension);
 }

 public interface IArray2D<T> : IArray<T>
 {
  
  
  public T this[int x, int y]
  {
   get => ElementAt(x, y);
   set => SetValue(value, x, y);
  }
 }
}