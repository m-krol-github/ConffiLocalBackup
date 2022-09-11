using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

namespace Conffi.Input
{
    public sealed class PinchDetection : MonoBehaviour
    {
        [Header("Zoom properties")]
        [SerializeField] private float minFov;
        [SerializeField] private float maxFov;
        [SerializeField] private float speed = 4f;
        
        [Header("Zoom transform reference"), Space]
        //if using orthoCamera track Camera itself instead transform
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform cameraTransformVirtual;
        
        private UserInput userInput;
        private Coroutine zoomCorutine;
        
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

        private void Start()
        {
            userInput.Touch.SecondaryTouchContact.started += _ => ZoomStart();
            userInput.Touch.SecondaryTouchContact.canceled += _ => ZoomEnd();
        }

        private void ZoomStart()
        {
            MouseRotate.Instance.CanRotate = false;
            zoomCorutine = StartCoroutine(ZoomDetection());
        }

        private void ZoomEnd()
        {
            MouseRotate.Instance.CanRotate = true;
            StopCoroutine(zoomCorutine);
        }

        private IEnumerator ZoomDetection()
        {
            float previosDistance = 0, distance = 0f;

            while (true)
            {
                distance = Vector2.Distance(userInput.Touch.PrimaryTouchPosition.ReadValue<Vector2>(), userInput.Touch.SecondaryTouchPosition.ReadValue<Vector2>());
                
                if (distance > previosDistance)
                {
                    virtualCamera.m_Lens.FieldOfView -= 1;
                    
                    //zoom out currently used as cameraFOV change uncomment these lines to use as camera transform
                    //Vector3 targetPosition = cameraTransform.position;
                    //targetPosition.z -= 1;
                    //cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * speed);

                    virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFov, maxFov);
                }
                else if (distance < previosDistance)
                {
                    virtualCamera.m_Lens.FieldOfView += 1;
                    
                    //zoom in currently used as cameraFOV change uncomment these lines to use as camera transform
                    //Vector3 targetPosition = cameraTransform.position;
                    //targetPosition.z += 1;
                    //cameraTransform.position = Vector3.Slerp(cameraTransform.position, targetPosition, Time.deltaTime * speed);
                    
                    virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFov, maxFov);
                }
                
                //track distance for next loop
                previosDistance = distance;
                yield return null;
            }
        }
    }
}