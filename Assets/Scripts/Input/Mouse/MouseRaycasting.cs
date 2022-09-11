using System;
using System.Collections;
using System.Collections.Generic;
using Conffi.Item;
using UnityEngine;

namespace Conffi.Input
{
    public class MouseRaycasting : MonoBehaviour
    {
        [SerializeField] private Camera currentCamera;
        
        private UserInput _userInput;
        
        private void OnEnable()
        {
            _userInput.Enable();    
        }

        private void OnDisable()
        {
            _userInput.Disable();
        }

        private void Awake()
        {
            _userInput = new UserInput();
        }

        private void Update()
        {
            if (_userInput.Mouse.PrimaryButton.IsPressed())
            {
                Vector2 mousePos = _userInput.Mouse.MousePosition.ReadValue<Vector2>();
                Ray ray = currentCamera.ScreenPointToRay(mousePos);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if(hitInfo.collider.gameObject.GetComponent<BaseItem>())
                    {
                        Debug.Log("BaseItem");
                    }
                }
            }
        }
    }
}