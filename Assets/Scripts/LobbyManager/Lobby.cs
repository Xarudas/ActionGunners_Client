using UnityEngine;
using MeatInc.ActionGunnersShared;
using DarkRift.Client;
using System;
using DarkRift;
using MeatInc.ActionGunnersShared.Looby;

namespace MeatInc.ActionGunnersClient.LobbySystem
{
    public class Lobby : MonoBehaviour
    {
        public event Action<LobbyInfoData> JoinedLobby;
        public event Action JoinLobbyFailed;
        public event Action LeavedLobby;

        private void Start()
        {
            NetworkManager.Instance.Connected += Connect;
            NetworkManager.Instance.Disconnected += Disconnect;
        }
        private void OnDisable()
        {
            NetworkManager.Instance.Connected -= Connect;
            NetworkManager.Instance.Disconnected -= Disconnect;
            NetworkManager.Instance.Client.MessageReceived -= OnMessage;
        }
        private void Connect()
        {
            NetworkManager.Instance.Client.MessageReceived += OnMessage;
            ConnectToLobby();
        }
        private void Disconnect(DisconnectedEventArgs obj)
        {
            LeavedLobby?.Invoke();
        }
        private void ConnectToLobby()
        {
            using (Message message = Message.CreateEmpty(Tags.Looby.JoinRequest))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Looby.JoinAccepted:
                        JoinLobby(message.Deserialize<LobbyInfoData>());
                        break;
                    case Tags.Looby.JoinDenied:
                        JoinLobbyFail();
                        break;
                }
            }
        }

        private void JoinLobbyFail()
        {
            JoinLobbyFailed?.Invoke();
        }

        private void JoinLobby(LobbyInfoData loobyInfoData)
        {
            JoinedLobby?.Invoke(loobyInfoData);
        }
    }
}
