using MeatInc.ActionGunnersClient.Network.Components.Character;
using UnityEngine;
using Zenject;

public class NetworkedCharacterInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<NetworkedCharacterStateUpdater>().AsSingle();
    }
}