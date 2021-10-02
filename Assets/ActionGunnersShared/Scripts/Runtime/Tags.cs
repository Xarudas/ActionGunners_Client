using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared
{
    public static class Tags 
    {
        public const ushort SpawnCharacter = 0;
        public const ushort DespawnCharacter = 1;
        public const ushort PlayerDisconnected = 2;
        public const ushort PlayerInput = 3;
        public const ushort GameStateResponse = 4;
    }
}
