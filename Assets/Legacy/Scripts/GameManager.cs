using DarkRift;
using MeatInc.ActionGunnersSharedLegacy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClientLegacy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private Dictionary<ushort, ClientPlayer> players = new Dictionary<ushort, ClientPlayer>();
        private Buffer<GameUpdateData> _gameUpdateDataBuffer = new Buffer<GameUpdateData>(1, 1);

        [SerializeField]
        private GameObject _playerPrefab;


        public uint ClientTick { get; private set; }
        public uint LastReceivedServerTick { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            ConnectionManager.Instance.Client.MessageReceived += OnMessage;
            using (Message message = Message.CreateEmpty(Tags.Game.GameJoinRequest))
            {
                ConnectionManager.Instance.Client.SendMessage(message, SendMode.Reliable);
            }
        }
        private void FixedUpdate()
        {
            ClientTick++;
            GameUpdateData[] receivedGameUpdateData = _gameUpdateDataBuffer.Get();
            foreach (var data in receivedGameUpdateData)
            {
                UpdateClientGameState(data);
            }
        }

        

        private void OnDestroy()
        {
            Instance = null;
            ConnectionManager.Instance.Client.MessageReceived -= OnMessage;
        }
        private void UpdateClientGameState(GameUpdateData gameUpdateData)
        {
            LastReceivedServerTick = gameUpdateData.Frame;
            foreach (var data in gameUpdateData.SpawnData)
            {
                if (data.Id != ConnectionManager.Instance.PlayerId)
                {
                    SpawnPlayer(data);
                }
            }
            foreach (var data in gameUpdateData.DespawnData)
            {
                if (players.ContainsKey(data.Id))
                {
                    Destroy(players[data.Id].gameObject);
                    players.Remove(data.Id);
                }
            }
            foreach (var data in gameUpdateData.UpdateData)
            {
                ClientPlayer p;
                if (players.TryGetValue(data.Id, out p))
                {
                    p.OnServerDataUpdate(data);
                }
            }
            foreach (var data in gameUpdateData.HealthData)
            {
                ClientPlayer p;
                if (players.TryGetValue(data.PlayerId, out p))
                {
                    p.SetHealth(data.Value);
                }
            }
        }
        private void OnMessage(object sender, DarkRift.Client.MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.Game.GameStartDataResponse:
                        OnGameJoinAccept(message.Deserialize<GameStartData>());
                        break;
                    case Tags.Game.GameUpdate:
                        OnGameUpdate(message.Deserialize<GameUpdateData>());
                        break;
                    default:
                        break;
                }
            }
        }

        

        private void OnGameJoinAccept(GameStartData gameStartData)
        {
            LastReceivedServerTick = gameStartData.OnJoinServerTick;
            ClientTick = gameStartData.OnJoinServerTick;
            foreach (var playerSpawnData in gameStartData.Players)
            {
                SpawnPlayer(playerSpawnData);
            }
        }
        private void OnGameUpdate(GameUpdateData gameUpdateData)
        {
            _gameUpdateDataBuffer.Add(gameUpdateData);
        }
        private void SpawnPlayer(PlayerSpawnData playerSpawnData)
        {
            GameObject go = Instantiate(_playerPrefab);
            ClientPlayer player = go.GetComponent<ClientPlayer>();
            player.Initialize(playerSpawnData.Id, playerSpawnData.Name);
            players.Add(playerSpawnData.Id, player);
        }
    }
}
