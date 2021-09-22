using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersSharedLegacy
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerLogic : MonoBehaviour
    {
        private Vector3 gravity;

        [SerializeField]
        private float _walkSpeed;
        [SerializeField]
        private float _gravityConstant;
        [SerializeField]
        private float _jumpStrength;

        public CharacterController CharacterController { get; private set; }

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
        }

        public PlayerStateData GetNextFrameData(PlayerInputData input, PlayerStateData currentStateData)
        {
            bool w = input.KeyInputs[0];
            bool a = input.KeyInputs[1];
            bool s = input.KeyInputs[2];
            bool d = input.KeyInputs[3];
            bool space = input.KeyInputs[4];

            Vector3 rotation = input.LookDirection.eulerAngles;

            gravity = new Vector3(0, currentStateData.Gravity, 0);

            Vector3 movement = Vector3.zero;

            if (w)
            {
                movement += Vector3.forward;
            }
            if (a)
            {
                movement += Vector3.left;
            }
            if (s)
            {
                movement += Vector3.back;
            }
            if (d)
            {
                movement += Vector3.right;
            }

            movement = Quaternion.Euler(0, rotation.y, 0) * movement;

            movement.Normalize();
            movement = movement * _walkSpeed;

            movement = movement * Time.fixedDeltaTime;
            movement = movement + gravity * Time.fixedDeltaTime;

            CharacterController.Move(new Vector3(0, -0.001f, 0));

            if (CharacterController.isGrounded)
            {
                if (space)
                {
                    gravity = new Vector3(0, _jumpStrength, 0);
                }
            }
            else
            {
                gravity -= new Vector3(0, _gravityConstant, 0);
            }

            CharacterController.Move(movement);

            return new PlayerStateData(currentStateData.Id, gravity.y, transform.localPosition, input.LookDirection);
        }
    }
}
