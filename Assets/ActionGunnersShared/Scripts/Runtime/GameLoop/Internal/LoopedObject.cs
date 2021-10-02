using MeatInc.ActionGunnersShared.Relays;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersShared.GameLoop.Internal
{
    public abstract class LoopedObject<TLoop> : IInitializable, IDisposable
    {
        protected TLoop GameLoop { get; private set; }
        protected IRelay Relay { get; private set; }

        protected virtual bool DefaultSubscribe { get; } = true;
        private bool _isSubscribed = false;


        [Inject]
        public void Construct(TLoop gameLoop, IRelay relay)
        {
            GameLoop = gameLoop;
            Relay = relay;
        }
        public virtual void Initialize()
        {
            if (DefaultSubscribe)
            {
                Relay.OnEnableEvt += SubscribeLoop;
                Relay.OnDisableEvt += UnsubscribeLoop;

                if (Relay.IsActive)
                {
                    SubscribeLoop();
                }
            }
        }
        public virtual void Dispose()
        {
            if (DefaultSubscribe)
            {
                Relay.OnEnableEvt -= SubscribeLoop;
                Relay.OnDisableEvt -= UnsubscribeLoop;
            }

            UnsubscribeLoop();
        }
        protected void UnsubscribeLoop()
        {
            if (_isSubscribed == true)
            {
                UnsubscribeLoopInternal();
                _isSubscribed = false;
            }
        }

        protected void SubscribeLoop()
        {
            if (_isSubscribed == false)
            {
                SubscribeLoopInternal();
                _isSubscribed = true;
            }
        }

        protected abstract void SubscribeLoopInternal();
        protected abstract void UnsubscribeLoopInternal();
    }
}
