using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersShared
{
    public static class Tags 
    {
        public static class Looby
        {
            public const ushort JoinRequest = 1;
            public const ushort JoinAccepted = 2;
            public const ushort JoinDenied = 3;
        }

        public static class Room
        {
            public const ushort RefreshRequest = 100;
            public const ushort RefreshResponse = 101;
            public const ushort CreateRequest = 102;
            public const ushort CreateAccept = 103;
            public const ushort CreateDenied = 104;
            public const ushort JoinRequest = 105;
            public const ushort JoinAccept = 106;
            public const ushort JoinDenied = 107;
            public const ushort LeaveRequest = 108;
            public const ushort LeaveResponse = 109;
        }
        public static class Game
        {
            public const ushort JoinRequest = 200;
        }
    }
}
