using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ButtonScript : MonoBehaviour
{
    public Queue _queue; //holds default queue

    public string mtype, imageloc; //holds motion type an image location for motion buttons

    //Main function used by Motion Buttons
    public void AddMotiontoQueue()
    {
        MotionObject motion = new MotionObject(mtype, imageloc);
        _queue.AddMotion(motion);
    }

    //Used for Debugging Purposes in early phases of GUI (Print button)
    public void DisplayQueue()
    {
        _queue.PrintQueue();
    }

    //Used for Undo Button
    public void RemoveLastMotion(){
        _queue.RemoveLast();
    }

    //Used for Clear Button
    public void ClearQueue(){
        _queue.ClearQueue();
    }

    //Used for SaveButton in SaveTools Menu
    public void SaveQueueButton(){
        TMP_InputField ltext = gameObject.transform.parent.GetChild(1).GetComponent<TMP_InputField>(); //Get Input Name
        string qName = ltext.text;

        if(string.IsNullOrEmpty(qName)){ //Check if any input
            Debug.Log("Empty Name: Please Enter Valid Name");
        }
        else{
            _queue.qName = qName;
            _queue.SaveQueue();
        }
    }

    //Used for LoadButton in LoadTools Menu
    public void LoadQueueButton(){
        TMP_Dropdown m_Dropdown = gameObject.transform.parent.GetChild(2).GetComponent<TMP_Dropdown>(); //get selected queue

        if (m_Dropdown.options.Count > 0){ //If there is any options available
            string selected = m_Dropdown.options[m_Dropdown.value].text;
            _queue.LoadQueue(selected);
        }
        else {
            Debug.Log("No Queues to Load");
        }
    }

    //Used for RemoveButton in LoadTools Menu
    public void RemoveSelectedQueue(){
        TMP_Dropdown m_Dropdown = gameObject.transform.parent.GetChild(2).GetComponent<TMP_Dropdown>(); //get selected queue
        if (m_Dropdown.options.Count > 0){ //If there is any options available
            string selected = m_Dropdown.options[m_Dropdown.value].text;
            _queue.RemoveQueue(selected);
        }
        else {
            Debug.Log("No Queues to Remove");
        }
        
    }
}
