using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared
{
    public struct PlayerInputData : IDarkRiftSerializable
    {
        public bool[] KeyInputs; // 0 = w, 1 = a, 2 = s, 3 = d, 4 = space, 5 = leftClick
        public Quaternion LookDirection;
        public uint Time;

        public PlayerInputData(bool[] keyInputs, Quaternion lookDirection, uint time)
        {
            KeyInputs = keyInputs;
            LookDirection = lookDirection;
            Time = time;
        }

        public void Deserialize(DeserializeEvent e)
        {
            KeyInputs = e.Reader.ReadBooleans();
            LookDirection = new Quaternion(
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle(),
                e.Reader.ReadSingle());

            if (KeyInputs[5])
            {
                Time = e.Reader.ReadUInt32();
            }
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(KeyInputs);
            e.Writer.Write(LookDirection.x);
            e.Writer.Write(LookDirection.y);
            e.Writer.Write(LookDirection.z);
            e.Writer.Write(LookDirection.w);

            if (KeyInputs[5])
            {
                e.Writer.Write(Time);
            }
        }
    }
}
