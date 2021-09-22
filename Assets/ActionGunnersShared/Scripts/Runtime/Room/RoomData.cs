using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Room
{
    public struct RoomData : IDarkRiftSerializable
    {
        public ushort Id { get; private set; }
        public string Name { get; private set; }
        public ushort MaxSlots { get; private set; }
        public ushort Slots { get; set; }

        public RoomData(ushort id, string name, ushort maxSlots)
        {
            Id = id;
            Name = name;
            MaxSlots = maxSlots;
            Slots = 0;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
            MaxSlots = e.Reader.ReadUInt16();
            Slots = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(MaxSlots);
            e.Writer.Write(Slots);
        }
    }
}