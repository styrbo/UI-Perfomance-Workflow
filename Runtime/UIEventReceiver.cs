using UnityEngine;

namespace Sarteck.UIWorkflow
{
    [DisallowMultipleComponent]
    public class UIEventReceiver : MonoBehaviour
    {
        private UIEventSender _owner;

        public bool Visible
        {
            get => Owner.Visible;
            set => Owner.Visible = value;
        }

        public event UIEventSender.VisibilityAction OnEnable
        {
            add => Owner.OnEnable += value;
            remove => Owner.OnEnable -= value;
        }

        public event UIEventSender.VisibilityAction  OnDisable
        {
            add => Owner.OnDisable += value;
            remove => Owner.OnDisable -= value;
        }

        public UIEventSender Owner => _owner == null ? _owner = FindOwner() : _owner;

        public UIEventSender FindOwner()
        {
            var t = transform;
            UIEventSender owner = null;

            while (t != null && owner == null)
            {
                owner = t.GetComponent<UIEventSender>();

                if (owner == null)
                    t = t.parent;
                else
                    return owner;
            }

            Debug.LogWarning("the event receiver not found sender", this);
            return null;
        }
    }
}