using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IGameComponentContainer<TState> : IGameComponentContainer<IGameComponentHandler<TState>, TState>
    {

    }
    public interface IEntityComponentContainer<TState> : IGameComponentContainer<IEntityComponentHandler<TState>, TState>
    {

    }
    public interface IGameComponentContainer<TGameComponentHandler, TState> : IInserter<TGameComponentHandler>, IRemover<TGameComponentHandler>
        where TGameComponentHandler : IGameComponentHandler<TState>
    {
        IReadOnlyList<TGameComponentHandler> Components { get; }
    }

}
