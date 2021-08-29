using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared
{
    public struct GameStartData : IDarkRiftSerializable
    {
        public uint OnJoinServerTick;
        public PlayerSpawnData[] Players;

        public GameStartData(PlayerSpawnData[] players, uint serverTick)
        {
            Players = players;
            OnJoinServerTick = serverTick;
        }

        public void Deserialize(DeserializeEvent e)
        {
            OnJoinServerTick = e.Reader.ReadUInt32();
            Players = e.Reader.ReadSerializables<PlayerSpawnData>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(OnJoinServerTick);
            e.Writer.Write(Players);
        }
    }
}
