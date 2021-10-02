using MeatInc.ActionGunnersShared.Character;
using MeatInc.ActionGunnersShared.GameLoop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Player
{
    public class PlayerCameraRotation : FixedUpdatableObject
    {
        private readonly CharacterControlState _controlState;
        private readonly CameraTargetComponent _cameraTargetComponent;
        private readonly Settings _settings = new Settings();
        private float _vertical;

        public PlayerCameraRotation(CharacterControlState controlState, CameraTargetComponent cameraTargetComponent)
        {
            _controlState = controlState;
            _cameraTargetComponent = cameraTargetComponent;
        }

        public override void OnFixedUpdate(float deltaTime)
        {
            _cameraTargetComponent.transform.rotation = Quaternion.Euler(60, 0, 0);
        }

        public class Settings
        {
            public float MinAxis = 60;
            public float MaxAxis = -50;
        }
    }
}
