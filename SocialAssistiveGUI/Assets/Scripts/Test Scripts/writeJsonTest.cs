using UnityEngine;
using System.IO;

public class writeJsonTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        userPerformance userLog = new userPerformance();
        userLog.name = "Misty";
        userLog.totalCorrect = 5;
        userLog.totalIncorrect = 2;

        string fileName = "simpleExample.json";
        string json = JsonUtility.ToJson(userLog); // Create the json
        WriteJsonToFile(fileName, json);
    }

    public void WriteJsonToFile(string fileName, string json)
    {
        string path = Application.dataPath + "/"; // Assets folder
        if (!File.Exists(path + fileName))
            System.IO.File.WriteAllText(path + fileName, json);
        else
            print("File already exists.");
    }

}
