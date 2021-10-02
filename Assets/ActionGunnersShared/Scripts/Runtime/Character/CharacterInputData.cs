using DarkRift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Character
{
    public struct CharacterInputData : IDarkRiftSerializable
    {
        public InputsContainer Inputs { get; private set; }
        public Vector2 MoveAxis { get; private set; }
        public Vector2 LookEulerAngels { get; private set; }
        public ushort Time { get; private set; }
        public CharacterInputData(InputsContainer inputs, Vector2 moveAxis, Vector2 lookEulerAngels, ushort time)
        {
            Inputs = inputs;
            MoveAxis = moveAxis;
            LookEulerAngels = lookEulerAngels;
            Time = time;
        }
        public void Deserialize(DeserializeEvent e)
        {
            Inputs = e.Reader.ReadSerializable<InputsContainer>();
            MoveAxis = e.Reader.ReadVector2();
            LookEulerAngels = e.Reader.ReadVector2();

            if (Inputs.PrimaryAction)
            {
                Time = e.Reader.ReadUInt16();
            }
        }
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Inputs);
            e.Writer.WriteVector2(MoveAxis);
            e.Writer.WriteVector2(LookEulerAngels);
            if (Inputs.PrimaryAction)
            {
                e.Writer.Write(Time);
            }
        }
    }
}
