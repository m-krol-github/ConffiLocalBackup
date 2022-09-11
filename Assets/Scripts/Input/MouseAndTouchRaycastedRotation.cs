using UnityEngine;

namespace Conffi.Input
{
    public sealed class MouseAndTouchRaycastedRotation : MonoBehaviour
    {
        [SerializeField] private  float smoothTurnSpeed = 5f;
        [SerializeField] private  float threshold = .9f;
        [SerializeField] private  Quaternion startRot;
        [SerializeField] private  Quaternion endRot;
        
        [SerializeField] private float rotateSpeed;

        private UserInput userInput;
        private Camera transformCamera;
        
        private void OnEnable()
        {
            userInput.Enable();     
        }

        private void OnDisable()
        {
            userInput.Disable();
        }

        private void Awake()
        {
            userInput = new UserInput();

            transformCamera = Camera. main;
        }

        public void UpdateRaycastedRotation()
        {
            MouseRotateRaycasted();
            
            TouchRotateRaycasted();
        }

        private void TouchRotateRaycasted()
        {
            Vector3 touchPos = userInput.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
            
            if (userInput.Touch.PrimaryContact.IsPressed())
            {
                Ray ray = transformCamera.ScreenPointToRay(touchPos);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "CollisionCube")
                    {
                        Vector2 delta = userInput.Touch.PrimaryTouchDelta.ReadValue<Vector2>();
                        transform.Rotate(0, -delta.x * rotateSpeed * Time.deltaTime, 0) ;
                    }
                }
            }
        }
        
        //todo: check can you make the joint input with input actions, DRY
        private void MouseRotateRaycasted()
        {
            Vector3 mousePos = userInput.Mouse.MousePosition.ReadValue<Vector2>();
            
            Ray ray = transformCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (userInput.Mouse.PrimaryButton.IsPressed())
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.name == "CollisionCube")
                    {
                        Vector2 delta = userInput.Mouse.MousePositionDelta.ReadValue<Vector2>();
                        transform.Rotate(0, -delta.x * rotateSpeed * Time.deltaTime, 0) ;
                    }
                }
            }
        }
    }
}