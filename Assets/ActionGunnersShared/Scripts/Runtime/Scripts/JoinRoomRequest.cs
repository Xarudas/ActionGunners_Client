using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared {
    public struct JoinRoomRequest : IDarkRiftSerializable
    {

        public string RoomName;

        public JoinRoomRequest(string name)
        {
            RoomName = name;
        }
        public void Deserialize(DeserializeEvent e)
        {
            RoomName = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(RoomName);
        }
    }
}
