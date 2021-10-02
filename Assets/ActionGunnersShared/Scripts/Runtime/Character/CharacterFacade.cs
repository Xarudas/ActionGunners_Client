using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersShared.Character
{

    public class CharacterFacade : MonoBehaviour, IPoolable<CharacterSpawnData, IMemoryPool>, IDisposable
    {
        public ushort Id { get => _id; }
        public Vector3 Position => _transform.position;
        public Quaternion Rotation => _transform.rotation;

        [SerializeField]
        private ushort _id;

        private IMemoryPool _pool;
        private Transform _transform;

        [Inject]
        public void Construct(
            Transform transform)
        {
            _transform = transform;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(CharacterSpawnData parameters, IMemoryPool pool)
        {
            _id = parameters.Id;
            _pool = pool;
        }

        public void Dispose()
        {
            if (_pool != null)
            {
                _pool.Despawn(this);
            }
        }
    }
}
