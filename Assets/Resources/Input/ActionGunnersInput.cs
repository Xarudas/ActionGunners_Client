using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MeatInc
{
    public class ActionGunnersInput : MonoBehaviour
    {

        public Vector2 Move { get => _move; }
        public Vector2 Look { get => _look; }
        public bool PrimaryAction { get => _primaryAction; }
        public bool Jump { get => jump; }

        [Header("Character Input Values")]
        [SerializeField]
        private Vector2 _move;
        [SerializeField]
        private Vector2 _look;
        [SerializeField]
        private bool _primaryAction;
        [SerializeField]
        private bool jump;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }
        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }
        public void OnPrimaryAction(InputValue value)
        {
            PrimaryActionInput(value.isPressed);
        }
        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }


        public void MoveInput(Vector2 newMoveDirection)
        {
            _move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            _look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }
        public void PrimaryActionInput(bool newPrimaryActionState)
        {
            _primaryAction = newPrimaryActionState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif
    }

}
