using UIWorkflow.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UIWorkflow
{
    [RequireComponent(typeof(LayoutGroup))]
    [RequireComponent(typeof(UIEventReceiver))]
    public class UIGroupForsedUpdate : MonoBehaviour, IEnableEvent
    {
        private LayoutGroup _layout;

        private ContentSizeFitter _filter;

        private void Awake()
        {
            _layout = GetComponent<LayoutGroup>();
            
            
            _layout.CalculateLayoutInputHorizontal();
            _layout.CalculateLayoutInputVertical();
            
            _filter.SetLayoutHorizontal();
            _filter.SetLayoutVertical();
        }
        
        public void OnEnableUI()
        {
            _layout.CalculateLayoutInputHorizontal();
                        _layout.CalculateLayoutInputVertical();
        }
    }
}