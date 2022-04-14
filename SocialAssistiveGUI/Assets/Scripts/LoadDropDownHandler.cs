using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LoadDropDownHandler : MonoBehaviour
{
    public Queue q;
    TMP_Dropdown m_Dropdown;

    //Populates the LoadDropdown using default Queue dictionary (system).
    public void PopulateDropDown(){
        List<string> keyList = new List<string>(q.qDictionary.Keys);

        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(keyList);
    }

    //Populates Dropdown with all "*.q" files
    public void PopulateFileDropDown(){
        string path = Application.dataPath + "/SavedItems/"; // SavedItems folder
        int length = path.Length;
        string [] files = Directory.GetFiles(path, "*.q");
        List<string> fileList = files.ToList();

        //Remove path from string
        for (var i = 0; i < fileList.Count; i++) {
            fileList[i] = fileList[i].Remove(0, length);
        }

        //Fetch the Dropdown GameObject the script is attached to
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(fileList);
    }
}
