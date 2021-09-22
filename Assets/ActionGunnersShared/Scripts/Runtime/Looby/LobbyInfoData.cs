using DarkRift;
using MeatInc.ActionGunnersShared.Room;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Looby
{
    public struct LobbyInfoData : IDarkRiftSerializable
    {
        public ClientInfoData ClientData { get; private set; } 
        public RoomsInfoData RoomsData { get; private set; }

        public LobbyInfoData(ClientInfoData client, RoomsInfoData rooms)
        {
            ClientData = client;
            RoomsData = rooms;
        }

        public void Deserialize(DeserializeEvent e)
        {
            ClientData = e.Reader.ReadSerializable<ClientInfoData>();
            RoomsData = e.Reader.ReadSerializable<RoomsInfoData>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(ClientData);
            e.Writer.Write(RoomsData);
        }
    }
}
