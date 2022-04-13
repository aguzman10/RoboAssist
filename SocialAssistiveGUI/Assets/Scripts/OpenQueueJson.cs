using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class OpenQueueJson : MonoBehaviour
{
    public string json;

    public Queue _queue; //holds default Queue

    // Start is called before the first frame update
    void Start()
    {
        string filename = Application.dataPath + "/SavedItems/Queues.json"; // Queues.txt file
        if (File.Exists(filename))
        {
            json = File.ReadAllText(filename); //Read File if it exists
            if (String.IsNullOrEmpty(json)){ //If File is empty, create new Dictionary.
                _queue.qDictionary = new Dictionary<string, LinkedList<MotionObject>>();
            }
            else{
                _queue.qDictionary = JsonConvert.DeserializeObject<Dictionary<string, LinkedList<MotionObject>>>(json);
            }
        }
        else //If File does not Exist
        {
            _queue.qDictionary = new Dictionary<string, LinkedList<MotionObject>>();
        }

    }

}
