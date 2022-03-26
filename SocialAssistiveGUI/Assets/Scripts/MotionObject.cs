using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MotionObject", menuName = "MotionObject")]
[System.Serializable]
public class MotionObject : ScriptableObject{
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
