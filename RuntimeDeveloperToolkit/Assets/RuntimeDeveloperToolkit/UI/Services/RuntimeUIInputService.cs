using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace RuntimeDeveloperToolkit.UI.Services
{
    public sealed class RuntimeUIInputService
    {
        private bool _isInitialized;

        public bool IsInitialized => _isInitialized;
        
        public Vector2 PointerPosition { get; private set; }
        public bool IsPointerPressed { get; private set; }
        public bool WasPointerPressedThisFrame { get; private set; }
        public bool WasPointerReleasedThisFrame { get; private set; }
        public bool IsKeyboardAvailable => Keyboard.current != null;
        
        public bool IsKeyboardInputCaptured { get; private set; }

        public bool IsPointerOverToolkit { get; private set; }

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;
        }

        public void Update() 
        {
            if (!_isInitialized)
                return;

            IsPointerOverToolkit =
                EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject();
            
            //PointerPosition = Input.mousePosition;
            
            if (Mouse.current != null)
            {
                PointerPosition = Mouse.current.position.ReadValue();

                IsPointerPressed =
                    Mouse.current.leftButton.isPressed;

                WasPointerPressedThisFrame =
                    Mouse.current.leftButton.wasPressedThisFrame;

                WasPointerReleasedThisFrame =
                    Mouse.current.leftButton.wasReleasedThisFrame;
            }
            else
            {
                IsPointerPressed = false;
                WasPointerPressedThisFrame = false;
                WasPointerReleasedThisFrame = false;
            }
        }

        public void Shutdown()
        {
            if (!_isInitialized)
                return;

            IsPointerOverToolkit = false;
            _isInitialized = false;
            
            IsPointerPressed = false;
            WasPointerPressedThisFrame = false;
            WasPointerReleasedThisFrame = false; 
            IsKeyboardInputCaptured = false;
        }
        
        public bool IsKeyPressed(Key key)
        {
            if (Keyboard.current == null)
                return false;

            return Keyboard.current[key].isPressed;
        }

        public bool WasKeyPressedThisFrame(Key key)
        {
            if (Keyboard.current == null)
                return false;

            return Keyboard.current[key].wasPressedThisFrame;
        }

        public bool WasKeyReleasedThisFrame(Key key)
        {
            if (Keyboard.current == null)
                return false;

            return Keyboard.current[key].wasReleasedThisFrame;
        }
        
        public void SetKeyboardInputCaptured(bool captured)
        {
            IsKeyboardInputCaptured = captured;
        }
    }
}