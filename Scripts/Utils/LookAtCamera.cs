using System;
using UnityEngine;

namespace Utils
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private Mode mode = Mode.CameraForward;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    transform.LookAt(_camera.transform);
                    break;

                case Mode.LookAtInverted:
                    var position = transform.position;
                    var directionFromCamera = _camera.transform.position - position;
                    transform.LookAt(position - directionFromCamera);
                    break;

                case Mode.CameraForward:
                    transform.forward = _camera.transform.forward;
                    break;

                case Mode.CameraForwardInverted:
                    transform.forward = -_camera.transform.forward;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }
    }
}