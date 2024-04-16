using UnityEngine;

namespace Ji2.Context.Context
{
 public class Entity: MonoBehaviour, IDependenciesProvider
 {
  public class Factory
  {
   public Entity Create(GameObject gameObject, DiContext localContext)
   {
    Entity entity = gameObject.AddComponent<Entity>();

    entity.Construct(localContext);
    return entity;
   }
  }
  
  private DiContext _localContext;

  private void Construct(DiContext localContext)
  {
   _localContext = localContext;
  }

  public TContract Get<TContract>()
  {
   return _localContext.Get<TContract>();
  }

  public bool TryGetService<TContract>(out TContract result)
  {
   return _localContext.TryGetService(out result);
  }
 }
}