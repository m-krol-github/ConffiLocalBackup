using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Conffi.Input
{

    public sealed class ScrollZoom : MonoBehaviour
    {
        [Header("Zoom properities"), Space]
        [SerializeField] private float minFov = 1f;
        [SerializeField] private float maxFov = 150f;
        
        [SerializeField] private float zoomSpeed = 15f;
        
        [Header("Zoom transform reference")]
        [SerializeField] private CinemachineVirtualCamera cameraTransform;

        [SerializeField] private CinemachineFreeLook freeLookCamera;

        private UserInput userInput;

        private void Awake()
        {
            userInput = new UserInput();
        }

        private void OnEnable()
        {
            userInput.Enable();
        }

        private void OnDisable()
        {
            userInput.Disable();
        }

        private void Update()
        {
            //float minMax = Mathf.Clamp(cameraTransform.m_Lens.FieldOfView, minZoom, maxZoom); //currently NA
            
            ScrollZoomLoop();
        }

        private void ScrollZoomLoop()
        {
            float scrollInput = userInput.Mouse.Scroll.ReadValue<float>();
            
            float currentFov = cameraTransform.m_Lens.FieldOfView * zoomSpeed * scrollInput * Time.deltaTime;
            float fov = currentFov / 100;

            if (scrollInput > 0)
            {
                cameraTransform.m_Lens.FieldOfView += -fov;
                
                cameraTransform.m_Lens.FieldOfView = Mathf.Clamp(cameraTransform.m_Lens.FieldOfView, minFov, maxFov);
            }
            else if (scrollInput < 0)
            {
                cameraTransform.m_Lens.FieldOfView -= fov;
                
                cameraTransform.m_Lens.FieldOfView = Mathf.Clamp(cameraTransform.m_Lens.FieldOfView, minFov, maxFov);
            }
        }
    }
}