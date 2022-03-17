using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Activity Queue", menuName = "ActivityQueues")]
public class Queue : ScriptableObject
{
    public LinkedList<MotionObject> activityQueue = new LinkedList<MotionObject>();
    
    public void AddMotion(MotionObject motion)
    {
        activityQueue.AddLast(motion);
    }

    public void PrintQueue()
    {
        foreach (var item in activityQueue)
        {
            Debug.Log(item.MotionType);
        }
    }
}
