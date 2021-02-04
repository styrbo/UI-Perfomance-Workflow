using System;
using UnityEngine;

namespace UIWorkflow.Events
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

        public event UIEventSender.VisibilityAction OnDisable
        {
            add => Owner.OnDisable += value;
            remove => Owner.OnDisable -= value;
        }

        public UIEventSender Owner => _owner == null ? _owner = FindOwner() : _owner;

        private IEnableEvent[] _enableEvents;
        private IDisableEvent[] _disableEvents;

        private void Awake()
        {
            GetReceivers();
            TrySubscribeEvents();
            if(Visible)
                InvokeEnableEvents();
        }
        
        private void OnDestroy()
        {
            InvokeDisableEvents();
            TryUnsubscribeEvents();
        }

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

        private void GetReceivers()
        {
            _enableEvents = GetComponents<IEnableEvent>();
            _disableEvents = GetComponents<IDisableEvent>();
        }

        private void TrySubscribeEvents()
        {
            if (_enableEvents.Length > 0)
                OnEnable += InvokeEnableEvents;
            if (_disableEvents.Length > 0)
                OnDisable += InvokeDisableEvents;
        }

        private void TryUnsubscribeEvents()
        {
            if (_enableEvents.Length > 0)
                OnEnable -= InvokeEnableEvents;
            if (_disableEvents.Length > 0)
                OnDisable -= InvokeDisableEvents;
        }

        private void InvokeEnableEvents()
        {
            foreach (var enableEvent in _enableEvents)
            {
                enableEvent.OnEnableUI();
            }
        }

        private void InvokeDisableEvents()
        {
            foreach (var enableEvent in _disableEvents)
            {
                enableEvent.OnDisableUI();
            }
        }
    }
}