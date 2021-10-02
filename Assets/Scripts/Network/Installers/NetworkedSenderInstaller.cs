using MeatInc.ActionGunnersClient.Network.MessageSenders;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Network.Installers
{
    public class NetworkedSenderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputMessageSender>().AsSingle();
        }
    }
}