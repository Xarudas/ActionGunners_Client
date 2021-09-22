using DarkRift;
using MeatInc.ActionGunnersSharedLegacy;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MeatInc.ActionGunnersClientLegacy
{
    struct ReconciliationInfo
    {
        public uint Frame;
        public PlayerStateData Data;
        public PlayerInputData Input;

        public ReconciliationInfo(uint frame, PlayerStateData data, PlayerInputData input)
        {
            Frame = frame;
            Data = data;
            Input = input;
        }
    }

    [RequireComponent(typeof(PlayerLogic))]
    [RequireComponent(typeof(PlayerInterpolation))]
    public class ClientPlayer : MonoBehaviour
    {
        private PlayerLogic _playerLogic;
        private PlayerInterpolation _interpolation;

        private float _yaw;
        private float _pitch;

        private ushort _id;
        private string _playerLogin;
        private bool _isOwn;

        private int _health;

        private Queue<ReconciliationInfo> _reconciliationHistory = new Queue<ReconciliationInfo>();


        [SerializeField]
        private float _sensivityX;
        [SerializeField]
        private float _sensivityY;
        [SerializeField]
        private Text _loginText;
        [SerializeField]
        private Image _healthBarFill;
        [SerializeField]
        private GameObject _healthBarObject;
        [SerializeField]
        private GameObject _shotPrefab;

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

                if (inputs[5])
                {
                    GameObject go = Instantiate(_shotPrefab);
                    go.transform.position = _interpolation.CurrentData.Position;
                    go.transform.rotation = transform.rotation;
                    Destroy(go, 1f);
                }

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

                _reconciliationHistory.Enqueue(new ReconciliationInfo(GameManager.Instance.ClientTick, nextStateData, inputData));
            }
        }
        private void LateUpdate()
        {
            Vector3 point = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));
            if (point.z > 2)
            {
                _healthBarObject.transform.position = point;
            }
            else
            {
                _healthBarObject.transform.position = new Vector3(10000, 0, 0);
            }
        }
        public void Initialize(ushort id, string playerLogin)
        {
            _id = id;
            _playerLogin = playerLogin;
            _loginText.text = playerLogin;
            SetHealth(100);
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
                while (_reconciliationHistory.Any() && _reconciliationHistory.Peek().Frame < GameManager.Instance.LastReceivedServerTick)
                {
                    _reconciliationHistory.Dequeue();
                }
                if (_reconciliationHistory.Any() && _reconciliationHistory.Peek().Frame == GameManager.Instance.LastReceivedServerTick)
                {
                    ReconciliationInfo info = _reconciliationHistory.Dequeue();
                    if (Vector3.Distance(info.Data.Position, playerStateData.Position) > 0.05f)
                    {
                        List<ReconciliationInfo> infos = _reconciliationHistory.ToList();
                        _interpolation.CurrentData = playerStateData;
                        transform.position = playerStateData.Position;
                        transform.rotation = playerStateData.LookDirection;
                        for (int i = 0; i < infos.Count; i++)
                        {
                            PlayerStateData u = _playerLogic.GetNextFrameData(infos[i].Input, _interpolation.CurrentData);
                            _interpolation.SetFramePosition(u);
                        }
                    }
                }

            }
            else
            {
                _interpolation.SetFramePosition(playerStateData);
            }
        }
        public void SetHealth(int value)
        {
            _health = value;
            _healthBarFill.fillAmount = value / 100f;
        }


    }
}
