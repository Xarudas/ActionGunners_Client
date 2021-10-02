using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using Assets.ActionGunnersShared.Scripts.Runtime.Interfaces;

namespace MeatInc.ActionGunnersShared.Character
{
    public struct CharacterStateData : IDarkRiftSerializable, IState
    {
        public ushort Id { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }
        public float Gravity { get; private set; }
        

        public CharacterStateData(ushort id, float gravity, Vector3 position, Vector3 rotation)
        {
            Id = id;
            Position = position;
            Rotation = rotation;
            Gravity = gravity;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Position = e.Reader.ReadVector3();
            Rotation = e.Reader.ReadVector3();
            Gravity = e.Reader.ReadSingle();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.WriteVector3(Position);
            e.Writer.WriteVector3(Rotation);
            e.Writer.Write(Gravity);
        }
    }
}
