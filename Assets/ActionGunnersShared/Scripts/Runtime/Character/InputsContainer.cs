using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Character
{
    public struct InputsContainer : IDarkRiftSerializable
    {
        public bool PrimaryAction;
        public bool Jump;
        public void Deserialize(DeserializeEvent e)
        {
            PrimaryAction = e.Reader.ReadBoolean();
            Jump = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(PrimaryAction);
            e.Writer.Write(Jump);
        }
    }
}
