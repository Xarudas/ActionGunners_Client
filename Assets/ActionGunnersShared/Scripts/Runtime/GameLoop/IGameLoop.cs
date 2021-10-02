using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.GameLoop
{
    public interface IGameLoop 
    {
        bool IsPaused { get; set; }
        void Subscribe(IBaseUpdatable updatable);
        void Unsubscribe(IBaseUpdatable updatable);
    }
}
