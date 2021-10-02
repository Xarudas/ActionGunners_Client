using MeatInc.ActionGunnersClient.Utility;
using MeatInc.ActionGunnersShared.Character;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Installers {
    [CreateAssetMenu(fileName = "PoolableInstaller", menuName = "Installers/PoolableInstaller")]
    public class PoolableInstaller : ScriptableObjectInstaller<PoolableInstaller>
    {
        [SerializeField]
        private CharacterFacade _playerPrefab;
        [SerializeField]
        private CharacterFacade _networkedCharacterPrefab;
        public override void InstallBindings()
        {
            Container.BindMonoPrefabPool<CharacterFacade, CharacterSpawnData, CharacterFacadeFactory, CharacterPool>
                (Identifiers.Player, 1, _playerPrefab, "Players");
            Container.BindMonoPrefabPool<CharacterFacade, CharacterSpawnData, CharacterFacadeFactory, CharacterPool>
                (Identifiers.Network, 4, _networkedCharacterPrefab, "Characters");
        }

        public class CharacterPool : MonoPoolableMemoryPool<CharacterSpawnData, IMemoryPool, CharacterFacade>
        {
        }
    }
}