
using DarkRift;
using DarkRift.Client;
using MeatInc.ActionGunnersSharedLegacy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClientLegacy
{
    public class LoginManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _loginWindow;
        [SerializeField]
        private InputField _loginInput;
        [SerializeField]
        private Button _submitLoginButton;




        private void Start()
        {
            ConnectionManager.Instance.OnConnected += StartLoginProcess;
            ConnectionManager.Instance.Client.MessageReceived += OnMessage;
            _loginWindow.SetActive(false);
            _submitLoginButton.onClick.AddListener(OnSubmitLogin);
        }

        

        private void OnDestroy()
        {
            ConnectionManager.Instance.OnConnected -= StartLoginProcess;
            ConnectionManager.Instance.Client.MessageReceived -= OnMessage;
        }

        

        public void OnSubmitLogin()
        {
            if (!string.IsNullOrEmpty(_loginInput.text))
            {
                _loginWindow.SetActive(false);

                using (Message message = Message.Create(Tags.Login.LoginRequest, new LoginRequestData(_loginInput.text)))
                {
                    ConnectionManager.Instance.Client.SendMessage(message, SendMode.Reliable);
                }
            }
        }
        private void StartLoginProcess()
        {
            _loginWindow.SetActive(true);
        }
        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Login.LoginRequestDenied:
                        OnLoginDecline();
                        break;
                    case Tags.Login.LoginRequestAccepted:
                        OnLoginAccept(message.Deserialize<LoginInfoData>());
                        break;
                    default:
                        break;
                }
            }
        }

       

        private void OnLoginDecline()
        {
            _loginWindow.SetActive(true);
        }

        private void OnLoginAccept(LoginInfoData loginInfoData)
        {
            ConnectionManager.Instance.PlayerId = loginInfoData.Id;
            ConnectionManager.Instance.LobbyInfoData = loginInfoData.Data;
            SceneManager.LoadScene("Lobby");
        }
    }
}
