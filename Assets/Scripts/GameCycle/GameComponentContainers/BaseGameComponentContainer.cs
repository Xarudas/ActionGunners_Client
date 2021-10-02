using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.GameCycle.GameComponentsContainers
{
    public class BaseGameComponentContainer<TGameComponentHandler, TState> : IGameComponentContainer<TGameComponentHandler, TState>
        where TGameComponentHandler : IGameComponentHandler<TState>
    {
        public IReadOnlyList<TGameComponentHandler> Components => _components;

        private readonly List<TGameComponentHandler> _components = new List<TGameComponentHandler>();

        public void Add(TGameComponentHandler element)
        {
            _components.Add(element);
        }

        public void Remove(TGameComponentHandler element)
        {
            _components.Remove(element);
        }
    }
    public class GameComponentContainer<TState> : BaseGameComponentContainer<IGameComponentHandler<TState>, TState>, IGameComponentContainer<TState>
    {
    }
    public class EntityComponentContainer<TState> : BaseGameComponentContainer<IEntityComponentHandler<TState>, TState>, IEntityComponentContainer<TState>
    {

    }
}
