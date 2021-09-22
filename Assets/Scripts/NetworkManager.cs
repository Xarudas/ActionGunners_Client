using DarkRift.Client;
using DarkRift.Client.Unity;
using System;
using UnityEngine;

namespace MeatInc.ActionGunnersClient
{
    [RequireComponent(typeof(UnityClient))]
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance { get; private set; }
        public event Action Connected;
        public event Action<DisconnectedEventArgs> Disconnected;
        public event Action<Exception> ConnectionFailed;

        public UnityClient Client { get; private set; }
        
        [SerializeField]
        private string _ipAdress;
        [SerializeField]
        private ushort _port;
        [SerializeField]
        private bool _noDelay;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
            Client = GetComponent<UnityClient>();
            Client.Disconnected += OnDisconnect;
        }
        private void OnDestroy()
        {
            if (Client != null)
            {
                Client.Disconnected -= OnDisconnect;
            }
        }
        public void Connect()
        {
            Client.ConnectInBackground(_ipAdress, _port, _noDelay, OnConnect);
        }
        public void Disconnect()
        {
            Client.Disconnect();
        }
        private void OnConnect(Exception e)
        {
            if (Client.ConnectionState == DarkRift.ConnectionState.Connected)
            {
                Connected?.Invoke();
            }
            else
            {
                Debug.LogException(e);
                ConnectionFailed?.Invoke(e);
            }
        }
        private void OnDisconnect(object sender, DisconnectedEventArgs e)
        {
            Disconnected?.Invoke(e);
            if (!e.LocalDisconnect)
            {
                Debug.LogException(e.Exception);
            }
        }
    }

}
