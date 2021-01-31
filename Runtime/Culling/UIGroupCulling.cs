using System.Collections;
using System.Collections.Generic;
using UIWorkflow.Events;
using UnityEngine;

namespace UIWorkflow.Culling
{
    [RequireComponent(typeof(UIEventSender))]
    public class UIGroupCulling : MonoBehaviour
    {
        private UIEventSender _sender;
        private RectTransform _rect;

        private static bool _init = false;
        private static readonly List<UIGroupCulling> _cullings = new List<UIGroupCulling>();

        private static Vector3[] objectCorners = new Vector3[4];
        private static Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
        private static Vector3 tempScreenSpaceCorner;
        private static UIGroupCulling _curCulling;
        private static Camera _cam;
        private static int visibleCorners;
        private static int listCount;

        private void Awake()
        {
            _sender = GetComponent<UIEventSender>();
            _rect = GetComponent<RectTransform>();

            if (_cam == null)
                _cam = Camera.main;
        }

        private void Start()
        {
            if (_init) return;

            var manager = new GameObject("culling manager").AddComponent<UIGroupCullingManager>();
            manager.StartCoroutine(OnUpdate());

            _init = true;
        }

        private static IEnumerator OnUpdate()
        {
            int i;

            while (true)
            {
                i = 0;

                for (; i < listCount; i += 1)
                {
                    _curCulling = _cullings[i];

                    _curCulling._sender.Visible = IsVisibleFrom(_curCulling._rect);
                }

                yield return null;
            }
        }

        private void OnEnable()
        {
            _cullings.Add(this);
            listCount++;
        }

        private void OnDisable()
        {
            _cullings.Remove(this);
            listCount--;
        }

        //code from https://forum.unity.com/threads/test-if-ui-element-is-visible-on-screen.276549/#post-2978773
        /// <summary>
        /// Counts the bounding box corners of the given RectTransform that are visible from the given Camera in screen space.
        /// </summary>
        /// <returns>The amount of bounding box corners that are visible from the Camera.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera.</param>
        public static bool IsVisibleFrom(RectTransform rectTransform)
        {
            // Screen space bounds (assumes camera renders across the entire screen)
            screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);

            rectTransform.GetWorldCorners(objectCorners);

            visibleCorners = 0;
            for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
            {
                tempScreenSpaceCorner = _cam.WorldToScreenPoint(objectCorners[i]); // Cached
                if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
                {
                    return true;
                }
            }
            return false;
        }
    }
}