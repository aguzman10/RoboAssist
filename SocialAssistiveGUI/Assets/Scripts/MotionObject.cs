using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotionObject{
    public string MotionType; //StringIdentifier
    public string imageLocation; //String Location through Assets

     //Constructor
    public MotionObject(string motion, string imageloc){
        MotionType = motion;
        imageLocation = imageloc;
    }
}
