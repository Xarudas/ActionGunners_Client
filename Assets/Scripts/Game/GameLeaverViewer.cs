using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClient.Game
{
    public class GameLeaverViewer : MonoBehaviour
    {
        [SerializeField]
        private Button _buttonLeave;
        [SerializeField]
        private GameLeaver _gameLeaver;

        private void Awake()
        {

            _buttonLeave.onClick.AddListener(Leave);
        }
        private void OnDestroy()
        {
            _buttonLeave.onClick.RemoveListener(Leave);
        }
        private void Leave()
        {
            _gameLeaver.Leave();
        }
    }
}
