using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotionObject{
    public string MotionType; //StringIdentifier
    public string imageLocation; //String Location through Assets
    [TextArea(15,20)]
    public string description; //String Tooltip description

    public MotionObject(string motion, string imageloc, string desc){
        MotionType = motion;
        imageLocation = imageloc;
        description = desc;
    }
}
