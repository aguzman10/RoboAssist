using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ButtonScript : MonoBehaviour
{
    public Queue _queue;

    public string mtype, imageloc;

    public void AddMotiontoQueue()
    {
        MotionObject motion = new MotionObject(mtype, imageloc);
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
        //TextMeshProUGUI ltext = gameObject.transform.parent.GetChild(1).GetChild(0).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
        TMP_InputField ltext = gameObject.transform.parent.GetChild(1).GetComponent<TMP_InputField>();
        string qName = ltext.text;
        if(string.IsNullOrEmpty(qName)){
            Debug.Log("Empty Name: Please Enter Valid Name");
        }
        else{
            _queue.qName = qName;
            _queue.SaveQueue();
        }
    }

    public void LoadQueueButton(){
        TMP_Dropdown m_Dropdown = gameObject.transform.parent.GetChild(2).GetComponent<TMP_Dropdown>();
        string selected = m_Dropdown.options[m_Dropdown.value].text;
        _queue.LoadQueue(selected);
    }
}
