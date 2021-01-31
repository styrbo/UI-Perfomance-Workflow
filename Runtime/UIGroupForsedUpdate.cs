using UnityEngine;
using UnityEngine.UI;

namespace UIWorkflow
{
    [RequireComponent(typeof(LayoutGroup))]
    public class UIGroupForsedUpdate : MonoBehaviour
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

        private void OnEnable()
        {
            _layout.CalculateLayoutInputHorizontal();
            _layout.CalculateLayoutInputVertical();
        }
    }
}