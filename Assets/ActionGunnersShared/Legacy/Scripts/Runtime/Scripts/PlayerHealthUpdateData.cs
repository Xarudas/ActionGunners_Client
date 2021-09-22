using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersSharedLegacy
{
    public struct PlayerHealthUpdateData : IDarkRiftSerializable
    {
        public ushort PlayerId;
        public byte Value;

        public PlayerHealthUpdateData(ushort id, byte value)
        {
            PlayerId = id;
            Value = value;
        }
        
        public void Deserialize(DeserializeEvent e)
        {
            PlayerId = e.Reader.ReadUInt16();
            Value = e.Reader.ReadByte();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(PlayerId);
            e.Writer.Write(Value);
        }
    }
}
