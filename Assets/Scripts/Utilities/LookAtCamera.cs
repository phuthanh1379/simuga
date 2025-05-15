using System;
using UnityEngine;

namespace Utilities
{
    public class LookAtCamera : MonoBehaviour
    {
        private GameObject _mainCamera;

        private void Awake()
        {
            if (Camera.main != null)
            {
                _mainCamera = Camera.main.gameObject;
            }
        }

        private void LateUpdate()
        {
            transform.LookAt(_mainCamera.transform);
        }
    }
}