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

    /*/Class containing a queue's name and array of motion objects
    [Serializable]
    public class node
    {
        public node(string name, MotionObject[] q)
        {
            this.name = name;
            this.q = q;
        }

        public string name;
        public MotionObject[] q;
    }
    */

    public Dictionary<string, LinkedList<MotionObject>> qDictionary = new Dictionary<string, LinkedList<MotionObject>>();

    //used to create sprites in sliding window, need an offset variable to 
    public GameObject prefabIcon;

    private static GameObject contentWindow;


    //Add motion to queue, currently used on all Motionbuttons
    public void AddMotion(MotionObject motion)
    {
        activityQueue.AddLast(motion);
        contentWindow = GameObject.Find("Content"); //Any way to do this only once? Can't use start function (SCRIPTABLE OBJECT TYPE)
        GameObject icon = Instantiate(prefabIcon, contentWindow.transform);
        iconList.Add(icon);

        TextMeshProUGUI ltext = icon.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        ltext.text = activityQueue.Count.ToString();

        var img = Resources.Load<Sprite>(motion.imageLocation);
        var imageLinker = icon.GetComponent<Image>();
        var transformLinker = icon.GetComponent<Transform>();

        imageLinker.sprite = img;
    }

    //Debug purposes only, currently used in run button
    public void PrintQueue()
    {
        foreach (var item in activityQueue)
        {
            Debug.Log(item.MotionType);
        }
    }

    //Used for undo Button
    public void RemoveLast(){
        if (activityQueue.Count > 0){
            activityQueue.RemoveLast();
            Destroy(iconList[iconList.Count - 1]);
            iconList.RemoveAt(iconList.Count-1);
        }
        
    }

    //Used for Clear Queue Button (not yet implemented)
    public void ClearQueue(){
        activityQueue.Clear();
    }

    //Copies Current Queue to Simulation (simulation can remove freely)
 
    public LinkedList<string> CopyCurrent(){
        LinkedList<string> _q = new LinkedList<string>();
        foreach (var item in activityQueue)
        {
            _q.AddLast(item.MotionType);
        }
        return _q;
    }

    //Used in Save Button.
    public void SaveQueue(){
        // if the dictionary doesn't contain a queue with the current qname, then add the queue to the dictionary
        if (!qDictionary.ContainsKey(qName))
        {
            qDictionary.Add(qName, activityQueue);
        }
        else
        {
            Debug.Log("Queue already exists!");
        }        

        string json = JsonConvert.SerializeObject(qDictionary);

        WriteJsonToFile("Queues.txt", json);
    }

    private void WriteJsonToFile(string fileName, string json)
    {
        string path = Application.dataPath + "/SavedItems/"; // SavedItems folder
        File.WriteAllText(path + fileName, json);
    }
}
