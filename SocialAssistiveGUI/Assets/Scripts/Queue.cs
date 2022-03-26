using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New Activity Queue", menuName = "ActivityQueues")]
public class Queue : ScriptableObject
{
    //Main Queue, only modified by buttons. Copy of Queue is sent to simulation.
    private LinkedList<MotionObject> activityQueue = new LinkedList<MotionObject>();
    private List<GameObject> iconList = new List<GameObject>();

    public string qName;

    private struct node
    {
        string name;
        LinkedList<MotionObject> q;
    }

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
    //Probably not void unless a reference is used.
    public void CopyCurrent(){

    }

    //Possibly not Void, return JSON string? Used in Save Button.
    public void SaveQueue(){
        Debug.Log(qName);
    }

    /*private void WriteJsonToFile(string fileName, string json)
    {
        string path = Application.dataPath + "/"; // Assets folder
        if (!File.Exists(path + fileName))
            System.IO.File.WriteAllText(path + fileName, json);
        else
            print("File already exists.");
    }*/
}
