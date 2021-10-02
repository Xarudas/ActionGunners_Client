using MeatInc.ActionGunnersShared.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.Interfaces.GameCycle
{
    public interface IGetter<T>
    {
        int Count { get; }
        T[] Get();
    }

    public interface IGameStateBufferGetter : IGetter<GameStateData> { }
}
