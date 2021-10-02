using MeatInc.ActionGunnersClient.GameCycle.GameSystems;
using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.GameCycle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.GameCycle
{
    public class NetworkGameStateDistributor
    {
        private readonly NetworkedCharacterStatesUpdater _statesUpdater;
        public NetworkGameStateDistributor(NetworkedCharacterStatesUpdater statesUpdater)
        {
            _statesUpdater = statesUpdater;
        }

        public void Distribute(GameStateData gameState)
        {
            HandleStates(gameState.StateDatas, _statesUpdater);
        }

        private void HandleStates<TStates, TGameSystem>(TStates[] states, TGameSystem gameSystem)
            where TGameSystem : IGameSystemHandler<TStates>
        {
            gameSystem.HandleStates(states);
        }
    }
}
