using DarkRift;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersSharedLegacy
{
    public struct LoginRequestData : IDarkRiftSerializable
    {
        public string Login;
        public LoginRequestData(string login)
        {
            Login = login;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Login = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Login);
        }
    }
}
