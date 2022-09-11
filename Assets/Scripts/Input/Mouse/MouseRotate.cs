using System;
using Cinemachine;
using UnityEngine;

namespace Conffi.Input
{
    public sealed class MouseRotate : Singleton<MouseRotate>
    {
        public bool CanRotate { get; set; } = true;
        
        [Header("Rotation properties "), Space]
        [SerializeField] private float rotateSpeed = 100f;
        [SerializeField] private float directionThreshold = .9f;
        [SerializeField] private float maxXAngle = 60f;
        [SerializeField] private float minXAngle = -60f;
        
        [Header("Rotate transform reference"), Space]
        [SerializeField] private Transform itemToRotate;
        [SerializeField] private Transform localTransform;
        [SerializeField] private CinemachineVirtualCamera currentCamera;

        //if using orthoCamera track Camera itself instead transform
        private Transform cameraTransform;

        private Vector3 mousePos;
        private Vector3 mousePrevPos = Vector3.zero;
        private Vector3 mousePosDelta = Vector3.zero;
        
        private UserInput userInput;

        private void Awake()
        {
            userInput = new UserInput();
            cameraTransform = Camera.main.transform;
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
            if(CanRotate == false)
                return;

            MouseRotationNotLimited();
            
            CheckRotationAngle();
        }

        private void MouseRotationNotLimited()
        {
            if (userInput.Mouse.PrimaryButton.IsPressed())
            {
                Vector3 mousePosition = userInput.Mouse.MousePosition.ReadValue<Vector2>();
                
                mousePosDelta = mousePosition - mousePrevPos;
                
                itemToRotate.transform.Rotate(-cameraTransform.right, Vector3.Dot(mousePosDelta, cameraTransform.up) * rotateSpeed * Time.deltaTime, Space.World);
                itemToRotate.transform.Rotate(transform.up, Vector3.Dot(mousePosDelta, cameraTransform.right) * rotateSpeed * Time.deltaTime, Space.World);
                
                //check for usability in further versions
                //GetDirection(mousePosDelta);
            }

            mousePrevPos = userInput.Mouse.MousePosition.ReadValue<Vector2>();
        }
        
        private void CheckRotationAngle()
        {
            Vector3 playerEuelerAngles = localTransform.rotation.eulerAngles;

            //limit x axis
            playerEuelerAngles.x = (playerEuelerAngles.x > 180) ? playerEuelerAngles.x - 360 : playerEuelerAngles.x;
            playerEuelerAngles.x = Mathf.Clamp(playerEuelerAngles.x, minXAngle, maxXAngle);
            
            //limit z axis
            playerEuelerAngles.z = 0;

            //y axis unlimited
            localTransform.rotation = Quaternion.Euler(playerEuelerAngles);
        }

        #region SWIPE_DIRECTION
        
        private void GetDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
            {
                Debug.Log("up");
            }

            if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
            {
                Debug.Log("down");
            }
            
            if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                Debug.Log("right");
            }
            
            if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                Debug.Log("left");
            }
        }

        #endregion
    }
}