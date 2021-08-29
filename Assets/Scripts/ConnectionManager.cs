using DarkRift.Client.Unity;
using DarkRift;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using System;
using MeatInc.ActionGunnersShared;

namespace MeatInc.ActionGunnersClient
{
    [RequireComponent(typeof(UnityClient))]
    public class ConnectionManager : MonoBehaviour
    {
        public delegate void OnConnectedDelegate();
        public static ConnectionManager Instance;
        public event OnConnectedDelegate OnConnected;

        public ushort PlayerId { get; set; }

        public LobbyInfoData LobbyInfoData { get; set; }

        [SerializeField]
        private string _ipAdress;
        [SerializeField]
        private int port;

        public UnityClient Client { get; private set; }

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
        }

        private void Start()
        {
            Client.ConnectInBackground(IPAddress.Parse(_ipAdress), port, true, OnConnect);
        }

        private void OnConnect(Exception exception)
        {
            if (Client.ConnectionState == ConnectionState.Connected)
            {
                OnConnected?.Invoke();
            }
            else
            {
                Debug.LogError("Unable to connect to server.");
            }
        }
    }
}
