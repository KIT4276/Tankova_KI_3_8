using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ziggurat
{
    public class BasePanelManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Panel move speed")]
        protected float _moveSpeed;

        [Header("Camera"), SerializeField]
        protected Camera _camera;

        protected bool _panelIsOpened = false;
        protected Ziggurat.Controls _controls;
        //protected GameObject _activePanel;

        //protected void Awake() => _controls = new Controls();

        protected IEnumerator MoveFromTo(Vector2 targetToMove, GameObject panel)
        {
            var currentTime = 0f;
            var startPos = panel.GetComponent<RectTransform>().transform.position;

            while (currentTime < 2)
            {
                Debug.Log("Move");
                panel.GetComponent<RectTransform>().transform.position = Vector2.Lerp(startPos, targetToMove, currentTime / 2);
                currentTime += _moveSpeed * Time.deltaTime;
                yield return null;
            }
            panel.GetComponent<RectTransform>().transform.position = targetToMove;
        }

        //protected void OnEnable() => _controls.Camera.Enable();

        //protected void OnDisable() => _controls.Camera.Disable();

        //protected void OnDestroy() => _controls.Dispose();
    }
}
