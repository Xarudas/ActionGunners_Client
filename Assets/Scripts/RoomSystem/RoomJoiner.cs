using DarkRift;
using MeatInc.ActionGunnersShared;
using MeatInc.ActionGunnersShared.Room;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.RoomSystem
{
    public class RoomJoiner 
    {
        private RoomData _data;
        public RoomJoiner(RoomData data)
        {
            _data = data;
        }

        public void Join()
        {
            using (Message message = Message.Create(Tags.Room.JoinRequest, new JoinRoomRequest(_data.Id)))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
    }
}
