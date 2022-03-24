using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MotionObject", menuName = "MotionObject")]
public class MotionObject : ScriptableObject{
    public GameObject prefab;
    public string MotionType;
    [TextArea(15,20)]
    public string description;

}
