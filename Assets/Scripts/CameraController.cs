using UnityEngine;
using UnityEngine.InputSystem;

namespace Ziggurat
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Range(0.1f,100f)]
        private float _moveSpeed = 10f;
        [SerializeField, Range(0.1f, 100f)]
        private float _rotateSpeed = 10f;
        [SerializeField, Range(0.1f, 100f)]
        private float _upDownSpeed = 5f;

        private Ziggurat.Controls _controls;
        private Camera _camera;
        private LayerMask _gatesMask;
        private bool _activeRotate;

        private void Awake()
        {
            _controls = new Ziggurat.Controls();
            //_controls.Camera.Select.performed += OnLeftClic;
            _controls.Camera.ActivateRotation.performed += OnActivateRotation;
            _controls.Camera.ActivateRotation.canceled += OnActivateRotation;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _gatesMask = LayerMask.GetMask("GatesMask");
        }
        private void Update()
        {
            OnMoveAndRotate();
            OnWASD();
        }

        public void OnWASD()
        {
            var value = _controls.Camera.WASD.ReadValue<Vector2>();
            transform.Translate(new Vector3(value.x, 0, value.y) * 3 * Time.deltaTime, Space.Self);
        }

        private void OnMoveAndRotate()
        {
           // Move
            var direction = _controls.Camera.Scale.ReadValue<Vector2>();
            transform.position += _moveSpeed * Time.deltaTime * (transform.forward * direction.y + transform.right * direction.x);

            //Rotate
            if (!_activeRotate) return;

            direction = _controls.Camera.Rotate.ReadValue<Vector2>();
            var angle = transform.eulerAngles;
            angle.x -= direction.y * _rotateSpeed * Time.deltaTime;
            angle.y += direction.x * _rotateSpeed * Time.deltaTime;
            angle.z = 0f;

            transform.eulerAngles = angle;
        }

        //private void OnLeftClic(InputAction.CallbackContext context)
        //{
        //    Debug.Log("OnLeftClic");
        //    GetRaycastPoint();
        //}

        private void OnActivateRotation(InputAction.CallbackContext context)
        {
            _activeRotate = context.performed;

            if (_activeRotate) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }
       
        private Vector3? GetRaycastPoint()
        {
            Debug.Log("GetRaycastPoint");

            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, _gatesMask))
            {
                Debug.Log(hit.point);
                return hit.point;
            }
            return null;
        }

        private void OnEnable() => _controls.Camera.Enable();

        private void OnDisable() => _controls.Camera.Disable();

        private void OnDestroy() => _controls.Dispose();
    }
}
