using MeatInc.ActionGunnersShared.Room;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient.RoomSystem
{
    public class RoomManagerViewer : MonoBehaviour
    {
        [SerializeField]
        private RoomManager _roomManager;
        [SerializeField]
        private GameObject _roomPrefab;
        [SerializeField]
        private Transform _containerRooms;
        [SerializeField]
        private Button _buttonRefreshRooms;
        [SerializeField]
        private Button _buttonCreateRoom;

        private List<GameObject> _rooms = new List<GameObject>();

        private void Awake()
        {
            _roomManager.RefreshedRooms += OnRefreshRooms;
            _buttonRefreshRooms.onClick.AddListener(RefreshRooms);
            _buttonCreateRoom.onClick.AddListener(CreateRoom);
        }

        

        private void OnDestroy()
        {
            _roomManager.RefreshedRooms -= OnRefreshRooms;
            _buttonRefreshRooms.onClick.RemoveListener(RefreshRooms);
            _buttonCreateRoom.onClick.RemoveListener(CreateRoom);
        }

        private void OnRefreshRooms(List<RoomData> rooms)
        {
            DestroyRooms();
            CreateRoomsObjects(rooms);
        }

        private void RefreshRooms()
        {
            _roomManager.Refresh();
        }
        private void CreateRoom()
        {
            _roomManager.Create();
        }
        private void DestroyRooms()
        {
            foreach (var room in _rooms)
            {
                Destroy(room);
            }
        }
        private void CreateRoomsObjects(List<RoomData> rooms)
        {
            if (rooms != null)
            {
                foreach (var room in rooms)
                {
                    CreateRoomObject(room);
                }
            }
        }
        private void CreateRoomObject(RoomData data)
        {
            var roomObject = Instantiate(_roomPrefab, _containerRooms);
            _rooms.Add(roomObject);
            var room = roomObject.GetComponent<Room>();
            room.UpdateData(data);
        }
    }
}
