using System;
using UnityEngine;

namespace Sarteck.UIWorkflow
{
    [RequireComponent(typeof(CanvasGroup))]
    [ExecuteAlways]
    public class UIEventSender : MonoBehaviour
    {
        internal event Action OnEnable;
        internal event Action OnDisable;
        private CanvasGroup _group;

        public CanvasGroup Group => _group == null ? _group = GetComponent<CanvasGroup>() : _group;

        public bool Visible
        {
            get => Group.alpha == 1;
            set
            {
                if(value == Visible)
                    return;
                
                Group.alpha = value ? 1 : 0;
                Group.blocksRaycasts = value;
                Group.interactable = value;
                
                if(value)
                    OnEnable?.Invoke();
                else
                    OnDisable?.Invoke();
            }
        }

        public void ToggleVisibility()
        {
            Visible = !Visible;
        }

        public void SetVisibility(bool value)
        {
            Visible = value;
        }
    }
}