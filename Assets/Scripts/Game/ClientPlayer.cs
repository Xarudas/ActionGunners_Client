using MeatInc.ActionGunnersShared.Game;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeatInc.ActionGunnersClient.Game
{
    [RequireComponent(typeof(PlayerInterpolation))]
    public class ClientPlayer : MonoBehaviour
    {
        public event Action PlayerInitialized;
        [SerializeField]
        private PlayerLogic _playerLogic;
        [SerializeField]
        private float _sensivity;

        private ushort _id;
        private bool _isLocal;
        private Vector2 _look;

        private StarterAssetsInputs _input;
        private PlayerInterpolation _playerInterpolation;

        private void Awake()
        {
            _playerInterpolation = GetComponent<PlayerInterpolation>();
            _input = GetComponent<StarterAssetsInputs>();
        }
        public void Initialize(ushort id)
        {
            _id = id;
            if (NetworkManager.Instance.Client.ID == _id)
            {
                _isLocal = true;
                _input = GetComponent<StarterAssetsInputs>();
                _playerInterpolation.CurrentData = new PlayerStateData(_id, 0, Vector3.zero, Quaternion.identity);
            }
            PlayerInitialized?.Invoke();
        }

        private void FixedUpdate()
        {
            InputsContainer inputs = GetInputs();
            _look += _input.look * _sensivity;

            Quaternion rotation = Quaternion.Euler(0, _look.x, 0);

            PlayerInputData inputData = new PlayerInputData(inputs, rotation, 0);

            transform.position = _playerInterpolation.CurrentData.Position;

            PlayerStateData nextStateData = _playerLogic.GetNextFrameData(inputData, _playerInterpolation.CurrentData);
            _playerInterpolation.SetFramePosition(nextStateData);
        }

        private InputsContainer GetInputs()
        {
            InputsContainer inputs = new InputsContainer();

            inputs.W = _input.move.y > 0;
            inputs.S = _input.move.y < 0;
            inputs.D = _input.move.x > 0;
            inputs.A = _input.move.x < 0;

            inputs.Shift = _input.sprint;
            inputs.Space = _input.jump;

            return inputs;
        }
    }
}
