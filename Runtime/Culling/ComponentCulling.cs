using UIWorkflow.Events;
using UnityEngine;

namespace UIWorkflow.Culling
{
    [RequireComponent(typeof(UIEventReceiver))]
    public class ComponentCulling : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _component;
        
        private UIEventReceiver _receiver;

        private void Awake()
        {
            _receiver = GetComponent<UIEventReceiver>();
        
            _component.enabled = _receiver.Visible;
            _receiver.OnEnable += ReceiverOnOnEnable;
            _receiver.OnDisable += ReceiverOnOnDisable;
        }

        private void OnDestroy()
        {
            _receiver.OnEnable -= ReceiverOnOnEnable;
            _receiver.OnDisable -= ReceiverOnOnDisable;
        }

        private void ReceiverOnOnEnable()
        {
            _component.enabled = true;
        }

        private void ReceiverOnOnDisable()
        {
            _component.enabled = false;
        }
    }
}