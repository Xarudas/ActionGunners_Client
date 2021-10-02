using MeatInc.ActionGunnersShared.Character;
using System;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Character
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField]
        private CharacterFacade _characterFacade;
        [SerializeField]
        private CharacterController _characterControler;
        public override void InstallBindings()
        {
            InstallCharacter();
            InstalComponents();
        }

        private void InstallCharacter()
        {
            Container.BindInterfacesAndSelfTo<CharacterMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterRotation>().AsSingle();
            Container.Bind<CharacterControlState>().AsSingle();
        }

        private void InstalComponents()
        {
            Container.Bind<CharacterController>().FromInstance(_characterControler).AsSingle();
            Container.Bind<Transform>().FromInstance(transform).AsSingle();
            Container.Bind(typeof(CharacterFacade), typeof(IDisposable)).FromInstance(_characterFacade).AsSingle();
        }
    }
}