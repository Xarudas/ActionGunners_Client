using DarkRift.Client.Unity;
using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersClient.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MeatInc.ActionGunnersClient.Player;

namespace MeatInc.ActionGunnersClient.Character.Spawning
{
    public class CharacterSpawner
    {
        public event Action<CharacterFacade> CharacterSpawned;
        public event Action<CharacterFacade> CharacterDespawned;


        private readonly CharacterFacadeFactory _networkFactory;
        private readonly CharacterFacadeFactory _playerFactory;
        private readonly CameraManager _cameraManager;
        private readonly Dictionary<ushort, CharacterFacade> _characters;
        private readonly UnityClient _unityClient;

        public CharacterSpawner(
            [Inject(Id = Identifiers.Network)]
            CharacterFacadeFactory networkFactory,
            [Inject(Id = Identifiers.Player)]
            CharacterFacadeFactory playerFactory,
            CameraManager cameraManager, UnityClient unityClient)
        {
            _networkFactory = networkFactory;
            _playerFactory = playerFactory;
            _cameraManager = cameraManager;
            _unityClient = unityClient;

            _characters = new Dictionary<ushort, CharacterFacade>();
        }

        public void Spawn(CharacterSpawnData spawnParameters)
        {
            ushort playerId = spawnParameters.Id;
            bool isLocal = spawnParameters.IsLocal;

            if (_characters.ContainsKey(playerId) == false)
            {
                CharacterFacade characterFacade = null;
                if (isLocal)
                {
                    characterFacade = _playerFactory.Create(spawnParameters);
                    var targer = characterFacade.GetComponentInChildren<CameraTargetComponent>();
                    _cameraManager.SetFollowToTransform(targer.transform);
                }
                else
                {
                    characterFacade = _networkFactory.Create(spawnParameters);
                }
                Transform transform = characterFacade.transform;
                transform.SetPositionAndRotation(spawnParameters.Position, transform.rotation);
                _characters.Add(playerId, characterFacade);
                CharacterSpawned?.Invoke(characterFacade);
            }
        }

        public void Despawn(ushort clientId)
        {
            if (_characters.ContainsKey(clientId) == true)
            {
                var character = _characters[clientId];
                _characters.Remove(clientId);
                character.Dispose();
                CharacterDespawned?.Invoke(character);
            }
        }
    }
}
