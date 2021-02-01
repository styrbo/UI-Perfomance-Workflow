using TMPro;
using UIWorkflow.Events;
using UnityEngine;

[RequireComponent(typeof(UIEventReceiver))]
public class TMP_Culling : MonoBehaviour
{
    private TMP_Text _text;
    private UIEventReceiver _receiver;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _receiver = GetComponent<UIEventReceiver>();

        _text.enabled = _receiver.Visible;
        
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
        _text.enabled = true;
    }

    private void ReceiverOnOnDisable()
    {
        _text.enabled = false;
    }
}
