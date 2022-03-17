using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Queue _queue;
    public MotionObject motion;
    public void AddMotiontoQueue()
    {
        _queue.AddMotion(motion);
    }
        public void DisplayQueue()
    {
        _queue.PrintQueue();
    }

}
