using DarkRift.Client.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MeatInc.ActionGunnersClient.Network.Components.ConnectionManagement
{
    public class DisconnectionManager : IInitializable, IDisposable
    {
        private readonly UnityClient _client;
        public DisconnectionManager(UnityClient client)
        {
            _client = client;
        }
        public void Initialize()
        {
            _client.Disconnected += OnDiconnect;
        }
        public void Dispose()
        {
            _client.Disconnected -= OnDiconnect;
        }
        private void OnDiconnect(object sender, DarkRift.Client.DisconnectedEventArgs e)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        
    }
}