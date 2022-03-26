using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonScript : MonoBehaviour
{
    public Queue _queue;
    public MotionObject motion;
    public void AddMotiontoQueue()
    {
        _queue.AddMotion(motion);
    }
    public void DisplayQueue()
    {
        _queue.PrintQueue();
    }

    public void RemoveLastMotion(){
        _queue.RemoveLast();
    }

    public void SaveQueueButton(){
        TextMeshProUGUI ltext = gameObject.transform.parent.GetChild(1).GetChild(0).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        _queue.qName = ltext.text;
        _queue.SaveQueue();
    }
}
