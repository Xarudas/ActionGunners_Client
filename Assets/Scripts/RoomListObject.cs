using MeatInc.ActionGunnersShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient
{
    public class RoomListObject : MonoBehaviour
    {
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _slotsText;
        [SerializeField]
        private Button _joinButton;

        public void Set(LobbyManager lobbyManager, RoomData data)
        {
            _nameText.text = data.Name;
            _slotsText.text = data.Slots + "/" + data.MaxSlots;
            _joinButton.onClick.RemoveAllListeners();
            _joinButton.onClick.AddListener(delegate { lobbyManager.SendJoinRoomRequest(data.Name); });
        }
    }
}
