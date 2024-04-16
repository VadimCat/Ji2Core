namespace Ji2Core.DataTypes.Array
{
 public abstract class Array1D<T> : Array<T>
 {
  public T this[int x]
  {
   get => ElementAt(x);
   set => SetValue(value, x);
  }
 }
}