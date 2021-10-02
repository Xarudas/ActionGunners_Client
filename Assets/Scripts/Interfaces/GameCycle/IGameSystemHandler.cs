using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IGameSystemHandler<TState>
    {
        void HandleStates(TState[] states);
    }
}
