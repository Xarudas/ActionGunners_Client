using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace MeatInc.ActionGunnersShared.Game
{
    public struct PlayerStateData : IDarkRiftSerializable
    {
        public ushort Id;
        public Vector3 Position;
        public float Gravity;
        public Quaternion LookDirection;

        public PlayerStateData(ushort id, float gravity, Vector3 position, Quaternion lookDirection)
        {
            Id = id;
            Position = position;
            LookDirection = lookDirection;
            Gravity = gravity;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Position = new Vector3(e.Reader.ReadSingle(), e.Reader.ReadSingle(), e.Reader.ReadSingle());
            LookDirection = new Quaternion(e.Reader.ReadSingle(), e.Reader.ReadSingle(), e.Reader.ReadSingle(), e.Reader.ReadSingle());
            Id = e.Reader.ReadUInt16();
            Gravity = e.Reader.ReadSingle();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Position.x);
            e.Writer.Write(Position.y);
            e.Writer.Write(Position.z);

            e.Writer.Write(LookDirection.x);
            e.Writer.Write(LookDirection.y);
            e.Writer.Write(LookDirection.z);
            e.Writer.Write(LookDirection.w);
            e.Writer.Write(Id);
            e.Writer.Write(Gravity);
        }
    }
}
