using System.Collections;
using System.Collections.Generic;
using Ji2Core.DataTypes.Array;
using UnityEngine;

namespace Ji2Core.DataTypes
{
 public class Included2DArrayIndexes: IReadOnlyList<Vector2Int>
 {
  public readonly IArray2D<bool> ExceptMask;
  private readonly IReadOnlyList<Vector2Int> _availableElements;
  
  public Vector2Int this[int i] => _availableElements[i];

  public int Count => _availableElements.Count;

  public Included2DArrayIndexes(IArray2D<bool> exceptMask)
  {
   ExceptMask = exceptMask;
   int capacity = exceptMask.GetLength(0) * exceptMask.GetLength(1);

   List<Vector2Int> availableElements = new List<Vector2Int>(capacity);
   for (int row = 0; row < exceptMask.GetLength(0); row++)
   for (int column = 0; column < exceptMask.GetLength(1); column++)
   {
    if (!exceptMask[row, column])
    {
     availableElements.Add(new Vector2Int(row, column));
    }
   }
   
   _availableElements = availableElements;
  }

  public IEnumerator<Vector2Int> GetEnumerator()
  {
   return _availableElements.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
   return ((IEnumerable)_availableElements).GetEnumerator();
  }
 }
}