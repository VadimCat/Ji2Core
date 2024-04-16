using UnityEngine;

namespace Ji2.Tools.Layer
{
 public class Layer
 {
  public readonly int Mask;

  public static Layer FromId(int id)
  {
   return new Layer(1 << id);
  }

  public static Layer FromName(string name)
  {
   return FromId(LayerMask.NameToLayer(name));
  }

  public Layer(int mask)
  {
   Mask = mask;
  }
 }
}