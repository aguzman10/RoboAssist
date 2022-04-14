using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "New Activity Queue", menuName = "ActivityQueues")]
public class Queue : ScriptableObject
{
    //Main Queue, only modified by buttons. Copy of Queue is sent to simulation.
    private LinkedList<MotionObject> activityQueue = new LinkedList<MotionObject>();
    private List<GameObject> iconList = new List<GameObject>();

    // String containing the name of a specific activity queue
    public string qName;

    public Dictionary<string, LinkedList<MotionObject>> qDictionary = new Dictionary<string, LinkedList<MotionObject>>();

    //used to create sprites in sliding window, need an offset variable to 
    public GameObject prefabIcon;

    private static GameObject contentWindow;


    //Add motion to queue, currently used on all Motionbuttons
    public void AddMotion(MotionObject motion)
    {
        //Add To Linked List and Create Icon
        activityQueue.AddLast(motion);
        contentWindow = GameObject.Find("Content"); //Any way to do this only once? Can't use start function (SCRIPTABLE OBJECT TYPE)
        GameObject icon = Instantiate(prefabIcon, contentWindow.transform);
        iconList.Add(icon); //Add Icon to List (for Deleting Purposes)

        //Set Index under Icon
        TextMeshProUGUI ltext = icon.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        ltext.text = activityQueue.Count.ToString();

        //Load Icon Image
        var img = Resources.Load<Sprite>(motion.imageLocation);
        var imageLinker = icon.GetComponent<Image>();
        var transformLinker = icon.GetComponent<Transform>();
        imageLinker.sprite = img;
    }

    //Debug purposes only; not currently used for anything
    public void PrintQueue()
    {
        foreach (var item in activityQueue)
        {
            Debug.Log(item.MotionType);
        }
    }

    //Used for undo Button
    public void RemoveLast(){
        if (activityQueue.Count > 0){ //If item to remove
            activityQueue.RemoveLast(); //remove from list
            Destroy(iconList[iconList.Count - 1]); //destroy icon
            iconList.RemoveAt(iconList.Count-1); //remove icon from list
        }
        
    }

    //Used for Clear Queue Button and Loading a Queue
    public void ClearQueue(){
        activityQueue.Clear(); //clear all motion objects
        foreach(GameObject x in iconList)
        {
         Destroy(x); //delete icons
        }
        iconList.Clear(); //clear iconlist
    }

    //Copies Current Queue to Simulation (simulation can remove freely)
    // NOTE: ONLY STRINGS ARE COPIED OVER
    public LinkedList<string> CopyCurrent(){
        LinkedList<string> _q = new LinkedList<string>();
        foreach (var item in activityQueue)
        {
            _q.AddLast(item.MotionType);
        }
        return _q;
    }

    //Used in Save System Button.
    public void SaveQueue(){
        // if the dictionary doesn't contain a queue with the current qname, then add the queue to the dictionary
        if (!qDictionary.ContainsKey(qName))
        {
            qDictionary.Add(qName, new LinkedList<MotionObject>(activityQueue));
            //Serialize and Write the Dictionary to a File
            string json = JsonConvert.SerializeObject(qDictionary);
            WriteJsonToFile("Queues.json", json);
        }
        else
        {
            Debug.Log("Queue already exists!"); //IMPLEMENT THIS
        }        
    }

    public void SaveFile(string name){
        name = name + ".q";
        string path = Application.dataPath + "/SavedItems/"; // SavedItems folder
        string [] files = Directory.GetFiles(path, "*.q");
        if (!Array.Exists(files, x => x == (path + name)))
        {
            //Serialize and Write the Dictionary to a File
            string json = JsonConvert.SerializeObject(activityQueue);
            WriteJsonToFile(name, json);
        }
        else
        {
            Debug.Log("File already exists!"); //IMPLEMENT THIS
        }        
    }

    //Dictionary Key is passed and the value's icons and motion objects are loaded
    public void LoadQueue(string key){
        ClearQueue();
        activityQueue = new LinkedList<MotionObject>(qDictionary[key]);
        LoadIcons();
    }

    public void LoadQueueFile(string fileName){
        ClearQueue();
        string path = Application.dataPath + "/SavedItems/" + fileName;

        string json = File.ReadAllText(path); //Read File if it exists
        if (String.IsNullOrEmpty(json)){ //If File is empty, create new LinkedList
            activityQueue = new LinkedList<MotionObject>();
        }
        else{
            activityQueue = new LinkedList<MotionObject>(JsonConvert.DeserializeObject<LinkedList<MotionObject>>(json));
            LoadIcons();
        }

    }

    //Remove a system Queue by Dictionary Key
    public void RemoveQueue(string key){
        qDictionary.Remove(key);
        string json = JsonConvert.SerializeObject(qDictionary);
        WriteJsonToFile("Queues.json", json);
    }

    //Used for loading Queue from system or file
    public void LoadIcons(){
        int count = 1;
        TextMeshProUGUI ltext;
        contentWindow = GameObject.Find("Content"); //Any way to do this only once? Can't use start function (SCRIPTABLE OBJECT TYPE)
        foreach (var item in activityQueue) //For each motion object loaded in Queue
        {
            //Create Icons, set index and image
            GameObject icon = Instantiate(prefabIcon, contentWindow.transform);
            iconList.Add(icon);

            ltext = icon.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            ltext.text = count.ToString();

            var img = Resources.Load<Sprite>(item.imageLocation);
            var imageLinker = icon.GetComponent<Image>();
            var transformLinker = icon.GetComponent<Transform>();

            imageLinker.sprite = img;
            count++;
        }
    }

    //Save to System
    private void WriteJsonToFile(string fileName, string json)
    {
        string path = Application.dataPath + "/SavedItems/"; // SavedItems folder
        File.WriteAllText(path + fileName, json);
    }

    public bool isEmpty(){
        return (activityQueue.Count == 0);
    }
}
