using MeatInc.ActionGunnersShared.GameLoop;
using MeatInc.ActionGunnersShared.GameLoop.Internal;
using MeatInc.ActionGunnersShared.GameLoop.Internal.Coroutines;
using MeatInc.ActionGunnersShared.Relays;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private MonoGameLoop _gameLoop;
        [SerializeField]
        private MonoRelay _backupRelay;
        public override void InstallBindings()
        {
            Container.Bind<IGameLoop>().FromInstance(_gameLoop).AsSingle();
            Container.Bind<IRelay>().FromInstance(_backupRelay).AsSingle();

            Container.BindInterfacesAndSelfTo<CoroutinePool>().AsSingle();
        }
    }
}