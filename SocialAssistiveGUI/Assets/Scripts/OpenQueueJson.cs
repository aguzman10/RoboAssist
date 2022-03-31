using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class OpenQueueJson : MonoBehaviour
{
    public string json;

    public Queue _queue;
    // Start is called before the first frame update
    void Start()
    {
        string filename = Application.dataPath + "/SavedItems/Queues.txt"; // Queues.txt file
        if (File.Exists(filename))
        {
            json = File.ReadAllText(filename);
            _queue.qDictionary = JsonConvert.DeserializeObject<Dictionary<string, LinkedList<MotionObject>>>(json);
        }
        else
        {
            _queue.qDictionary = new Dictionary<string, LinkedList<MotionObject>>();
        }

    }

}
