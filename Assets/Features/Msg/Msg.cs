using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Msg : MonoBehaviour
{
    public static Msg instance = null;
    void Awake()
    {
        instance = this;
    }

    public delegate void MessageHandleDelegate(Message message);
    private Dictionary<string, List<MessageHandleDelegate>> listenerDict = new Dictionary<string, List<MessageHandleDelegate>>();
    private Queue<Message> messageQueue = new Queue<Message>();
    public List<string> messages = new List<string>();

    void Update()
    {
        if (messageQueue.Count > 0)
        {
            FireMessages();
        }
    }

    void OnDisable()
    {
        FireMessages();
    }

    void OnApplicationQuit()
    {
        FireMessages();
    }

    private void FireMessages()
    {
        while (messageQueue.Count > 0)
        {
            Message message = messageQueue.Dequeue();
            if (listenerDict.ContainsKey(message.type))
            {
                List<MessageHandleDelegate> listeners = listenerDict[message.type];
                foreach (MessageHandleDelegate listener in listeners)
                {
                    listener.Invoke(message);
                }
            }
            messages.Add(message.type);
        }
    }

    public void RegisterListener(string messageType, MessageHandleDelegate listener)
    {
        if (!listenerDict.ContainsKey(messageType))
        {
            listenerDict[messageType] = new List<MessageHandleDelegate>();
        }
        listenerDict[messageType].Add(listener);
    }

    public void UnregisterListener(string messageType, MessageHandleDelegate listener)
    {
        if (listenerDict.ContainsKey(messageType))
        {
            listenerDict[messageType].Remove(listener);
            if (listenerDict[messageType].Count == 0)
            {
                listenerDict.Remove(messageType);
            }
        }
    }

    public void TriggerMessage(Message message)
    {
        messageQueue.Enqueue(message);
    }
}

public class Message
{
    public string type;
    public Message () { type = this.GetType ().Name; }
}