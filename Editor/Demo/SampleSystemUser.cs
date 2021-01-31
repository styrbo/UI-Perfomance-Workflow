using System;
using System.Collections;
using Sarteck.UIWorkflow;
using UnityEngine;

[RequireComponent(typeof(UIEventReceiver))]
public class SampleSystemUser : MonoBehaviour
{
    private UIEventReceiver _receiver;
    private static bool _single;

    private void Awake()
    {
        _receiver = GetComponent<UIEventReceiver>();
        _receiver.OnEnable += ReceiverOnEnable;
        _receiver.OnDisable += ReceiverOnDisable;

        if (_single == false)
        {
            StartCoroutine(OnUpdate());
            _single = true;
        }
    }

    private void ReceiverOnEnable()
    {
       // print("object enable!");
        SomeLargeOperation();
    }

    private void ReceiverOnDisable()
    {
      //  print("object disable!");
        SomeLargeOperation();
    }

    private void SomeLargeOperation()
    {
        var value = 0;
        for (var i = 0; i < 1000000; i++)
        {
            value += i % 10;
        }
    }

    private IEnumerator OnUpdate()
    {
        while (true)
        {
            if(Input.GetMouseButtonDown(0))
                _receiver.Visible = !_receiver.Visible;

            yield return null;
        }
    }
}
