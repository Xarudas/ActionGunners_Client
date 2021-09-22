using MeatInc.ActionGunnersShared.Looby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient.LobbySystem
{
    public class LobbyViewer : MonoBehaviour
    {
        [SerializeField]
        private Lobby _lobby;
        [SerializeField]
        private GameObject _loobyMenu;
        [SerializeField]
        private Text _userName;
        private void Start()
        {
            _lobby.JoinedLobby += OnJoinLooby;
            _lobby.LeavedLobby += OnLeftLooby;
            _loobyMenu.SetActive(false);
        }

        private void OnDestroy()
        {
            _lobby.JoinedLobby -= OnJoinLooby;
            _lobby.LeavedLobby -= OnLeftLooby;
        }

        private void OnJoinLooby(LobbyInfoData data)
        {
            _userName.text = "Èìÿ - " + data.ClientData.Name;
            _loobyMenu.SetActive(true);
        }

        private void OnLeftLooby()
        {
            _loobyMenu.SetActive(false);
        }
    }
}
