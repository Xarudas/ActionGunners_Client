using DarkRift;
using DarkRift.Client;
using MeatInc.ActionGunnersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Game
{
    public class GameManager : MonoBehaviour
    {


        public uint ClientTick { get; private set; }
        public uint LastReceivedServerTick { get; private set; }
        private void Start()
        {
            NetworkManager.Instance.Client.MessageReceived += OnMessage;
            using (Message message = Message.CreateEmpty(Tags.Game.JoinRequest))
            {
                NetworkManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void OnDestroy()
        {
            NetworkManager.Instance.Client.MessageReceived -= OnMessage;
        }

        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Game.StartDataResponse:
                        OnJoinGame();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnJoinGame()
        {
            
        }
    }
}
