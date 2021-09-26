using MeatInc.ActionGunnersClient.Utility;
using TMPro;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Menus.ConnectionMenu
{
    public class ConnectionMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private TMP_Text _connectionText;
        public override void InstallBindings()
        {
            Container.BindInstance(_connectionText).WithId(Identifiers.ConnectionText);
            Container.BindInterfacesAndSelfTo<ConnectionMenuManager>().AsSingle();
        }
    }
}