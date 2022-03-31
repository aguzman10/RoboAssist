using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public Queue _queue;

    public TooltipTrigger trigger;

    public string mtype, imageloc, desc;

    private void Start()
    {
        trigger.setDescription(desc);
        trigger.setName(mtype);
    }

    public void AddMotiontoQueue()
    {
        MotionObject motion = new MotionObject(mtype, imageloc, desc);
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
