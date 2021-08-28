using DarkRift;
using MeatInc.ActionGunnersShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient
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
            _loginWindow.SetActive(false);
            _submitLoginButton.onClick.AddListener(OnSubmitLogin);
        }
        private void OnDestroy()
        {
            ConnectionManager.Instance.OnConnected -= StartLoginProcess;
        }

        private void StartLoginProcess()
        {
            _loginWindow.SetActive(true);
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

        
    }
}
