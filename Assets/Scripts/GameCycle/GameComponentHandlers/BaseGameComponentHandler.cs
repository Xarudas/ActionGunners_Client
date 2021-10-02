using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.GameCycle.GameComponentHandlers
{
    public abstract class BaseGameComponentHandler<TState> : IGameComponentHandler<TState>
    {
        public abstract void Handle(TState state);
    }

    public abstract class BaseEntityGameComponentHandler<TState> : BaseGameComponentHandler<TState>, IEntityComponentHandler<TState>
    {
        public uint Id { get; }
        protected readonly IEntityComponentContainer<TState> Container;

    }
}
