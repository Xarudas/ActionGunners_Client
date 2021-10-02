using DarkRift;
using MeatInc.ActionGunnersShared.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.GameCycle
{
    public struct GameStateData : IDarkRiftSerializable
    {
        public CharacterStateData[] StateDatas { get; private set; }

        public GameStateData(CharacterStateData[] stateDatas)
        {
            StateDatas = stateDatas;
        }
        public void Deserialize(DeserializeEvent e)
        {
           
        }
        public void Serialize(SerializeEvent e)
        {
            
        }
    }
}
