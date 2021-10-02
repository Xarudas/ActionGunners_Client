using Cinemachine;
using MeatInc.ActionGunnersClient.Character;
using MeatInc.ActionGunnersClient.Character.Spawning;
using MeatInc.ActionGunnersClient.Character.Spawning.Network;
using MeatInc.ActionGunnersClient.GameCycle;
using MeatInc.ActionGunnersClient.GameCycle.GameComponentsContainers;
using MeatInc.ActionGunnersClient.GameCycle.GameSystems;
using MeatInc.ActionGunnersClient.Interfaces.GameCycle;
using MeatInc.ActionGunnersShared.GameCycle;
using System;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Installers {
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;
        public override void InstallBindings()
        {
            InstallCamras();
            InstallSpawning();
            InstallGameCycle();
        }

        

        private void InstallSpawning()
        {
            Container.BindInterfacesAndSelfTo<CharacterSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkCharacterSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<TestSpawn>().AsSingle().NonLazy();
        }

        private void InstallCamras()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<CinemachineVirtualCamera>().FromInstance(_virtualCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle();
        }

        private void InstallGameCycle()
        {
            Container.Bind(typeof(IGameComponentContainer<>)).To(typeof(GameComponentContainer<>)).AsSingle();
            Container.Bind(typeof(IEntityComponentContainer<>)).To(typeof(EntityComponentContainer<>)).AsSingle();
            InstallSystems();
            Container.Bind<NetworkGameStateBuffer.Settings>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkGameStateBuffer>().AsSingle();
            Container.Bind<NetworkGameStateDistributor>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkGameStateUpdater>().AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkGameStateReceiver>().AsSingle();
            
        }

        private void InstallSystems()
        {
            Container.Bind<NetworkedCharacterStatesUpdater>().AsSingle();
        }
    }
}