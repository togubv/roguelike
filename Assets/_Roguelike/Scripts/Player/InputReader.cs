using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Roguelike
{
    public class InputReader : MonoBehaviour
    {
        public Vector2 axisInput { get; private set; }

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            AddHandlers();
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            RemoveHandlers();
            _playerInput.Disable();
        }

        private void AddHandlers()
        {
            _playerInput.Player.Movement.performed += OnMoveStarted;
            _playerInput.Player.Movement.canceled += OnMoveCanceled;
        }

        private void RemoveHandlers()
        {
            _playerInput.Player.Movement.performed -= OnMoveStarted;
            _playerInput.Player.Movement.canceled -= OnMoveCanceled;
        }

        private void OnMoveStarted(InputAction.CallbackContext ctx)
        {
            axisInput = ctx.ReadValue<Vector2>();
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            axisInput = ctx.ReadValue<Vector2>();
        }
    }
}
