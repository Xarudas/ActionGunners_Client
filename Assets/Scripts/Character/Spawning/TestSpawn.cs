using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MeatInc.ActionGunnersShared.Character;

namespace MeatInc.ActionGunnersClient.Character.Spawning
{
    public class TestSpawn : IInitializable
    {
        private CharacterSpawner _spawner;
        public TestSpawn(CharacterSpawner spawner)
        {
            _spawner = spawner;
        }
        public void Initialize()
        {
            _spawner.Spawn(new CharacterSpawnData(0, Vector3.zero, true));
        }
    }
}
