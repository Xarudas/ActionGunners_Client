using MeatInc.ActionGunnersClient.Utility;
using System;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private ActionGunnersInput _input;
        [SerializeField]
        private CameraTargetComponent _cameraTargetComponent;
        public override void InstallBindings()
        {
            InstallPlayer();
        }
        private void InstallPlayer()
        {
            Container.Bind<ActionGunnersInput>().FromInstance(_input).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle();
            Container.Bind<CameraTargetComponent>().FromInstance(_cameraTargetComponent).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerCameraRotation>().AsSingle();
        }
    }
}