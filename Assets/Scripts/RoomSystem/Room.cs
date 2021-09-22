using MeatInc.ActionGunnersShared.Room;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.RoomSystem
{
    public class Room : MonoBehaviour
    {
        public event Action<RoomData> RoomUpdated;
        private RoomData _data;
        public void UpdateData(RoomData data)
        {
            _data = data;
            RoomUpdated?.Invoke(data);
        }
        public void Join()
        {
            RoomJoiner roomJoiner = new RoomJoiner(_data);
            roomJoiner.Join();
        }
    }
}
