using DarkRift;
using MeatInc.ActionGunnersClient.LobbySystem;
using MeatInc.ActionGunnersShared;
using MeatInc.ActionGunnersShared.Looby;
using MeatInc.ActionGunnersShared.Room;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MeatInc.ActionGunnersClient.RoomSystem
{
    public class RoomManager : MonoBehaviour
    {
        public event Action<List<RoomData>> RefreshedRooms;
        [SerializeField]
        private Lobby _lobby;

        private List<RoomData> _rooms = new List<RoomData>();

        private void Start()
        {
            _lobby.JoinedLobby += OnLoobyJoin;
            _lobby.LeavedLobby += OnLeftLobby;
        }
        private void OnDestroy()
        {
            _lobby.JoinedLobby -= OnLoobyJoin;
            _lobby.LeavedLobby -= OnLeftLobby;
        }

        public void Refresh()
        {
            using (Message message = Message.CreateEmpty(Tags.Room.RefreshRequest))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
        public void Create()
        {
            using (Message message = Message.Create(Tags.Room.CreateRequest, new CreateRoomRequest($"Room Client_{NetworkManager.Instance.Client.ID}", 20)))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void OnLoobyJoin(LobbyInfoData data)
        {
            RefreshRooms(data.RoomsData.Rooms);
            NetworkManager.Instance.Client.MessageReceived += OnMessage;
        }
        private void OnLeftLobby()
        {
            NetworkManager.Instance.Client.MessageReceived -= OnMessage;
        }
        private void RefreshRooms(RoomData[] rooms)
        {
            _rooms = rooms.ToList();
            RefreshedRooms?.Invoke(_rooms);
        }
        private void OnMessage(object sender, DarkRift.Client.MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Room.RefreshResponse:
                        RefreshRooms(message.Deserialize<RoomsInfoData>().Rooms);
                        break;
                    case Tags.Room.CreateAccept:
                        OnCreatedRoom(message.Deserialize<RoomData>());
                        break;
                    case Tags.Room.CreateDenied:
                        Debug.Log("CreateDenied");
                        break;
                    case Tags.Room.JoinAccept:
                        OnJoinRoom();
                        break;
                    case Tags.Room.JoinDenied:
                        Debug.Log("JoinDenied");
                        break;
                    default:
                        break;
                }
            }
        }
        private void OnJoinRoom()
        {
            SceneManager.LoadScene("Game");
        }
        private void OnCreatedRoom(RoomData roomData)
        {
            RoomJoiner roomJoiner = new RoomJoiner(roomData);
            roomJoiner.Join();
        }
    }
}
