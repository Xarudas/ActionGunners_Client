namespace MeatInc.ActionGunnersSharedLegacy
{
    public static class Tags
    {
        public static class Login
        {
            public const ushort LoginRequest = 0;
            public const ushort LoginRequestAccepted = 1;
            public const ushort LoginRequestDenied = 2;
        }

        public static class Lobby
        {
            public const ushort LobbyJoinRoomRequest = 100;
            public const ushort LobbyJoinRoomDenied = 101;
            public const ushort LobbyJoinRoomAccepted = 102;
        }

        public static class Game
        {
            public const ushort GameJoinRequest = 200;
            public const ushort GameStartDataResponse = 201;
            public const ushort GameUpdate = 202;
            public const ushort GamePlayerInput = 203;
            
        }
    }
}
