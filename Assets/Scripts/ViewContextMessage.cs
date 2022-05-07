using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class MessageView : ISignal
{
    public string Message;
}

public class ViewContextMessage : MonoBehaviour
{
    [SerializeField] private Text textView;

    private void Start()
    {
        textView = GetComponent<Text>();
        SignalSystem<MessageView>.Sub(OnMessageViewHandler);
    }

    private void OnDestroy()
    {
        SignalSystem<MessageView>.UnSub(OnMessageViewHandler);
    }

    private void OnMessageViewHandler(MessageView message)
    {
        textView.text = message.Message;
    }
}
