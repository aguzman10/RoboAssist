using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotionObject{
    public string MotionType; //StringIdentifier
    public string imageLocation; //String Location through Assets

    public MotionObject(string motion, string imageloc){
        imageLocation = imageloc;
    }
}
