using System;

namespace Ji2.Collisions
{
    public interface ICollidable<TCollisionData>
    {
        public event Action<TCollisionData> CollisionEnter;
        public event Action<TCollisionData> CollisionStay;
        public event Action<TCollisionData> CollisionExit;
        public void EnableSimulation(bool isEnabled);
    }
}