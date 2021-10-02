using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Character
{
    public struct CharacterSpawnData : IDarkRiftSerializable
    {
        public ushort Id { get; private set; }
        public Vector3 Position { get; private set; }
        public bool IsLocal { get; private set; }

        public CharacterSpawnData(ushort id, Vector3 position, bool isLocal)
        {
            Id = id;
            Position = position;
            IsLocal = isLocal;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Position = e.Reader.ReadVector3();
            IsLocal = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.WriteVector3(Position);
            e.Writer.Write(IsLocal);
        }
    }
}
