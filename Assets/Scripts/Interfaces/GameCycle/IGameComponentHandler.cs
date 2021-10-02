using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IGameComponentHandler<TState>
    {
        void Handle(TState state);
    }
    public interface IEntityComponentHandler<TState> : IGameComponentHandler<TState>
    {
        public uint Id { get; }
    }
}
