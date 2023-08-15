using TD.Singleton;
using TD.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TD.Managers
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        private PlayerInput _playerInput;

        private InputAction touchPositionAction;
        private InputAction touchPressAction;

        private Camera _camera;

        protected override void Awake()
        {
            _camera = Camera.main;

            _playerInput = GetComponent<PlayerInput>();
            touchPositionAction = _playerInput.actions["InteractPosition"];
            touchPressAction = _playerInput.actions["InteractPress"];
        }

        private void ReadInteraction(InputAction.CallbackContext context)
        {
            Vector3 touchPosition = _camera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
            Vector3 viewDirection = _camera.transform.forward;

            Ray ray = new Ray(touchPosition, viewDirection);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 500))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact();
            }
        }

        private void OnEnable()
        {
            touchPressAction.performed += ReadInteraction;
        }

        private void OnDestroy()
        {
            touchPressAction.performed -= ReadInteraction;
        }
    }
}
