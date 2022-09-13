using System.Collections;
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

        protected IEnumerator MoveFromTo(Vector2 targetToMove, GameObject panel)
        {
            var currentTime = 0f;
            var startPos = panel.GetComponent<RectTransform>().transform.position;

            while (currentTime < 2)
            {
                panel.GetComponent<RectTransform>().transform.position = Vector2.Lerp(startPos, targetToMove, currentTime / 2);
                currentTime += _moveSpeed * Time.deltaTime;
                yield return null;
            }
            panel.GetComponent<RectTransform>().transform.position = targetToMove;
        }
    }
}
