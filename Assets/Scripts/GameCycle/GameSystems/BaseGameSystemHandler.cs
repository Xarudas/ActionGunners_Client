using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.GameCycle.GameSystems
{
    
    public abstract class BaseGameSystemHandler<TState> : IGameSystemHandler<TState>
    {
        public abstract void HandleStates(TState[] states);
    }
    public abstract class BaseEntitySystemHandler<TState> : BaseGameSystemHandler<TState>
    {
        protected readonly IEntityComponentContainer<TState> Container;
        public BaseEntitySystemHandler(IEntityComponentContainer<TState> container)
        {
            Container = container;
        }
    }
}
