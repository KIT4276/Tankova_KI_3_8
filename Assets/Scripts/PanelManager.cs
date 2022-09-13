using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Ziggurat
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Panel move speed")]
        private float _moveSpeed;

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
        [Space, SerializeField]
        private GameObject _infoPanel;

        [Header("Panel close buttons"), SerializeField, Tooltip("GreenPanelCloseButton")]
        private Button _greenPanelCloseButton;
        [SerializeField, Tooltip("RedPanelCloseButton")]
        private Button _redPanelCloseButton;
        [SerializeField, Tooltip("BluePanelCloseButton")]
        private Button _bluePanelCloseButton;

        [Header("Camera"), SerializeField]
        private Camera _camera;

        protected GameObject _activePanel;
        private Transform _activeGate;
        private Transform _activeSolder;
        private bool _panelIsOpened = false;
        private bool _infoPanelIsOpened = false;
        private Ziggurat.Controls _controls;
        private Vector2 _zeroPosition;
        private Vector2 _zeroInfoPosition;
        private Vector2 _finitePosition;
        private Vector2 _finiteInfoPosition;
        private LayerMask _gatesMask;
        private LayerMask _solderMask;
        private string _gateName;
        

        private void Awake()
        {
            _controls = new Ziggurat.Controls();
            _controls.Camera.Select.performed += OnLeftClic;
            _zeroPosition = _greenPanel.GetComponent<RectTransform>().transform.position;
            _finitePosition = new Vector2(202, 470);
            _zeroInfoPosition = _infoPanel.GetComponent<RectTransform>().transform.position;
            _finiteInfoPosition = new Vector2(1100, _infoPanel.transform.position.y);
        }

        private void Start()
        {
            _gatesMask = LayerMask.GetMask("GatesMask");
            _solderMask = LayerMask.GetMask("SolderMask");
        }

        private void OnLeftClic(InputAction.CallbackContext context) 
        {
            _activeGate = GetRaycastPoint();
        }

        private Transform GetRaycastPoint() 
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out var hit, _gatesMask))
            {
                Debug.Log("Выделены ворота");
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
            else _activePanel = _infoPanel;
            Debug.Log("активная панель  " + _activePanel);

            if(_activePanel != _infoPanel) OpenPanel();
        }

        public void OpenPanel()
        {
            if (_panelIsOpened) ClosePanel();
            else
            {
                StartCoroutine(MoveFromTo(_finitePosition));
                _panelIsOpened = true;
            }
        }

        public void ClosePanel()
        {
            if (_panelIsOpened != true) return;
            else
            {
                StartCoroutine(MoveFromTo(_zeroPosition));
                _panelIsOpened = false;
            }
        }

        public void OpenInfoPanel()
        {
            _activePanel = _infoPanel;
            if (_infoPanelIsOpened) CloseInfoPanel();
            else
            {
                StartCoroutine(MoveFromTo(_finiteInfoPosition));
                _infoPanelIsOpened = true;
            }
        }

        public void CloseInfoPanel()
        {
            StartCoroutine(MoveFromTo(_zeroInfoPosition));
            _infoPanelIsOpened = false;
        }

        private IEnumerator MoveFromTo(Vector2 targetToMove)
        {
            var currentTime = 0f;
            var startPos = _activePanel.GetComponent<RectTransform>().transform.position;
            targetToMove = new Vector2(targetToMove.x, targetToMove.y);

            while (currentTime < 2)
            {
                _activePanel.GetComponent<RectTransform>().transform.position = Vector2.Lerp(startPos, targetToMove, currentTime / 2);
                currentTime += _moveSpeed * Time.deltaTime;
                yield return null;
            }
            _activePanel.GetComponent<RectTransform>().transform.position = targetToMove;
        }

        private void OnEnable() => _controls.Camera.Enable();

        private void OnDisable() => _controls.Camera.Disable();

        private void OnDestroy() => _controls.Dispose();
    }
}
