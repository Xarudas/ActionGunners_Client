using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.GameCycle;
using MeatInc.ActionGunnersShared.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersClient.GameCycle
{
    public class NetworkGameStateBuffer : IGameStateBufferInserter, IGameStateBufferGetter
    {
        public int Count => _buffer.Count; 

        private readonly Buffer<GameStateData> _buffer;

        public NetworkGameStateBuffer(Settings settings)
        {
            _buffer = new Buffer<GameStateData>(settings.BufferSize, settings.CorrectionTollerance);
        }

        public void Add(GameStateData element)
        {
            _buffer.Add(element);
        }

        public GameStateData[] Get()
        {
            return _buffer.Get();
        }

        public class Settings
        {
            public int BufferSize = 1;
            public int CorrectionTollerance = 1;
        }
    }
}
