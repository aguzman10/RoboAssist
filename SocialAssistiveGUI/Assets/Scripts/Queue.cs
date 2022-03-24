using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Activity Queue", menuName = "ActivityQueues")]
public class Queue : ScriptableObject
{
    //Main Queue, only modified by buttons. Copy of Queue is sent to simulation.
    private LinkedList<MotionObject> activityQueue = new LinkedList<MotionObject>();

    private int offset = 0;
    private int count = 0;
    //used to create sprites in sliding window, need an offset variable to 
    public GameObject prefab;

    //Add motion to queue, currently used on all Motionbuttons
    public void AddMotion(MotionObject motion)
    {
        activityQueue.AddLast(motion);
        count += 1;
    }

    //Debug purposes only, currently used in run button
    public void PrintQueue()
    {
        foreach (var item in activityQueue)
        {
            Debug.Log(item.MotionType);
        }
    }

    public void RemoveLast(){
        activityQueue.RemoveLast();
    }

    public void ClearQueue(){
        activityQueue.Clear();
    }

    //Copies Current Queue to Simulation (simulation can remove freely)
    //Probably not void unless a reference is used.
    public void CopyCurrent(){

    }
    //Possibly not Void, return JSON string? Used in Save Button.
    public void SaveQueue(){

    }
}
