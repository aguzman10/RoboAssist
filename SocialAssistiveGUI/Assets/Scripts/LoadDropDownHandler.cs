using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadDropDownHandler : MonoBehaviour
{
    public Queue q;
    // Start is called before the first frame update
    TMP_Dropdown m_Dropdown;

    public void PopulateDropDown(){
        List<string> keyList = new List<string>(q.qDictionary.Keys);

        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(keyList);


    }
}
