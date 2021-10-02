using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.GameLoop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.GameCycle
{
    public class NetworkGameStateUpdater : FixedUpdatableObject
    {
        private readonly IGameStateBufferGetter _bufferGetter;
        private readonly NetworkGameStateDistributor _gameStateDistributor;
        public NetworkGameStateUpdater(IGameStateBufferGetter bufferGetter, NetworkGameStateDistributor gameStateDistributor)
        {
            _bufferGetter = bufferGetter;
            _gameStateDistributor = gameStateDistributor;
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            var gameStates = _bufferGetter.Get();
            foreach (var gameState in gameStates)
            {
                _gameStateDistributor.Distribute(gameState);
            }
        }
    }
}
