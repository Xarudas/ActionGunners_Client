using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersShared.GameLoop;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Character
{
    public class CharacterMovement : FixedUpdatableObject
    {
        private readonly CharacterController _characterControler;
        private readonly Transform _transform;
        private readonly CharacterControlState _controlState;
        private float _speed = 3f;
        private float _verticalVelocity;

        public CharacterMovement(CharacterController characterControler, Transform transform, CharacterControlState controlState)
        {
            _characterControler = characterControler;
            _transform = transform;
            _controlState = controlState;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            Gravity(deltaTime);
            Move(deltaTime);
        }

        

        private void Move(float deltaTime)
        {
            Vector3 move = new Vector3(_controlState.InputData.MoveAxis.x, _verticalVelocity, _controlState.InputData.MoveAxis.y) * (_speed * deltaTime);

            _characterControler.Move(move);
        }
        private void Gravity(float deltaTime)
        {

            _verticalVelocity -= 2 * deltaTime;

        }
    }
}
