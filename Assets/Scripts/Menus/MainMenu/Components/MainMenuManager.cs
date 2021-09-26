using MeatInc.ActionGunnersClient.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace MeatInc.ActionGunnersClient.Menus.MainMenu
{
    public class MainMenuManager : IInitializable, IDisposable
    {
        private readonly Button _playeButton;
        private readonly Button _quitButton;
        public MainMenuManager(
            [Inject(Id = Identifiers.MainMenuPlayButton)]
            Button playButton,
            [Inject(Id = Identifiers.MainMenuQuitButton)]
            Button quitButton)
        {
            _playeButton = playButton;
            _quitButton = quitButton;
        }
        public void Initialize()
        {
            _playeButton.onClick.AddListener(Play);
            _quitButton.onClick.AddListener(QuitGame);
        }
        public void Dispose()
        {
            _playeButton.onClick.RemoveListener(Play);
            _quitButton.onClick.RemoveListener(QuitGame);
        }

        private void Play()
        {
            SceneManager.LoadScene(2);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


    }
}
