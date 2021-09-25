using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared.Game
{
    public struct InputsContainer : IDarkRiftSerializable
    {
        public bool W;
        public bool S;
        public bool A;
        public bool D;
        public bool Shift;
        public bool Space;
        public bool LeftClick;

        public void Deserialize(DeserializeEvent e)
        {
            W = e.Reader.ReadBoolean();
            S = e.Reader.ReadBoolean();
            A = e.Reader.ReadBoolean();
            D = e.Reader.ReadBoolean();
            Space = e.Reader.ReadBoolean();
            Shift = e.Reader.ReadBoolean();
            LeftClick = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(W);
            e.Writer.Write(S);
            e.Writer.Write(A);
            e.Writer.Write(D);
            e.Writer.Write(Space);
            e.Writer.Write(Shift);
            e.Writer.Write(LeftClick);
        }
    }
}
