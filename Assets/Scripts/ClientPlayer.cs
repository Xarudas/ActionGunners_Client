using DarkRift;
using MeatInc.ActionGunnersShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient
{
    [RequireComponent(typeof(PlayerLogic))]
    [RequireComponent(typeof(PlayerInterpolation))]
    public class ClientPlayer : MonoBehaviour
    {
        private PlayerLogic _playerLogic;
        private PlayerInterpolation _interpolation;

        private float _yaw;
        private float _pitch;

        private ushort _id;
        private string _playerName;
        private bool _isOwn;

        private int _health;


        [SerializeField]
        private float _sensivityX;
        [SerializeField]
        private float _sensivityY;

        private void Awake()
        {
            _playerLogic = GetComponent<PlayerLogic>();
            _interpolation = GetComponent<PlayerInterpolation>();
        }

        

        private void FixedUpdate()
        {
            if (_isOwn)
            {
                bool[] inputs = new bool[6];
                inputs[0] = Input.GetKey(KeyCode.W);
                inputs[1] = Input.GetKey(KeyCode.A);
                inputs[2] = Input.GetKey(KeyCode.S);
                inputs[3] = Input.GetKey(KeyCode.D);
                inputs[4] = Input.GetKey(KeyCode.Space);
                inputs[5] = Input.GetMouseButtonDown(0);

                _yaw += Input.GetAxis("Mouse X") * _sensivityX;
                _pitch += Input.GetAxis("Mouse Y") * _sensivityY;

                Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);

                PlayerInputData inputData = new PlayerInputData(inputs, rotation, 0);

                transform.position = _interpolation.CurrentData.Position;
                PlayerStateData nextStateData = _playerLogic.GetNextFrameData(inputData, _interpolation.CurrentData);
                _interpolation.SetFramePosition(nextStateData);

                using (Message message = Message.Create(Tags.Game.GamePlayerInput, inputData))
                {
                    ConnectionManager.Instance.Client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        public void Initialize(ushort id, string playerName)
        {
            _id = id;
            _playerName = playerName;
            if (ConnectionManager.Instance.PlayerId == _id)
            {
                _isOwn = true;
                Camera.main.transform.SetParent(transform);
                Camera.main.transform.localPosition = new Vector3(0, 0, 0);
                Camera.main.transform.localRotation = Quaternion.identity;
                _interpolation.CurrentData = new PlayerStateData(_id, 0, Vector3.zero, Quaternion.identity);
            }
        }

        public void OnServerDataUpdate(PlayerStateData playerStateData)
        {
            if (_isOwn)
            {

            }
            else
            {
                _interpolation.SetFramePosition(playerStateData);
            }
        }
    }
}
