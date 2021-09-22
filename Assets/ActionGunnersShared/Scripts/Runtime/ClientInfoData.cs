using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared
{
    public struct ClientInfoData : IDarkRiftSerializable
    {
        public ushort Id { get; private set; }
        public string Name { get; private set; }

        public ClientInfoData(ushort id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
        }
    }
}
