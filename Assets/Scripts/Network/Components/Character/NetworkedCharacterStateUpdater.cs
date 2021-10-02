using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersShared.GameCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Network.Components.Character
{
    public class NetworkedCharacterStateUpdater : IInitializable, IDisposable, IEntityComponentHandler<CharacterStateData>
    {
        public uint Id { get; }

        private readonly IGameComponentContainer<CharacterStateData> _container;

        public NetworkedCharacterStateUpdater(IGameComponentContainer<CharacterStateData> container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.Add(this);
        }
        public void Dispose()
        {
            _container.Remove(this);
        }

        public void Handle(CharacterStateData state)
        {
            Debug.Log(state.Id);
        }

        

        
    }
}
