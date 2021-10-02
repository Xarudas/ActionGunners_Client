using Cinemachine;
using MeatInc.ActionGunnersClient.Character;
using MeatInc.ActionGunnersClient.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeatInc.ActionGunnersClient.Character
{
    public class CameraManager 
    {
        private readonly CinemachineVirtualCamera _cinemachineCamera;
        public CameraManager(CinemachineVirtualCamera cinemachineCamera)
        {
            _cinemachineCamera = cinemachineCamera;
        }

        public void SetFollowToTransform(Transform transform)
        {
            _cinemachineCamera.Follow = transform;
        }
        public void SetFollowToCameraTarget(CameraTargetComponent cameraTarget)
        {
            _cinemachineCamera.Follow = cameraTarget.transform;
        }
    }
}
