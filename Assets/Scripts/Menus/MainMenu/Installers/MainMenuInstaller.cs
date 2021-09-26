using MeatInc.ActionGunnersClient.Utility;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MeatInc.ActionGunnersClient.Menus.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _quitButton;
        public override void InstallBindings()
        {
            Container.BindInstance(_playButton).WithId(Identifiers.MainMenuPlayButton);
            Container.BindInstance(_quitButton).WithId(Identifiers.MainMenuQuitButton);

            Container.BindInterfacesAndSelfTo<MainMenuManager>().AsSingle();
        }
    }
}
