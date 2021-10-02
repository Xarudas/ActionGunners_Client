using MeatInc.ActionGunnersShared.GameLoop.Internal;
using MeatInc.ActionGunnersShared.Relays;

namespace MeatInc.ActionGunnersShared.GameLoop
{
    public abstract class UpdatableObject : GlobalLoopedObject, IUpdatable
    {
        protected override void SubscribeLoopInternal() => GameLoop.Subscribe(this);
        protected override void UnsubscribeLoopInternal() => GameLoop.Unsubscribe(this);
        public abstract void OnUpdate(float deltaTime);
    }

    public abstract class FixedUpdatableObject : GlobalLoopedObject, IFixedUpdatable
    {
        protected override void SubscribeLoopInternal() => GameLoop.Subscribe(this);
        protected override void UnsubscribeLoopInternal() => GameLoop.Unsubscribe(this);
        public abstract void OnFixedUpdate(float deltaTime);
    }

    public abstract class LateUpdatableObject : GlobalLoopedObject, ILateUpdatable
    {
        protected override void SubscribeLoopInternal() => GameLoop.Subscribe(this);
        protected override void UnsubscribeLoopInternal() => GameLoop.Unsubscribe(this);
        public abstract void OnLateUpdate(float deltaTime);
    }
}
