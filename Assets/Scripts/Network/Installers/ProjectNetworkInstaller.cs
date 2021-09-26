using DarkRift.Client.Unity;
using MeatInc.ActionGunnersClient.Network.Components;
using MeatInc.ActionGunnersClient.Network.Components.ConnectionManagement;
using System;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Network.Installers
{
    public class ProjectNetworkInstaller : MonoInstaller
    {
        [SerializeField]
        private UnityClient _client;
        public override void InstallBindings()
        {
            InstallConnectionManagment();
        }

        private void InstallConnectionManagment()
        {
            Container.Bind<UnityClient>().FromInstance(_client).AsSingle();
            Container.BindInterfacesAndSelfTo<NetworkRelay>().AsSingle();
            Container.BindInterfacesAndSelfTo<DisconnectionManager>().AsSingle();
        }
    }
}