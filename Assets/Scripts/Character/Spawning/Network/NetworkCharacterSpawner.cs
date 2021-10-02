using DarkRift;
using DarkRift.Client.Unity;
using MeatInc.ActionGunnersClient.Network.Components;
using MeatInc.ActionGunnersShared;
using MeatInc.ActionGunnersShared.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace MeatInc.ActionGunnersClient.Character.Spawning.Network
{
    public class NetworkCharacterSpawner : IInitializable, IDisposable
    {
        private readonly CharacterSpawner _characterSpawner;
        private readonly NetworkRelay _networkRelay;
        private readonly UnityClient _client;

        public NetworkCharacterSpawner(
            CharacterSpawner characterSpawner,
            NetworkRelay networkRelay,
            UnityClient client)
        {
            _characterSpawner = characterSpawner;
            _client = client;
            _networkRelay = networkRelay;
        }
        public void Initialize()
        {
            _networkRelay.Subscribe(Tags.SpawnCharacter, OnSpawnCharacter);
            _networkRelay.Subscribe(Tags.DespawnCharacter, OnDespawnCharacter);
        }
        public void Dispose()
        {
            _networkRelay.Unsubscribe(Tags.SpawnCharacter, OnSpawnCharacter);
            _networkRelay.Unsubscribe(Tags.DespawnCharacter, OnDespawnCharacter);
        }
        private void OnDespawnCharacter(Message message)
        {
            var despawnData = message.Deserialize<CharacterDespawnData>();
            _characterSpawner.Despawn(despawnData.ClientId);
        }
        private void OnSpawnCharacter(Message message)
        {
            var spawnData = message.Deserialize<CharacterSpawnData>();

            _characterSpawner.Spawn(spawnData);
        }
    }
}
