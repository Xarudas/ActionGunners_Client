using DarkRift.Client.Unity;
using MeatInc.ActionGunnersClient.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MeatInc.ActionGunnersClient.Menus.ConnectionMenu
{
    public class ConnectionMenuManager : IInitializable, ITickable
    {
        private readonly TMP_Text _connectionText;
        private readonly UnityClient _client;
        private readonly ConnectionParam _connectionParam;
        private uint _retryCount;
        private float _timer;
        private int _count;
        public ConnectionMenuManager(
            [Inject(Id = Identifiers.ConnectionText)]
            TMP_Text connectionText,
            UnityClient client,
            ConnectionParam connectionParam)
        {
            _connectionText = connectionText;
            _client = client;
            _connectionParam = connectionParam;
        }
        public void Initialize()
        {
            _retryCount = 0;
            _timer = 0;
            _client.StartCoroutine(UpdateText());
        }

        public void Tick()
        {
            if (_timer > 5)
            {
                _timer = 0;
            }
            if (_timer == 0)
            {
                _client.ConnectInBackground(_connectionParam.Host, _connectionParam.Port, _connectionParam.NoDelay, OnConnect);
            }
            _timer += Time.deltaTime;
        }

        private void OnConnect(Exception e)
        {
            if (_client.ConnectionState == DarkRift.ConnectionState.Connected)
            {
                _connectionText.text = "Connected";
                LoadMainMenu();
            }
            else
            {
                _retryCount++;
                if (_retryCount > _connectionParam.MaxRetryCount)
                {
                    QuitGame();
                }
            }
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        private IEnumerator UpdateText()
        {
            while (_client.ConnectionState != DarkRift.ConnectionState.Connected)
            {
                if (_count == 5)
                {
                    _connectionText.text = "Connection";
                    _count = 0;
                }
                else
                {
                    _connectionText.text += '.';
                    _count++;
                }
                yield return new WaitForSeconds(2);
            }
        }
    }
}
