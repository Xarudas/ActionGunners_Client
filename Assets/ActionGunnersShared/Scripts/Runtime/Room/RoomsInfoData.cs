using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.Room
{
    public struct RoomsInfoData : IDarkRiftSerializable
    {
        public RoomData[] Rooms { get; private set; }

        public RoomsInfoData(RoomData[] rooms)
        {
            Rooms = rooms;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Rooms = e.Reader.ReadSerializables<RoomData>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Rooms);
        }
    }
}
