using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient.Connection
{
    public class ConnectorViewer : MonoBehaviour
    {
        [SerializeField]
        private Text _connectionStateText;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _disconnectButton;

        private void Start()
        {
            NetworkManager.Instance.Connected += OnConnected;
            NetworkManager.Instance.Disconnected += OnDisconected;
            NetworkManager.Instance.ConnectionFailed += ConnectionFaled;
            _connectButton.onClick.AddListener(Connect);
            _disconnectButton.onClick.AddListener(Disconnect);

            if (NetworkManager.Instance.Client.ConnectionState == DarkRift.ConnectionState.Connected)
            {
                _connectButton.gameObject.SetActive(false);
                _disconnectButton.gameObject.SetActive(true);
                
            }
            _connectButton.gameObject.SetActive(true);
            _disconnectButton.gameObject.SetActive(false);
            _connectionStateText.text = "Не подключен";
        }
        private void OnDisable()
        {
            NetworkManager.Instance.Connected -= OnConnected;
            NetworkManager.Instance.Disconnected -= OnDisconected;
            NetworkManager.Instance.ConnectionFailed -= ConnectionFaled;
        }
        private void ConnectionFaled(Exception obj)
        {
            _connectButton.gameObject.SetActive(true);
            _connectionStateText.text = "Ошибка подключения";
        }
        private void OnConnected()
        {
            _disconnectButton.gameObject.SetActive(true);
            _connectionStateText.text = "Подключен. Твой Id:" + NetworkManager.Instance.Client.ID;
        }

        private void OnDisconected(DarkRift.Client.DisconnectedEventArgs obj)
        {
            _disconnectButton.gameObject.SetActive(false);
            _connectButton.gameObject.SetActive(true);
            _connectionStateText.text = "Отключен";
        }
        

        private void Disconnect()
        {
            NetworkManager.Instance.Disconnect();
            
        }

        private void Connect()
        {
            NetworkManager.Instance.Connect();
            _connectButton.gameObject.SetActive(false);
        }


    }
}
