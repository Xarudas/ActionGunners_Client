using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.Room
{
    public struct CreateRoomRequest : IDarkRiftSerializable
    {
        public string Name { get; private set; }
        public ushort MaxSlots { get; private set; }

        public CreateRoomRequest(string name, ushort maxSlots)
        {
            Name = name;
            MaxSlots = maxSlots;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Name = e.Reader.ReadString();
            MaxSlots = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Name);
            e.Writer.Write(MaxSlots);
        }
    }
}
