using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient
{
    [CreateAssetMenu(fileName = "ConnectionSettingsInstaller", menuName = "Installers/ConnectionSettingsInstaller")]
    public class ConnectionSettingsInstaller : ScriptableObjectInstaller<ConnectionSettingsInstaller>
    {
        [SerializeField]
        private ConnectionParam _connectionParam;
        public override void InstallBindings()
        {
            Container.BindInstance(_connectionParam).AsSingle();
        }
    }

    [System.Serializable]
    public class ConnectionParam
    {
        public string Host { get => _host; }
        public ushort Port { get => _port; }
        public bool NoDelay { get => _noDelay; }
        public byte MaxRetryCount { get => _maxRetryCount; }

        [SerializeField]
        private string _host;
        [SerializeField]
        private ushort _port;
        [SerializeField]
        private bool _noDelay;
        [SerializeField]
        private byte _maxRetryCount;
    }
}