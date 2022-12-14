using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Ziggurat
{
    public class PanelManager : BasePanelManager
    {
        [Header("Gates"), SerializeField, Tooltip("GreenGates")]
        private GameObject _greenGates;
        [SerializeField, Tooltip("RedGates")]
        private GameObject _redGates;
        [SerializeField, Tooltip("BlueGates")]
        private GameObject _blueGates;

        [Header("Panels"), SerializeField, Tooltip("GreenPanel")]
        protected GameObject _greenPanel;
        [SerializeField, Tooltip("RedPanel")]
        protected GameObject _redPanel;
        [SerializeField, Tooltip("BluePanel")]
        protected GameObject _bluePanel;

        [Header("Panel close buttons"), SerializeField, Tooltip("GreenPanelCloseButton")]
        private Button _greenPanelCloseButton;
        [SerializeField, Tooltip("RedPanelCloseButton")]
        private Button _redPanelCloseButton;
        [SerializeField, Tooltip("BluePanelCloseButton")]
        private Button _bluePanelCloseButton;

        protected GameObject _activePanel;
        private Transform _activeGate;
        //private Transform _activeSolder;
        private Vector2 _zeroPosition;
        private Vector2 _finitePosition;
        private LayerMask _gatesMask;
        private LayerMask _solderMask;
        private string _gateName;
        
        private void Start()
        {
            _controls = new Controls();
            _controls.Camera.Enable();
            _zeroPosition = _greenPanel.GetComponent<RectTransform>().transform.position;
            _finitePosition = new Vector2(_zeroPosition.x, _zeroPosition.y - 300);

            _controls.Camera.Select.performed += OnLeftClic;

            _gatesMask = LayerMask.GetMask("GatesMask");
            _solderMask = LayerMask.GetMask("SolderMask");
        }

        private void OnLeftClic(InputAction.CallbackContext context) => _activeGate = GetRaycastPoint();

        private Transform GetRaycastPoint() 
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, _gatesMask))
            {
                Debug.Log("Выделены ворота " + hit.transform.name);
                ActivePanel(hit.transform);
                return hit.transform;
            }
            return null;
        }

        private void ActivePanel(Transform gate)
        {
            if (gate == _greenGates.transform) _activePanel = _greenPanel;
            else if (gate == _redGates.transform) _activePanel = _redPanel;
            else if (gate == _blueGates.transform) _activePanel = _bluePanel;
            Debug.Log("активная панель  " + _activePanel);

            OpenPanel();
        }

        public void OpenPanel()
        {
            if (_activePanel != null)
            {
                if (_panelIsOpened) ClosePanel();
                else
                {
                    StartCoroutine(MoveFromTo(_finitePosition, _activePanel));
                    _panelIsOpened = true;
                }
            }
        }

        public void ClosePanel()
        {
            if (_panelIsOpened != true) return;
            else
            {
                StartCoroutine(MoveFromTo(_zeroPosition, _activePanel));
                _panelIsOpened = false;
            }
            _activePanel = null;
        }
    }
}
