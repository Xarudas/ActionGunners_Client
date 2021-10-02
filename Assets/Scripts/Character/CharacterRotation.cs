using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersShared.GameLoop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Character
{
    public class CharacterRotation : FixedUpdatableObject
    {
        private readonly CharacterControlState _controlState;
        private readonly Transform _transform;
        private float _targetRotation;
        private float _rotationSmoothTime = 0.12f;
        private float _rotationVelocity;

        public CharacterRotation(CharacterControlState controlState, Transform transform)
        {
            _controlState = controlState;
            _transform = transform;
        }
        public override void OnFixedUpdate(float deltaTime)
        {
            if (_controlState.InputData.MoveAxis != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(_controlState.InputData.MoveAxis.x, 0, _controlState.InputData.MoveAxis.y).normalized;
                _targetRotation = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float rotation = Mathf.SmoothDampAngle(_transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, _rotationSmoothTime);

                _transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }
    }
}
