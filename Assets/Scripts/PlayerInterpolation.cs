using MeatInc.ActionGunnersShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient
{
    public class PlayerInterpolation : MonoBehaviour
    {
        private float _lastInputTime;

        public PlayerStateData CurrentData { get; set; }
        public PlayerStateData PreviousData { get; private set; }

        private void Update()
        {
            float timeSinceLastInput = Time.time - _lastInputTime;
            float t = timeSinceLastInput / Time.fixedDeltaTime;
            transform.position = Vector3.LerpUnclamped(PreviousData.Position, CurrentData.Position, t);
            transform.rotation = Quaternion.SlerpUnclamped(PreviousData.LookDirection, CurrentData.LookDirection, t);
        }

        public void SetFramePosition(PlayerStateData data)
        {
            RefreshToPosition(data, CurrentData);
        }

        public void RefreshToPosition(PlayerStateData data, PlayerStateData prevData)
        {
            PreviousData = prevData;
            CurrentData = data;
            _lastInputTime = Time.fixedTime;
        }
    }
}
