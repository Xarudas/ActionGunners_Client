using DarkRift;
using MeatInc.ActionGunnersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeatInc.ActionGunnersClient
{
    public class LobbyManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _roomListContainerTransform;

        [SerializeField]
        private GameObject _roomListPrefab;

        private void Start()
        {
            ConnectionManager.Instance.Client.MessageReceived += OnMessage;
            RefreshRooms(ConnectionManager.Instance.LobbyInfoData);
        }

        

        private void OnDestroy()
        {
            ConnectionManager.Instance.Client.MessageReceived -= OnMessage;
        }

        

        private void OnMessage(object sender, DarkRift.Client.MessageReceivedEventArgs e)
        {
            using(Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Lobby.LobbyJoinRoomDenied:
                        OnRoomJoinDenied(message.Deserialize<LobbyInfoData>());
                        break;
                    case Tags.Lobby.LobbyJoinRoomAccepted:
                        OnRoomJoinAccepted();
                        break;
                    default:
                        break;
                }
            }
        }

        private void RefreshRooms(LobbyInfoData lobbyInfoData)
        {
            RoomListObject[] roomObjects = _roomListContainerTransform.GetComponentsInChildren<RoomListObject>();

            if (roomObjects.Length > lobbyInfoData.Rooms.Length)
            {
                for (int i = lobbyInfoData.Rooms.Length; i < roomObjects.Length; i++)
                {
                    Destroy(roomObjects[i].gameObject);
                }
            }
            for (int i = 0; i < lobbyInfoData.Rooms.Length; i++)
            {
                RoomData d = lobbyInfoData.Rooms[i];
                if (i < roomObjects.Length)
                {
                    roomObjects[i].Set(this, d);
                }
                else
                {
                    GameObject go = Instantiate(_roomListPrefab, _roomListContainerTransform);
                    go.GetComponent<RoomListObject>().Set(this, d);
                }
            }
        }
        public void SendJoinRoomRequest(string roomName)
        {
            using (Message message = Message.Create(Tags.Lobby.LobbyJoinRoomRequest, new JoinRoomRequest(roomName)))
            {
                ConnectionManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }

        public void OnRoomJoinDenied(LobbyInfoData data)
        {
            RefreshRooms(data);
        }
        public void OnRoomJoinAccepted()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
