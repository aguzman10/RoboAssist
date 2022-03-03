using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour, IPointerDownHandler{
   
   public void OnPointerDown(PointerEventData eventData){
       Debug.Log("OnPointerDown");
   }
}
