using MeatInc.ActionGunnersShared.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IInserter<T>
    {
        void Add(T element);
    }
    public interface IGameStateBufferInserter : IInserter<GameStateData> { }
}
