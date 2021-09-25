using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Game
{
    public struct PlayerInputData : IDarkRiftSerializable
    {
        public InputsContainer Inputs { get; private set; }
        public Quaternion LookDirection { get; private set; }
        public uint Time { get; private set; }
        public PlayerInputData(InputsContainer inputs, Quaternion lookDirection, uint time)
        {
            Inputs = inputs;
            LookDirection = lookDirection;
            Time = time;
        }
        public void Deserialize(DeserializeEvent e)
        {
            Inputs = e.Reader.ReadSerializable<InputsContainer>();
            LookDirection = new Quaternion(
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle());
            if (Inputs.LeftClick)
            {
                Time = e.Reader.ReadUInt32();
            }
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Inputs);
            e.Writer.Write(LookDirection.x);
            e.Writer.Write(LookDirection.y);
            e.Writer.Write(LookDirection.z);
            e.Writer.Write(LookDirection.w);

            if (Inputs.LeftClick)
            {
                e.Writer.Write(Time);
            }
        }
    }
}
