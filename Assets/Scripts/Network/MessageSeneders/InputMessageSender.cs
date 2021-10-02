using DarkRift;
using DarkRift.Client.Unity;
using MeatInc.ActionGunnersShared;
using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersShared.GameLoop;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Network.MessageSenders
{
    public class InputMessageSender : UpdatableObject
    {
        private readonly UnityClient _client;
        private readonly CharacterControlState _controlState;
        public InputMessageSender(
            UnityClient client,
            CharacterControlState controlState)
        {
            _client = client;
            _controlState = controlState;
        }
        public override void OnUpdate(float deltaTime)
        {
            using (Message message = Message.Create(Tags.PlayerInput, _controlState.InputData))
            {
                _client.SendMessage(message, SendMode.Reliable);
            }
        }
    }
}
