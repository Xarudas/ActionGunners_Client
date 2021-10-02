using DarkRift;
using DarkRift.Client.Unity;
using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersClient.Network.Components;
using MeatInc.ActionGunnersShared;
using MeatInc.ActionGunnersShared.GameCycle;
using MeatInc.ActionGunnersShared.GameLoop;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.GameCycle
{
    public class NetworkGameStateReceiver : IInitializable, IDisposable
    {
        private readonly NetworkRelay _networkRelay;
        private readonly IGameStateBufferInserter _bufferInserter;

        public NetworkGameStateReceiver(NetworkRelay networkRelay, IGameStateBufferInserter bufferInserter)
        {
            _networkRelay = networkRelay;
            _bufferInserter = bufferInserter;
        }
        public  void Initialize()
        {
            _networkRelay.Subscribe(Tags.GameStateResponse, OnGameStateRecieved);
            
        }
        public void Dispose()
        {
            _networkRelay.Unsubscribe(Tags.GameStateResponse, OnGameStateRecieved);
        }

        private void OnGameStateRecieved(Message message)
        {
            var gameState = message.Deserialize<GameStateData>();

            _bufferInserter.Add(gameState);
        }
    }
}
