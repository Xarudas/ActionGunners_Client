using MeatInc.ActionGunnersShared.GameLoop;
using MeatInc.ActionGunnersShared.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MeatInc.ActionGunnersClient.Player
{
    public class PlayerInput : UpdatableObject
    {
        private readonly CharacterControlState _controlState;
        private readonly ActionGunnersInput _input;

        public PlayerInput(CharacterControlState controlState, ActionGunnersInput input)
        {
            _controlState = controlState;
            _input = input;
        }

        public override void OnUpdate(float deltaTime)
        {
            var inputsContainer = new InputsContainer();
            inputsContainer.Jump = _input.Jump;
            inputsContainer.PrimaryAction = _input.PrimaryAction;
            var moveAxis = _input.Move;
            var lookAxis = _input.Look;
            var newInputData = new CharacterInputData(inputsContainer, moveAxis, lookAxis, 0);
            _controlState.InputData = newInputData;
        }
    }
}
