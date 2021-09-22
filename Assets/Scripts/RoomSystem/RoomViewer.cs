using MeatInc.ActionGunnersShared.Room;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient.RoomSystem
{
    [RequireComponent(typeof(Room))]
    public class RoomViewer : MonoBehaviour
    {
        [SerializeField]
        private Button _buttonJoin;
        [SerializeField]
        private Text _textNameRoom;
        [SerializeField]
        private Text _textSlots;

        private Room _room;

        private void Awake()
        {
            _room = GetComponent<Room>();
            _buttonJoin.onClick.AddListener(Join);
            _room.RoomUpdated += OnRoomUpdate;
        }
        private void OnDestroy()
        {
            _buttonJoin.onClick.RemoveListener(Join);
            _room.RoomUpdated -= OnRoomUpdate;
        }
        private void OnRoomUpdate(RoomData data)
        {
            _textNameRoom.text = data.Name;
            _textSlots.text = $"{data.Slots}/{data.MaxSlots}";
        }
        private void Join()
        {
            _room.Join();
        }
    }
}
